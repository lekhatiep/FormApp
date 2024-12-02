using Business.Dtos.FormDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.FormService
{
    public interface IFormService
    {
        Task<List<FormDynamicDto>> GetDynamicFormList();
    }
}
