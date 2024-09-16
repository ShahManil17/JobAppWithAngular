namespace JobApplicationAPIs.Model
{
    public class ShowAllViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }
        public string? Gender { get; set; }
        public string? RelationshipStatus { get; set; }
        public List<EducationViewModel>? education { get; set; }
        public List<WorkExperienceViewModel>? work { get; set; }
        public List<LanguageViewModel>? languages { get; set; }
        public List<TechnologiesViewModel>? technologies { get; set; }
        public List<PreferencesViewModel>? preferences { get; set; }

    }
}
