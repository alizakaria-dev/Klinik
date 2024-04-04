namespace Shared.Models
{
    public class Feature
    {
        public int FEATUREID { get; set; }
        public string TITLE { get; set; }
        public string TEXT { get; set; }
        public List<FeatureItem>? FEATUREITEMS { get; set; }
    }
}
