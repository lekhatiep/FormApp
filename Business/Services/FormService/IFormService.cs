using Business.Dto.FormDto;
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
    }
}
