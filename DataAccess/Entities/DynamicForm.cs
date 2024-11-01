using System;

namespace DataAccess.Entities
{
    public class DynamicForm
    {
        public int FormID { get; set; }
        public string FormTitle { get; set; }
        public string FormName { get; set; }
        public string FormFile { get; set; }
        public string FormData { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
