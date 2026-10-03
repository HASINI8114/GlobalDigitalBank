using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Core.Infrastructure.Repositories.Contracts
{
    public interface IDataBaseConnectionManager
    {
        DbConnection GetConnection();
    }
}
