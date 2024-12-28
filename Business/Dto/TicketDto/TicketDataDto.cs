using System.Text.Json.Serialization;

namespace Business.Dto.TicketDto
{
    public class TicketDataDto
    {
        [JsonPropertyName("ticket_data")]
        public string TicketData { get; set; }
        [JsonPropertyName("note")]
        public string Note { get; set; }
        [JsonPropertyName("username")]
        public string UserName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("active_step")]
        public int ActiveStep { get; set; }
        [JsonPropertyName("form_name")]
        public string FormName { get; set; }

        [JsonPropertyName("status_id")]
        public int StatusID { get; set; }

      
    }
}
