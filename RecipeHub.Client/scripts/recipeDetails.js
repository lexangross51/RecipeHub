const api = 'https://localhost:7244/api/v1/';
const id = '6c71fcb5-5dca-4fa7-8b2a-6b5d5b1462e1';

document.addEventListener('DOMContentLoaded', async () => {
    var queryParams = new URLSearchParams(window.location.search);
    const recipeId = /*queryParams.get('id');*/id;
    await getRecipe(recipeId);
});

async function getRecipe(id) {
    fetch(api + `recipes/${id}`)
    .then(response => response.json())
    .then(recipe => {
        fillElementsWithRecipeData(recipe);
    })
}

function fillElementsWithRecipeData(recipe) {
    console.log(recipe);
    document.getElementById('recipe-name').innerText = recipe.name;
    const ingredientList = document.getElementById('ingredients-list');

    // добавляем инфу об ингредиентах
    for (const ingredient of recipe.ingredients) {
        ingredientList.append(createIngredientItem(ingredient));
    }

    // добавляем инфу о шагах приготовления
    for (const step of recipe.steps) {
        
    }
}

function createIngredientItem(ingredient) {
    const ingredientItemTemplate = document.getElementById('ingredient-item-template');
    const ingredientElem = ingredientItemTemplate.content.cloneNode(true).firstElementChild;
    ingredientElem.querySelector('.product-name').innerText = ingredient.name;
    ingredientElem.querySelector('.product-measure').innerText = `${ingredient.measure.value} ${ingredient.measure.unit}`;

    return ingredientElem;
}