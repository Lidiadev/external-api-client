using System.ComponentModel.DataAnnotations;

namespace RealEstate.Presentation.Models.Property
{
    public class PropertyViewModel
    {
        [Display(Name = "Agent id")]
        public long RealEstateId { get; set; }

        [Display(Name = "Agent name")]
        public string RealEstateAgentName { get; set; }

        [Display(Name = "Number of properties")]
        public int Count { get; set; }
    }
}
