using JBOT.Application.Common.Interfaces;
using JBOT.Application.Constants;
using JBOT.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Infrastructure.Persistence
{
    public class ValidateDBContext : DbContext, IValidateDBContext
    {
        #region Constructor
        public ValidateDBContext(DbContextOptions<ValidateDBContext> options)
            : base(options)
        {
        }
        #endregion

        #region DbSets
        public DbSet<DatabaseDto> Databases { get; set; }
        public DbSet<TestableObjectDto> TestableObjects { get; set; }
        #endregion

        #region Methods
        public Task<List<DatabaseDto>> DatabaseList => this.Databases.FromSqlRaw(SqlQueries.GetDatabasesQuery).ToListAsync();
        public Task<DatabaseDto> GetDatabaseById(int id) => 
            this.Databases.FromSqlRaw(string.Format(SqlQueries.GetDatabaseByIdQuery, id)).FirstOrDefaultAsync();
        public Task<List<TestableObjectDto>> TestableObjectList(string databaseName) => 
            this.TestableObjects.FromSqlRaw(string.Format(SqlQueries.GetTestableObjectsQuery,databaseName)).ToListAsync();
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<DatabaseDto>();
            modelBuilder.Ignore<TestableObjectDto>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DatabaseDto>().HasNoKey().ToView($"vw_{nameof(Databases)}");
            modelBuilder.Entity<TestableObjectDto>().HasNoKey().ToView($"vw_{nameof(TestableObjects)}");
        }
    }
}
