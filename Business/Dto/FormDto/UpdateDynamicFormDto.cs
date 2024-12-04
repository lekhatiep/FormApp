using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business.Dto.FormDto
{
    public class UpdateDynamicFormDto
    {
        [JsonPropertyName("formId")]
        public int FormID { get; set; }
        [JsonPropertyName("formTitle")]
        public string FormTitle { get; set; }
        [JsonPropertyName("formUpdateName")]
        public string FormName { get; set; }
        [JsonPropertyName("formUpdateFile")]
        public string FormFile { get; set; }
        [JsonPropertyName("formUpdateData")]
        public string FormData { get; set; }
        [JsonPropertyName("formattedDate")]
        public DateTime DateCreated { get; set; }
    }
}
