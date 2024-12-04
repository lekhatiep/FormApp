using Business.Dto.FormDto;
using Dapper;
using DataAccess.Entities;
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

        public async Task<DynamicForm> GetFormByID(int formID, bool defaultIfEmpty = false)
        {
            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = @"SELECT * FROM DynamicForm WHERE FormID = @FormID";
                var form = await connection.QueryFirstOrDefaultAsync<DynamicForm>(sql, new { formID});

                if (defaultIfEmpty)
                {
                    if (form == null)
                    {
                        return new DynamicForm();
                    }
                }

                return form;
            }
        }

        public async Task<int> UpdateDynamicForm(UpdateDynamicFormDto updateDynamicFormDto)
        {
            var rs = 0;

            var form = await GetFormByID(updateDynamicFormDto.FormID, true);

            if(form.FormID > 0)
            {
                try
                {
                    using (var connection = _dapperDbConnection.CreateConnection())
                    {
                        string sql = @"UPDATE DynamicForm
                                SET FormName = @FormName, 
                                    FormFile = @FormFile, 
                                    FormData = @FormData, 
                                    DateCreated = @DateCreated,
                                    FormTitle = @FormTitle
                                WHERE FormID = @FormID";
                        rs = await connection.ExecuteAsync(sql, new
                        {
                            FormID = updateDynamicFormDto.FormID,
                            FormName = updateDynamicFormDto.FormName,
                            FormFile = updateDynamicFormDto.FormFile,
                            FormData = updateDynamicFormDto.FormData,
                            FormTitle = updateDynamicFormDto.FormTitle,
                            DateCreated = DateTime.Now
                        });
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
                
            }

            return rs;
            
        }

        public async Task<int> DeleteDynamicForm(int formID)
        {
            var rs = 0;

            var form = await GetFormByID(formID, true);

            if (form.FormID > 0)
            {
                try
                {
                    using (var connection = _dapperDbConnection.CreateConnection())
                    {
                        string sql = @"DELETE DynamicForm
                                WHERE FormID = @FormID";
                        rs = await connection.ExecuteAsync(sql, new
                        {
                            FormID = formID
                        });
                    }

                }
                catch (Exception)
                {
                    throw;
                }

            }

            return rs;
        }


        public async Task<FormLink> GetFormLinkByID(int formLinkID, bool defaultIfEmpty = false)
        {
            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = @"SELECT * FROM FormLink WHERE FormLinkID = @formLinkID";
                var form = await connection.QueryFirstOrDefaultAsync<FormLink>(sql, new { formLinkID });

                if (defaultIfEmpty)
                {
                    if (form == null)
                    {
                        return new FormLink();
                    }
                }

                return form;
            }
        }

        public async Task<int> DeleteDynamicFormLink(int formLinkID)
        {
            var rs = 0;

            var form = await GetFormLinkByID(formLinkID, true);

            if (form.FormLinkID > 0)
            {
                try
                {
                    using (var connection = _dapperDbConnection.CreateConnection())
                    {
                        string sql = @"DELETE FormLink
                                WHERE FormLinkID = @FormLinkID";
                        rs = await connection.ExecuteAsync(sql, new
                        {
                            FormLinkID = formLinkID
                        });
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return rs;
        }

        public async Task<int> UpdateFormLink(UpdateFormLinkDto updateFormLinkDto)
        {
            var rs = 0;

            var form = await GetFormLinkByID(updateFormLinkDto.FormLinkID, true);

            if (form.FormLinkID > 0)
            {
                try
                {
                    using (var connection = _dapperDbConnection.CreateConnection())
                    {
                        string sql = @"UPDATE FormLink
                                SET FormLinkName = @FormLinkName, 
                                    FormLinkDescription = @FormLinkDescription, 
                                    FormLinkURL = @FormLinkURL, 
                                    DateCreated = @DateCreated
                                WHERE FormLinkID = @FormLinkID";
                        rs = await connection.ExecuteAsync(sql, new
                        {
                            FormLinkID = updateFormLinkDto.FormLinkID,
                            FormLinkName = updateFormLinkDto.FormLinkName,
                            FormLinkDescription = updateFormLinkDto.FormLinkDescription,
                            FormLinkURL = updateFormLinkDto.FormLinkUrl,
                            DateCreated = DateTime.Now
                        });
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }

            }

            return rs;
        }

        public async Task<FormTicketDto> GetDynamicFormInputsByID(int ticketID , bool defaultIfEmpty)
        {
            using (var connection = _dapperDbConnection.CreateConnection())
            {
                string sql = @"SELECT FormName, FormData,TicketData, ActiveStep 
                                FROM DynamicForm  df
                                INNER JOIN RequestTicket rt ON
                                df.FormID = rt.FormID 
                                WHERE rt.TicketID = @TicketID";
                var data = await connection.QueryFirstOrDefaultAsync<FormTicketDto>(sql, new { ticketID });

                if (defaultIfEmpty)
                {
                    if (data == null)
                    {
                        return new FormTicketDto();
                    }
                }

                return data;
            }
        }
    }
}
