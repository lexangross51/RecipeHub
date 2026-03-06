using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RecipeHub.Application.Products.Commands.CreateProduct;
using RecipeHub.Application.Products.Commands.DeleteProduct;
using RecipeHub.Application.Products.Commands.UpdateProduct;
using RecipeHub.Application.Products.Queries.GetProduct;
using RecipeHub.WebServer.DtoModels.Products;

namespace RecipeHub.WebServer.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController(IMediator mediator, ILogger<ProductController> logger, IMapper mapper) : ControllerBase
{
	[HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
		try
		{
			var result = await mediator.Send(new GetProductQuery { Id = id });

			if (result.IsFailed)
			{
				return BadRequest(result.Errors.First());
			}

			var productDto = mapper.Map<GetProductDto>(result.Value);
			productDto.ImageUrl = Url.Action(nameof(ImageController.Get), "Image", new { id = result.Value.ImageId }, Request.Scheme);

			logger.LogInformation("Продукт с Id={id} успешно получен", id);
			return Ok(productDto);
		}
		catch (Exception ex)
		{
			logger.LogError("При получении продукта с Id={id} произошла ошибка: {Message}", id, ex.Message);
			return BadRequest();
		}
    }

	[HttpPost]
	public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
	{
        try
		{
			var createCommand = mapper.Map<CreateProductCommand>(dto);
			var result = await mediator.Send(createCommand);

			if (result.IsFailed)
			{
				return BadRequest(result.Errors.First());
			}

			logger.LogInformation("Продукт \"{Name}\" успешно добавлен", dto.Name);

			return Ok(new { Id = result.Value });
		}
		catch (Exception ex)
		{
            logger.LogError("При добавлении продукта \"{Name}\" произошла ошибка: {Message}", dto.Name, ex.Message);
            return BadRequest();
        }
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(string id)
	{
        try
        {
            var result = await mediator.Send(new DeleteProductCommand { Id = id });

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.First());
            }

            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("При удалении продукта с id = {id} произошла ошибка: {Message}", id, ex.Message);
            return BadRequest(new { error = ex.Message });
        }
    }

	[HttpPut]
	public async Task<IActionResult> Update([FromForm]UpdateProductDto dto)
	{
        try
        {
            var updateCommand = mapper.Map<UpdateProductCommand>(dto);
            var result = await mediator.Send(updateCommand);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.First());
            }

            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("При обновлении проудкта с id = {id} произошла ошибка {message}", dto.Id, ex.Message);
            return BadRequest(new { error = ex.Message });
        }
    }
}