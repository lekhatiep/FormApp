namespace DataAccess.Entities
{
    public class Account
    {
        public int AccountID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
