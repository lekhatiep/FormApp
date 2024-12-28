using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business.Dto.TicketDto
{
    public class TicketDto
    {
        [JsonPropertyName("ticket_id")]
        public int TicketID { get; set; }
        [JsonPropertyName("form_name")]
        public string FormName { get; set; }
        [JsonPropertyName("date_created")]
        public DateTime DateCreated { get; set; }
        [JsonPropertyName("date_approved")]
        public DateTime DateApproved { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("status_id")]
        public int StatusID { get; set; }
        [JsonPropertyName("note")]
        public string Note { get; set; }
        [JsonPropertyName("active_step")]
        public int ActiveStep { get; set; }
        [JsonPropertyName("username")]
        public string UserName { get; set; }
    }
}
