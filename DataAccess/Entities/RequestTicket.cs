using System;

namespace DataAccess.Entities
{
    public class RequestTicket
    {
        public int TicketID { get; set; }
        public string TicketData { get; set; }
        public int ActiveStep { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateApproved { get; set; }
        public int FormID { get; set; }
        public int ProfileID { get; set; }
    }
}
