using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealEstate.Presentation.Common.Constants;
using RealEstate.Presentation.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace RealEstate.Presentation.Controllers
{
    public class AgentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPropertyService _propertyService;

        public AgentController(ILogger<HomeController> logger,
            IPropertyService propertyService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _propertyService = propertyService ?? throw new ArgumentNullException(nameof(propertyService));
        }

        // GET: Property
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _propertyService.GetTopAsync(Constants.TopElements).ConfigureAwait(false));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}