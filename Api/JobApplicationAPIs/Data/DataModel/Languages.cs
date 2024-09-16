using System.ComponentModel.DataAnnotations;

namespace JobApplicationAPIs.Data.DataModel
{
    public class Languages
    {
        [Key]
        public int Id { get; set; }
        public string? LanguageName { get; set; }
        public string? LanguageLevel { get; set; }

        public int BasicDetailsId { get; set; }
        public virtual BasicDetails? BasicDetails { get; set; }
    }
}
