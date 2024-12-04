using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business.Dto.TicketDto
{
    public class CreateTicketDto
    {
        [JsonPropertyName("stringifyData")]
        public string TicketData { get; set; }
        public int ActiveStep { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int FormID { get; set; }
        public int ProfileID { get; set; }
        public string UserID { get; set; }
    }
}
