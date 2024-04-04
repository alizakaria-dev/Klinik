namespace Shared.Models
{
    public class User
    {
        public int USERID { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public int? ROLEID { get; set; }
        public string ROLENAME { get; set; }
        public string? Token { get; set; }

    }
}
