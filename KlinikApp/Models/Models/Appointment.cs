using System.Text.Json.Serialization;

namespace Shared.Models
{
    public class Appointment
    {
        public int APPOINTMENTID { get; set; }
        public string NAME { get; set; }
        public string EMAIL { get; set; }
        public string DOCTOR { get; set; }
        public string MOBILE { get; set; }
        public string DESCRIPTION { get; set; }
        public string DATE { get; set; }
        public TimeSpan TIME { get; set; }
    }
}
