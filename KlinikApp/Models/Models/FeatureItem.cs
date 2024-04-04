namespace Shared.Models
{
    public class FeatureItem
    {
        public int FEATUREITEMID { get; set; }
        public int FEATUREID { get; set; }
        public string ICON { get; set; }
        public string TEXTONE { get; set; }
        public string TEXTTWO { get; set; }
        public Feature? FEATURE { get; set; }
    }
}
