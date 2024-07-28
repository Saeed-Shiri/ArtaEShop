using Catalog.Application.Queries.GetAllBrands;
using Catalog.Application.Queries.GetAllTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllBrands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var response = await _mediator.Send(new GetAllBrandsQuery());
            return Ok(response);
        }

        [HttpGet("GetAllTypes")]
        public async Task<IActionResult> GetAllTypes()
        {
            var response = await _mediator.Send(new GetAllTypesQuery());
            return Ok(response);
        }
    }
}
