using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RecipeHub.Application.Products.Commands.CreateProduct;
using RecipeHub.Application.Products.Queries.GetProduct;
using RecipeHub.Domain.Models;
using RecipeHub.WebServer.DtoModels.Products;

namespace RecipeHub.WebServer.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController(IMediator mediator, ILogger<ProductController> logger) : ControllerBase
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

			var productDto = new GetProductDto
			{
				Name = result.Value.Name,
				ImageUrl = Url.Action(nameof(ImageController.Get), "Image", new { id = result.Value.ImageId }, Request.Scheme)
			};

			logger.LogInformation($"Продукт с Id={id} успешно получен");

			return Ok(productDto);
		}
		catch (Exception ex)
		{
			logger.LogError($"При получении продукта с Id={id} произошла ошибка: {ex.Message}");
			return BadRequest();
		}
    }

	[HttpPost]
	public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
	{
        try
		{
			var createCommand = new CreateProductCommand { Name = dto.Name };
            MemoryStream? ms = default;

			if (dto.Image != null)
			{
				ms = new MemoryStream();
				var stream = dto.Image.OpenReadStream();
				await stream.CopyToAsync(ms).ConfigureAwait(false);

				createCommand.Image = ms != null ? new Image { Data = ms, Name = dto.Image.FileName } : default;
            }

			var result = await mediator.Send(createCommand);

			if (result.IsFailed)
			{
				return BadRequest(result.Errors.First());
			}

			logger.LogInformation($"Продукт \"{dto.Name}\" успешно добавлен");

			return Ok(new { Id = result.Value });
		}
		catch (Exception ex)
		{
            logger.LogError($"При добавлении продукта \"{dto.Name}\" произошла ошибка: {ex.Message}");
            return BadRequest();
        }
	}
}