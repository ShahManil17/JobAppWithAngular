using System.ComponentModel.DataAnnotations;

namespace JobApplicationAPIs.Data.DataModel
{
    public class WorkExperience
    {
        [Key]
        public int Id { get; set; }

        public string? Company { get; set; }
        public string? Designation { get; set; }

        [RegularExpression(@"^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Invalid date or Format")]
        public DateOnly StartDate { get; set; }

        [RegularExpression(@"^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Invalid date or Format")]
        public DateOnly EndDate { get; set; }

        public int BasicDetailsId { get; set; }
        public virtual BasicDetails? BasicDetails { get; set; }
    }
}
