using JBOT.Application.Common.Interfaces;
using JBOT.Application.Constants;
using JBOT.Application.Dtos;
using JBOT.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using JBOT.Domain.Entities.Enums;
using Newtonsoft.Json.Serialization;
using JBOT.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Common;

namespace JBOT.Infrastructure.Persistence
{
    public class ValidateDBContext : DbContext, IValidateDBContext
    {
        #region Constructor
        public ValidateDBContext(DbContextOptions<ValidateDBContext> options)
            : base(options)
        {
        }

        public ValidateDBContext()
        {
        }
        #endregion

        #region DbSets
        public DbSet<DatabaseDto> Databases { get; set; }
        public DbSet<TestableObjectDto> TestableObjects { get; set; }
        public DbSet<TestableObjectDetailsDto> TestableObjectDetails { get; set; }
        public DbSet<ParameterDto> Parameters { get; set; }

        #endregion

        #region Methods

        public Task<List<DatabaseDto>> DatabaseList => this.Databases.FromSqlRaw(SqlQueries.GetDatabasesQuery).ToListAsync();
        public Task<DatabaseDto> GetDatabaseById(int id) => 
            this.Databases.FromSqlRaw(string.Format(SqlQueries.GetDatabaseByIdQuery, id)).FirstOrDefaultAsync();
        public Task<List<TestableObjectDto>> TestableObjectList(string databaseName) => 
            this.TestableObjects.FromSqlRaw(string.Format(SqlQueries.GetTestableObjectsQuery,databaseName)).ToListAsync();
        public Task<TestableObjectDetailsDto> GetTestableObjectDetailsById(string databaseName, int objectId) => 
            this.TestableObjectDetails.FromSqlRaw(string.Format(SqlQueries.GetTestableObjectDetailsQuery,databaseName,objectId)).FirstOrDefaultAsync();
        public Task<List<ParameterDto>> GetParametersByDatabaseAndObjectId(string databaseName, int objectId) =>
            this.Parameters.FromSqlRaw(string.Format(SqlQueries.GetTestableObjectParametersQuery, databaseName, objectId)).ToListAsync();


        public async Task<List<TestableObjectDetailsDto>> RunUnitTest(List<TestableObjectDetailsDto> unitTests)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                foreach (var unitTest in unitTests)
                {
                    await ExecuteSingleUnitTest(unitTest, command);
                }
            }

            return unitTests;
        }
        public async Task<TestableObjectDetailsDto> RunUnitTest(TestableObjectDetailsDto unitTest)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                await ExecuteSingleUnitTest(unitTest, command);
            }
            return unitTest;
        }

        private async Task ExecuteSingleUnitTest(TestableObjectDetailsDto unitTest, DbCommand command)
        {
            try
            {
                string parameterString = string.Empty, result = string.Empty;
                SqlParameter[] parameterArray;

                command.Parameters.Clear();
                parameterString = unitTest.InputParameters.Select(p => p.Name).ToList().ConcatList(",");
                parameterArray = unitTest.InputParameters.Select(p => new SqlParameter
                {
                    ParameterName = p.Name,
                    Value = p.Value
                }).ToArray();
                command.Parameters.AddRange(parameterArray);


                if (unitTest.Type == ObjectType.ScalarFunction)
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"SELECT [{unitTest.DatabaseName}].{unitTest.Name}({parameterString})";
                    await this.Database.OpenConnectionAsync();
                    result = (await command.ExecuteScalarAsync()).ToString();
                }
                else if (unitTest.Type == ObjectType.Procedure)
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = $"[{unitTest.DatabaseName}].{unitTest.Name}";
                    command.Parameters.Insert(0, unitTest.OutputParameters.Where(p => p.Id == 0)
                        .Select(p => new SqlParameter
                        {
                            ParameterName = p.Name,
                            Direction = ParameterDirection.ReturnValue
                        }).First());
                    var outputParameterArray = unitTest.OutputParameters.Where(p => p.Id > 0)
                                                .Select(p => new SqlParameter
                                                {
                                                    ParameterName = p.Name,
                                                    Direction = ParameterDirection.Output,
                                                    Precision = (byte)p.Precision,
                                                    Scale = (byte)p.Scale,
                                                    Size = p.MaxLength
                                                }).ToArray();
                    command.Parameters.AddRange(outputParameterArray);
                    await this.Database.OpenConnectionAsync();
                    result = (await command.ExecuteScalarAsync())?.ToString() ?? string.Empty;
                    if (string.IsNullOrEmpty(result))
                    {
                        result = command.Parameters[unitTest.OutputParameters.First().Name].Value?.ToString() ?? string.Empty;
                    }
                }
                unitTest.OutputParameters.First(p => p.Id == 0).Actual = result;
                foreach (var item in unitTest.OutputParameters.Where(p => p.Id > 0))
                {
                    item.Actual = command.Parameters[item.Name].Value?.ToString();
                }

                unitTest.OutputParameters.Assert();
                unitTest.Status = unitTest.OutputParameters.Where(p => !p.IsSuccess ?? false).Any() ? StatusEnums.Failed : StatusEnums.Success;
                unitTest.ErrorTitle = $"{unitTest.Status?.GetEnumDescription() ?? string.Empty}.";
            }
            catch (Exception ex)
            {
                unitTest.Status = StatusEnums.Failed;
                unitTest.ErrorTitle = $"{unitTest.Status?.GetEnumDescription() ?? string.Empty}.";
                unitTest.ErrorMessage = ex.Message;
            }
        }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<DatabaseDto>();
            modelBuilder.Ignore<TestableObjectDto>();
            modelBuilder.Ignore<TestableObjectDetailsDto>();
            modelBuilder.Ignore<ParameterDto>();
            modelBuilder.Ignore<BaseParameterDto>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DatabaseDto>().HasNoKey().ToView($"vw_{nameof(Databases)}");
            modelBuilder.Entity<TestableObjectDto>().HasNoKey().ToView($"vw_{nameof(TestableObjects)}");
            modelBuilder.Entity<TestableObjectDetailsDto>().HasNoKey()
                .ToView($"vw_{nameof(TestableObjectDetails)}");
            modelBuilder.Entity<ParameterDto>()
                .Property(e => e.Precision)
                .HasConversion<byte>();
            modelBuilder.Entity<ParameterDto>()
                .Property(e => e.Scale)
                .HasConversion<byte>();
            modelBuilder.Entity<ParameterDto>()
                .Property(e => e.MaxLength)
                .HasConversion<Int16>();
            modelBuilder.Entity<ParameterDto>()
                 .HasNoKey()
                .ToView($"vw_{nameof(Parameters)}");

        }
    }
}
