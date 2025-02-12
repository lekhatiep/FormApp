using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business.Dto.TicketDto
{
    public class UpdateTicketDto
    {
        public int TicketID { get; set; }
        [JsonPropertyName("date_created")]
        public DateTime DateCreated { get; set; }
        [JsonPropertyName("date_approved")]
        public DateTime DateApproved { get; set; }
        [JsonPropertyName("note")]
        public string Note { get; set; }
        [JsonPropertyName("active_step")]
        public int ActiveStep { get; set; }
        [JsonPropertyName("ticket_data")]
        public string TicketData { get; set; }
    }
}
