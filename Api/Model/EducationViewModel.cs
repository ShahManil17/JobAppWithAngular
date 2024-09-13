using System.ComponentModel.DataAnnotations;

namespace JobApplicationAPIs.Model
{
    public class EducationViewModel
    {
        public string? EducationLevel { get; set; }

        [Display(Name = "Board/University Name")]
        public string? BoardName { get; set; }

        [StringLength(4, ErrorMessage = "Invalid Year Entered", MinimumLength = 4)]
        [Display(Name = "Passing Year")]
        public string? PassingYear { get; set; }

        [Range(0, 100, ErrorMessage = "Invalid Percentage Entered")]
        public Decimal Percentage { get; set; }
    }
}
