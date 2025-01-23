using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Serialization;
using System.Text;
using Asp.Versioning;
using Com.DanLiris.Service.EmergencyAttendance.Lib;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Utilities.CacheManager;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Interfaces;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Services;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Utilities;
using Com.DanLiris.Service.EmergencyAttendance.WebApi.SchedulerJobs;
using Com.DanLiris.Service.EmergencyAttendance.WebApi.Helpers;
using Com.DanLiris.Service.EmergencyAttendance.WebApi.Tools.AppInsight;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Serializers;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Facades;

namespace Com.DanLiris.Service.EmergencyAttendance.WebApi
{
    public class Startup
    {
        /* Hard Code */
        private string[] EXPOSED_HEADERS = new string[] { "Content-Disposition", "api-version", "content-length", "content-md5", "content-type", "date", "request-id", "response-time" };
        private string PURCHASING_POLICITY = "PurchasingPolicy";

        public IConfiguration Configuration { get; }

        public bool HasAppInsight => !string.IsNullOrEmpty(Configuration.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY") ?? Configuration.GetValue<string>("ApplicationInsights:InstrumentationKey"));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region Register

        //private void RegisterEndpoints()
        //{
        //    APIEndpoint.Purchasing = Configuration.GetValue<string>(Constant.PURCHASING_ENDPOINT) ?? Configuration[Constant.PURCHASING_ENDPOINT];
        //    APIEndpoint.Core = Configuration.GetValue<string>(Constant.CORE_ENDPOINT) ?? Configuration[Constant.CORE_ENDPOINT];
        //    APIEndpoint.Inventory = Configuration.GetValue<string>(Constant.INVENTORY_ENDPOINT) ?? Configuration[Constant.INVENTORY_ENDPOINT];
        //    APIEndpoint.Finance = Configuration.GetValue<string>(Constant.FINANCE_ENDPOINT) ?? Configuration[Constant.FINANCE_ENDPOINT];
        //    APIEndpoint.CustomsReport = Configuration.GetValue<string>(Constant.CUSTOMSREPORT_ENDPOINT) ?? Configuration[Constant.CUSTOMSREPORT_ENDPOINT];
        //    APIEndpoint.Sales = Configuration.GetValue<string>(Constant.SALES_ENDPOINT) ?? Configuration[Constant.SALES_ENDPOINT];
        //    APIEndpoint.Auth = Configuration.GetValue<string>(Constant.AUTH_ENDPOINT) ?? Configuration[Constant.AUTH_ENDPOINT];
        //    APIEndpoint.GarmentProduction = Configuration.GetValue<string>(Constant.GARMENT_PRODUCTION_ENDPOINT) ?? Configuration[Constant.GARMENT_PRODUCTION_ENDPOINT];
        //    APIEndpoint.PackingInventory = Configuration.GetValue<string>(Constant.PACKINGINVENTORY_ENDPOINT) ?? Configuration[Constant.PACKINGINVENTORY_ENDPOINT];

        //    AuthCredential.Username = Configuration.GetValue<string>(Constant.USERNAME) ?? Configuration[Constant.USERNAME];
        //    AuthCredential.Password = Configuration.GetValue<string>(Constant.PASSWORD) ?? Configuration[Constant.PASSWORD];
        //}

        private void RegisterFacades(IServiceCollection services)
        {
            services
                .AddTransient<ICoreData, CoreData>()
                .AddTransient<ICoreHttpClientService, CoreHttpClientService>()
                .AddTransient<IAttendanceFacade, AttendanceFacade>();
        }

        private void RegisterServices(IServiceCollection services, bool isTest)
        {
            services
                .AddScoped<IdentityService>()
                .AddScoped<ValidateService>()
                .AddScoped<IHttpClientService, HttpClientService>()
                .AddScoped<IValidateService, ValidateService>();
        }

        private void RegisterSerializationProvider()
        {
            BsonSerializer.RegisterSerializationProvider(new SerializationProvider());
        }

        #endregion Register

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString(Constant.DEFAULT_CONNECTION) ?? Configuration[Constant.DEFAULT_CONNECTION];
            string connectionStringAttendance = Configuration.GetConnectionString("AttendanceConnection") ?? Configuration["AttendanceConnection"];

            string env = Configuration.GetValue<string>(Constant.ASPNETCORE_ENVIRONMENT);

            /* dbContext Emergency */
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(1000 * 60 * 20)));
            /* dbContext Attendance */
            services.AddDbContext<AttendanceDbContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionStringAttendance));

            //RegisterEndpoints();
            RegisterFacades(services);
            RegisterServices(services, env.Equals("Test"));

            services.AddAutoMapper(typeof(BaseAutoMapperProfile));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMemoryCache();

            RegisterSerializationProvider();

            services
                 .AddApiVersioning(options =>
                 {
                     options.ReportApiVersions = true;
                     options.AssumeDefaultVersionWhenUnspecified = true;
                     options.DefaultApiVersion = new ApiVersion(1, 1);
                 });

            #region Authenticatio
            string Secret = Configuration.GetValue<string>("Secret") ?? Configuration["Secret"];
            SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        IssuerSigningKey = Key,
                        ValidateIssuerSigningKey = true,
                    };
                });

            #endregion

            /* CORS */
            services.AddCors(options => options.AddPolicy(PURCHASING_POLICITY, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders(EXPOSED_HEADERS);
            }));

            /* API */
            services
               .AddMvcCore()
               .AddApiExplorer()
               .AddAuthorization();

            services
                .AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            // App Insight
            if (HasAppInsight)
            {
                services.AddApplicationInsightsTelemetry();
                services.AddAppInsightRequestBodyLogging();
            }

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
                options.OperationFilter<ResponseHeaderFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (HasAppInsight)
            {
                app.UseAppInsightRequestBodyLogging();
                app.UseAppInsightResponseBodyLogging();
            }

            app.UseAuthentication();
            app.UseCors(PURCHASING_POLICITY);
            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }
    }
}
