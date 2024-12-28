using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dto.FormDto
{
    public class UpdateFormLinkDto
    {
        public int FormLinkID { get; set; }
        public string FormLinkName { get; set; }
        public string FormLinkDescription { get; set; }
        public string FormLinkUrl { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
