using Com.DanLiris.Service.EmergencyAttendance.Lib.Models;
using Com.Moonlay.Models;
using EWorkplaceAbsensiService.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib
{
    public class AttendanceDbContext : DbContext
    {
        public AttendanceDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                Database.SetCommandTimeout(1000 * 60 * 20);
        }
        public DbSet<AttendanceModel> Attendances { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<UnitModel> Units { get; set; }
        public DbSet<SectionModel> Sections { get; set; }
        public DbSet<GroupModel> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureEntities(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private void ConfigureEntities(ModelBuilder modelBuilder)
        {
            Type baseType = typeof(IStandardEntity);
            foreach (IStandardEntity item in from typeInfo in GetType().Assembly.DefinedTypes
                                             where baseType.IsAssignableFrom(typeInfo) && !typeInfo.IsAbstract && typeInfo.IsClass
                                             select typeInfo into info
                                             select Activator.CreateInstance(info) as IStandardEntity)
            {
                EntityTypeBuilder builder = modelBuilder.Entity(item.GetType());
                ConfigureProperties(builder);
                ConfigureQueryFilter(builder);
            }
        }
        private void ConfigureProperties(EntityTypeBuilder builder)
        {
            builder.Property("LastModifiedBy").IsRequired().HasMaxLength(255);
            builder.Property("LastModifiedAgent").IsRequired().HasMaxLength(255);
            builder.Property("CreatedBy").IsRequired().HasMaxLength(255);
            builder.Property("CreatedAgent").IsRequired().HasMaxLength(255);
            builder.Property("DeletedBy").IsRequired().HasMaxLength(255);
            builder.Property("DeletedAgent").IsRequired().HasMaxLength(255);
        }
        private static void ConfigureQueryFilter(EntityTypeBuilder builder)
        {
            ParameterExpression parameterExpression = Expression.Parameter(((ITypeBase)builder.Metadata).ClrType, "IsDeleted");
            MemberExpression left = Expression.Property(parameterExpression, "IsDeleted");
            ConstantExpression right = Expression.Constant(false);
            BinaryExpression body = Expression.Equal(left, right);
            builder.HasQueryFilter(Expression.Lambda(body, parameterExpression));
        }
    }
}
