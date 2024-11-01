using System;

namespace DataAccess.Entities
{
    public class FormLink
    {
        public int FormLinkID { get; set; }
        public string FormLinkName { get; set; }
        public string FormLinkDescription { get; set; }
        public string FormLinkUrl { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
