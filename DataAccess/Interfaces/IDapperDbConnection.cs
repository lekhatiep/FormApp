using System.Data;

namespace DataAccess.Interfaces
{
    public interface IDapperDbConnection
    {
        IDbConnection CreateConnection();
    }
}
