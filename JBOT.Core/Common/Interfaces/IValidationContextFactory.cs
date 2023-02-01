using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Application.Common.Interfaces
{
    public interface IValidationContextFactory
    {
        IValidateDBContext CreateInstanceByServer(string server);
    }
}
