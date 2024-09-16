using System.ComponentModel.DataAnnotations;

namespace JobApplicationAPIs.Data.DataModel
{
    public class Preferences
    {
        [Key]
        public int Id { get; set; }
        public string? Location { get; set; }

        [Range(3, 24, ErrorMessage = "Invalid Notice Period Entered")]
        public int Notice { get; set; }

        [Range(360000, 500000, ErrorMessage = "We Offer Between 360000 And 500000 Only..")]
        public int ExpectedCtc { get; set; }
        public int CurrentCtc { get; set; }
        public string? Department { get; set; }

        public int BasicDetailsId { get; set; }
        public virtual BasicDetails? BasicDetails { get; set; }
    }
}
