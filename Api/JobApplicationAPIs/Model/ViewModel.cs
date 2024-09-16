using JobApplicationAPIs.Data.DataModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationAPIs.Model
{
    public class ViewModel
    {
        [Required(ErrorMessage = "This Field Must Be Filled Out")]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "This Field Must Be Filled Out")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "This Field Must Be Filled Out")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "This Field Must Be Filled Out")]
        [StringLength(10, ErrorMessage = "Invalid Number Entered", MinimumLength = 10)]
        [Display(Name = "Contact Number")]
        public string? PhoneNo { get; set; }

        [Required(ErrorMessage = "This Field Must Be Filled Out")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "This Field Must Be Filled Out")]
        [Display(Name = "Relationship Status")]
        public string? RelationshipStatus { get; set; }

        public List<EducationDetails>? EducationDetails { get; set; }

        public List<string>? Company { get; set; }
        public List<string>? Designation { get; set; }

        public List<DateOnly>? From { get; set; }
        public List<DateOnly>? To { get; set; }

        public List<string>? TechName { get; set; }
        public string? PhpLevel { get; set; }
        public string? MysqlLevel { get; set; }
        public string? OracleLevel { get; set; }
        public string? LaravelLevel { get; set; }

        public List<string>? LangName { get; set; }
        public List<List<string>>? LangLevel { get; set; }

        public List<string>? Location { get; set; }

        [Range(3, 24, ErrorMessage = "Invalid Notice Period Entered")]
        public int Notice { get; set; }

        [Range(359999, 500001, ErrorMessage = "We Offer Between 360000 And 500000 Only..")]
        public int ExpectedCtc { get; set; }
        public int CurrentCtc { get; set; }
        public string? Department { get; set; }

        public string? delExp { get; set; }
    }
}
