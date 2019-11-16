using RealEstate.Domain.Dtos;
using RealEstate.Presentation.Models.Property;
using System;
using System.Linq;

namespace RealEstate.Presentation.Common.Extensions
{
    /// <summary>
    /// Extensions to map a DTO to a Model.
    /// </summary>
    public static class PropertyMapperExtensions
    {
        /// <summary>
        /// Maps the DTO to the View Model.
        /// </summary>
        /// <param name="dto">The model.</param>
        /// <returns>The <see cref="PropertyViewModel"/>.</returns>
        public static PropertyViewModel ToViewModel(this IGrouping<int, PropertyDto> dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new PropertyViewModel
            {
                RealEstateId = dto.Key,
                RealEstateAgentName = dto.First().RealEstateAgentName,
                Count = dto.Count()
            };
        }
    }
}
