using JBOT.Domain.Entities;
using JBOT.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Application.Common.Interfaces
{
    public interface IApplicationDBContext
    {
        DbSet<UnitTest> UnitTests { get; set; }
        void Add<U>(BaseEntity<U> entity, string user = null);
        void BulkInsert<U>(IEnumerable<BaseEntity<U>> entity, string user = null);
        void Update<U>(BaseEntity<U> entity, string user = null);
        void BulkUpdate<U>(IEnumerable<BaseEntity<U>> entity, string user = null);
        void Delete<U>(BaseEntity<U> entity);
        Task<T> FindAsync<T>(int id) where T : class;
        Task<int> SaveChangeAsync();
    }
}
