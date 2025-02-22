using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.Application.Recipes.Commands.CreateRecipe;
using RecipeHub.Application.Recipes.Commands.DeleteRecipe;
using RecipeHub.Application.Recipes.Commands.UpdateRecipe;
using RecipeHub.Application.Recipes.Queries.GetRecipe;
using RecipeHub.Application.Recipes.Queries.GetRecipes;
using RecipeHub.WebServer.DtoModels.Recipes.Create;
using RecipeHub.WebServer.DtoModels.Recipes.Get;
using RecipeHub.WebServer.DtoModels.Recipes.Update;

namespace RecipeHub.WebServer.Controllers;

[ApiController]
[Route("api/v1/recipes")]
public class RecipeController(IMediator mediator, IMapper mapper, ILogger<RecipeController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateRecipeDto dto)
    {
        try
        {
            var createCommand = mapper.Map<CreateRecipeCommand>(dto);
            var result = await mediator.Send(createCommand);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.First());
            }

            logger.LogInformation("Рецепт \"{Name}\" успешно добавлен", dto.Name);

            return Ok(new { Id = result.Value });
        }
        catch (Exception ex)
        {
            logger.LogError("При добавлении рецепта \"{Name}\" произошла ошибка: {Message}", dto.Name, ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var result = await mediator.Send(new GetRecipeQuery { Id = id });

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.First());
            }

            var recipe = result.Value;
            var recipeDto = mapper.Map<GetRecipeDto>(recipe);
            recipeDto.ImageUrl = Url.Action(nameof(ImageController.Get), "Image", new { id = recipe.RecipeImageId }, Request.Scheme);

            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                var step = recipe.Steps[i];
                var stepDto = recipeDto.Steps[i];
                stepDto.ImageUrl = Url.Action(nameof(ImageController.Get), "Image", new { id = step.ImageId }, Request.Scheme);
            }

            return Ok(recipeDto);
        }
        catch (Exception ex)
        {
            logger.LogError("При получении рецепта с id = {id} произошла ошибка: {Message}", id, ex.Message);
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] SortParameter[]? sortBy = null,
        [FromQuery] string? name = null,
        [FromQuery] string[]? ingredients = null,
        [FromQuery] int? take = null,
        [FromQuery] int? skip = null)
    {
        try
        {
            var result = await mediator.Send(new GetRecipesQuery
            {
                Specification = new RecipeSpecification(take, skip, sortBy, name, ingredients)
            });

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.First());
            }

            var recipeDtos = mapper.Map<List<GetRecipeListItemDto>>(result.Value.Recipes);

            for (int i = 0; i < result.Value.Recipes.Count; i++)
            {
                string? imageId = result.Value.Recipes[i].RecipeImageId;
                recipeDtos[i].ImageUrl = Url.Action(nameof(ImageController.Get), "Image", new { id = imageId }, Request.Scheme);
            }

            return Ok(recipeDtos);
        }
        catch (Exception ex)
        {
            logger.LogError("При получении списка рецептов произошла ошибка: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var result = await mediator.Send(new DeleteRecipeCommand { Id = id });

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.First());
            }

            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("При удалении рецепта с id = {id} произошла ошибка: {Message}", id, ex.Message);
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm]UpdateRecipeDto dto)
    {
        try
        {
            var updateCommand = mapper.Map<UpdateRecipeCommand>(dto);
            var result = await mediator.Send(updateCommand);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.First());
            }

            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("При обновлении рецепта с id = {id} произошла ошибка {message}", dto.Id, ex.Message);
            return BadRequest(new { error = ex.Message });
        }
    }
}