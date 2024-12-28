using System;
using System.Text.Json.Serialization;

namespace Business.Dto.FormDto
{
    public class FormDynamicDto
    {
        [JsonPropertyName("form_id")]
        public int FormID { get; set; }
        [JsonPropertyName("form_title")]
        public string FormTitle { get; set; }
        [JsonPropertyName("form_name")]
        public string FormName { get; set; }
        [JsonPropertyName("form_file")]
        public string FormFile { get; set; }
        [JsonPropertyName("form_data")]
        public string FormData { get; set; }
        [JsonPropertyName("date_created")]
        public DateTime DateCreated { get; set; }
    }
}
