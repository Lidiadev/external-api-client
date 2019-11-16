﻿using RealEstate.Presentation.Models.Property;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Presentation.Services.Interfaces
{
    public interface IPropertyService
    {
        /// <summary>
        /// Gets the top real estate agents with the most properties.
        /// </summary>
        /// <param name="topElements">The number of the top agents.</param>
        /// <returns>The list of agents.</returns>
        Task<IReadOnlyCollection<PropertyViewModel>> GetTopAsync(int topElements);
    }
}
