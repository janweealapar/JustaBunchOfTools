using JBOT.Domain.Entities;
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
        Task<int> SaveChangeAsync();
    }
}
