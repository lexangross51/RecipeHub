using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.Application.Products.Commands.CreateProduct;
using RecipeHub.Application.Recipes.Commands.CreateRecipe;
using RecipeHub.WebServer.DtoModels.Recipes.Create;

namespace RecipeHub.WebServer.Controllers;

[ApiController]
[Route("api/v1/recipes")]
public class RecipeController(IMediator mediator, IMapper mapper, ILogger<RecipeController> logger) : ControllerBase
{
    public async Task<IActionResult> Create([FromForm] CreateRecipeDto dto)
    {
        try
        {
            var result = await mediator.Send(mapper.Map<CreateRecipeCommand>(dto));

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.First());
            }

            logger.LogInformation($"Рецепт \"{dto.Name}\" успешно добавлен");

            return Ok(new { Id = result.Value });
        }
        catch (Exception ex)
        {
            logger.LogError($"При добавлении рецепта \"{dto.Name}\" произошла ошибка: {ex.Message}");
            return BadRequest();
        }
    }
}