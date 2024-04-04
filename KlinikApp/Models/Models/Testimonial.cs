namespace Shared.Models
{
    public class Testimonial
    {
        public int TESTIMONIALID { get; set; }
        public string USERNAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string PROFESSION { get; set; }
        public File? File { get; set; }
    }
}
