using Business.Dto.FormDto;
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
        public async Task<int> CreateNewDynamicForm(FormDynamicDto formDynamic)
        {
            var rs = 1;
            try
            {
                using (var connection = _dapperDbConnection.CreateConnection())
                {
                    formDynamic.DateCreated = DateTime.Now;
                    string sql = @"INSERT INTO DynamicForm (FormTitle, FormName,FormFile,FormData,DateCreated) VALUES(@FormTitle, @FormName,@FormFile,@FormData,@DateCreated)";
                    rs = await connection.ExecuteAsync(sql, formDynamic);
                }
            }
            catch (Exception ex)
            {
                rs = 0;
                throw;
            }

            return rs;

        }

        public async Task<int> CreateFormLink(FormLinkDto formLink)
        {
            var rs = 1;
            try
            {
                using (var connection = _dapperDbConnection.CreateConnection())
                {
                    formLink.DateCreated = DateTime.Now;
                    string sql = @"INSERT INTO FormLink (FormLinkName, FormLinkDescription,FormLinkUrl,DateCreated) VALUES(@FormLinkName, @FormLinkDescription,@FormLinkUrl,@DateCreated)";
                    rs = await connection.ExecuteAsync(sql, formLink);
                }
            }
            catch (Exception ex)
            {
                rs = 0;
                throw;
            }

            return rs;
        }

        public async Task<List<FormLinkDto>> GetFormLinkList()
        {
            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = @"SELECT * FROM FormLink";
                var data = await connection.QueryAsync<FormLinkDto>(sql);

                if (data != null)
                {
                    return data.ToList();
                }
                else
                {
                    return new List<FormLinkDto>();
                }
            }
        }
    }
}
