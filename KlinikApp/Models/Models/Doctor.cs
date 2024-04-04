namespace Shared.Models
{
    public class Doctor
    {
        public int DOCTORID { get; set; }
        public string NAME { get; set; }
        public string DEPARTMENT { get; set; }
        public string FACEBOOKLINK { get; set; }
        public string TWITTERLINK { get; set; }
        public string INSTAGRAMLINK { get; set; }
        public string DESCRIPTION { get; set; }
        public List<File>? Files { get; set; }
    }
}
