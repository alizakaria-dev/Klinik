namespace Shared.Models
{
    public class File
    {
        public int FILEID { get; set; }
        public int REL_KEY { get; set; }
        public long SIZE { get; set; }
        public string REL_TABLE { get; set; }
        public string REL_FIELD { get; set; }
        public string EXTENSION { get; set; }
        public string URL { get; set; }
    }
}
