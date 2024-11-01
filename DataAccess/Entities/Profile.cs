namespace DataAccess.Entities
{
    public class Profile
    {
        public int ProfileID { get; set; }
        public int AccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
