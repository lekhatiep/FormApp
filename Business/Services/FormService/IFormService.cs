using Business.Dto.FormDto;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.FormService
{
    public interface IFormService
    {
        Task<List<FormDynamicDto>> GetDynamicFormList();
        Task<int> CreateNewDynamicForm(FormDynamicDto formDynamicDto);
        Task<int> CreateFormLink(FormLinkDto formLink);
        Task<List<FormLinkDto>> GetFormLinkList();
        Task<DynamicForm> GetFormByID(int formID, bool defaultIfEmpty = false);
        Task<FormLink> GetFormLinkByID(int formlinkID, bool defaultIfEmpty = false);
        Task<int> UpdateDynamicForm(UpdateDynamicFormDto updateDynamicFormDto);
        Task<int> DeleteDynamicForm(int formID);
        Task<int> DeleteDynamicFormLink(int formLinkID);
        Task<int> UpdateFormLink(UpdateFormLinkDto updateFormLinkDto);
        Task<FormTicketDto> GetDynamicFormInputsByID(int ticketID, bool defaultIfEmpty);
        Task<FormTicketDto> GetFormRequestById(int ticketID);
    }
}

