namespace JobApplicationAPIs.Data.DataModel
{
    public class Technologies
    {
        public int Id { get; set; }
        public string? TechnologyName { get; set; }
        public string? TechnologyLevel { get; set; }

        public int BasicDetailsId { get; set; }
        public virtual BasicDetails? BasicDetails { get; set; }
    }
}
