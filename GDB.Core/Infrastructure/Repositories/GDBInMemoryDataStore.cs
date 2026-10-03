using System.Data;
using GDB.Core.Data;

namespace GDB.Core.Infrastructure.Repositories
{
    public static class GDBInMemoryDataStore
    {
        public static DataSet DataSet { get; } =
            GDBInMemoryDB.CreateDataSet();
    }
}
