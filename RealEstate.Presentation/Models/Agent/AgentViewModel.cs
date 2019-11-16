using System.ComponentModel.DataAnnotations;

namespace RealEstate.Presentation.Models.Agent
{
    public class AgentViewModel
    {
        [Display(Name = "Agent id")]
        public long RealEstateAgentId { get; set; }

        [Display(Name = "Agent name")]
        public string RealEstateAgentName { get; set; }

        [Display(Name = "Number of properties")]
        public int Count { get; set; }
    }
}
