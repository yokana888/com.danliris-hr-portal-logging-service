using Com.DanLiris.Service.EmergencyAttendance.Lib.Helpers;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Utilities.CacheManager.CacheData;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Utilities.CacheManager
{
    public class CoreData : ICoreData
    {
        private readonly ICoreHttpClientService _http;
        private readonly IMemoryCacheManager _cacheManager;

        public CoreData(IServiceProvider serviceProvider)
        {
            _http = serviceProvider.GetService<ICoreHttpClientService>();
            _cacheManager = serviceProvider.GetService<IMemoryCacheManager>();
        }

        public void SetBankAccount()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var token = GetTokenAsync().Result;

            var bankAccountUri = APIEndpoint.Core + $"master/account-banks?size={int.MaxValue}";
            var bankAccountResponse = _http.GetAsync(bankAccountUri, token).Result;
        }

        public void SetCategoryCOA()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var token = GetTokenAsync().Result;

            var categoryUri = APIEndpoint.Core + $"master/categories?size={int.MaxValue}";
            var categoryResponse = _http.GetAsync(categoryUri, token).Result;
        }

        public void SetDivisionCOA()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var token = GetTokenAsync().Result;

            var categoryUri = APIEndpoint.Core + $"master/divisions?size={int.MaxValue}";
            var categoryResponse = _http.GetAsync(categoryUri, token).Result;
        }

        public void SetPPhCOA()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var token = GetTokenAsync().Result;

            var incomeTaxUri = APIEndpoint.Core + $"master/income-taxes?size={int.MaxValue}";
            var incomeTaxResponse = _http.GetAsync(incomeTaxUri, token).Result;
        }

        public void SetUnitCOA()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var token = GetTokenAsync().Result;

            var categoryUri = APIEndpoint.Core + $"master/units?size={int.MaxValue}";
            var categoryResponse = _http.GetAsync(categoryUri, token).Result;
        }

        protected async Task<string> GetTokenAsync()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var response = await _http.PostAsync(APIEndpoint.Auth + "authenticate",
                new StringContent(JsonConvert.SerializeObject(new { username = AuthCredential.Username, password = AuthCredential.Password }), Encoding.UTF8, "application/json"));
            var tokenResult = new BaseResponse<string>();
            if (response.IsSuccessStatusCode)
            {
                tokenResult = JsonConvert.DeserializeObject<BaseResponse<string>>(await response.Content.ReadAsStringAsync(), jsonSerializerSettings);
            }

            return tokenResult.data;
        }
    }

    public interface ICoreData
    {
        void SetCategoryCOA();
        void SetDivisionCOA();
        void SetUnitCOA();
        void SetPPhCOA();
        void SetBankAccount();
    }
}
