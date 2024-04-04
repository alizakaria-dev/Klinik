namespace Shared.Models
{
    public class Info
    {
        public int INFOID { get; set; }
        public int DOCTORS { get; set; }
        public int PATIENTS { get; set; }
        public int STAFF { get; set; }

        public List<File>? FILES { get; set; }
    }
}
