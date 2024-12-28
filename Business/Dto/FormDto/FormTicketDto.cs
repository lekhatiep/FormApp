using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dto.FormDto
{
    public class FormTicketDto
    {
        public string FormName { get; set; }
        public string FormData { get; set; }
        public string TicketData { get; set; }
        public int ActiveStep { get; set; }
    }
}
