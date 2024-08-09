using Catalog.Application.Commands.CreateProduct;
using Catalog.Application.Commands.DeleteProduct;
using Catalog.Application.Commands.UpdateProduct;
using Catalog.Application.Queries.GetAllBrands;
using Catalog.Application.Queries.GetAllProducts;
using Catalog.Application.Queries.GetAllTypes;
using Catalog.Application.Queries.GetProductById;
using Catalog.Application.Queries.GetProductdByName;
using Catalog.Application.Queries.GetProductsByBrand;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogController : ApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllBrands()
        {
            var response = await _mediator.Send(new GetAllBrandsQuery());
            return Ok(response);
        }

        [HttpGet("GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypeResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllTypes()
        {
            var response = await _mediator.Send(new GetAllTypesQuery());
            return Ok(response);
        }

        [HttpGet("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _mediator.Send(new GetAllProductsQuery());
            return Ok(response);
        }

        [HttpGet("[action]/{id}", Name = "GetProducById")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductById(string id)
        {
            var response = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(response);
        }

        [HttpGet("[action]/{productName}")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductsByName(string productName)
        {
            var response = await _mediator.Send(new GetProductsByNameQuery(productName));
            return Ok(response);
        }

        [HttpGet("[action]/{brandName}")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductsByBrandName(string brandName)
        {
            var response = await _mediator.Send(new GetProductsByBrandQuery(brandName));
            return Ok(response);
        }

        [HttpPut("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var response = await _mediator.Send(new DeleteProductByIdCommand(id));
            return Ok(response);
        }
    }
}
