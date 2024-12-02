using Business.Dtos.FormDto;
using Dapper;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services.FormService
{
    public class FormService : IFormService
    {
        private readonly IDapperDbConnection _dapperDbConnection;

        public FormService(IDapperDbConnection dapperDbConnection)
        {
            this._dapperDbConnection = dapperDbConnection;
        }
       

        public async Task<List<FormDynamicDto>> GetDynamicFormList()
        {
            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = @"SELECT * FROM DynamicForm";
                var data = await connection.QueryAsync<FormDynamicDto>(sql);

                if (data != null)
                {
                    return data.ToList();
                }
                else
                {
                    return new List<FormDynamicDto>();
                }
            }
        }
    }
}
