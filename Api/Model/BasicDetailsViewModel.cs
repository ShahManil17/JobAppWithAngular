using JobApplicationAPIs.Data.DataModel;
using JobApplicationAPIs.Model.ResponseModels;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationAPIs.Model
{
    public class BasicDetailsViewModel 
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? PhoneNo { get; set; }

        public string? Gender { get; set; }

        public string? RelationshipStatus { get; set; }
    }

    public class BasicDetailsViewModelResponse : CustomeResponse
    {
        public List<BasicDetails> BasicDetails { get; set; }
    }
}
