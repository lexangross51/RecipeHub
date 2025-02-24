const api = 'https://localhost:7244/api/v1/';

document.addEventListener("DOMContentLoaded", () => {
    attachEventListeners();
    getRecipes();
});

function attachEventListeners() {
    const search = document.getElementById('input-search');
    search.addEventListener('input', async (e) => {
        const inputName = e.target.value;
        await getRecipes(inputName);
    })
}

async function getRecipes(searchName = '', take = 4) {
    let link = api + `recipes?take=${take}`;

    if (searchName) {
        link += `&name=${searchName}`;
    }

    console.log(`Получаем список рецептов по адресу: ${link}`);
    document.getElementById('recipes-list').children.cle

    fetch(link, {
        method: 'GET'
    })
    .then(response => response.json())
    .then(recipes => {
        const recipeList = document.getElementById('recipes-list');
        const newRecipes = [];

        recipes.forEach(recipe => {
            const newRecipe = createRecipeCard(recipe);
            newRecipes.push(newRecipe);
        });

        recipeList.replaceChildren(...newRecipes);
    })
    .catch(error => {
        console.error(`Произошла ошибка: ${error.message}`);
    });
}

async function getRecipe(id) {
    fetch(api + `recipes/${id}`)
    .then(response => response.json())
    .then(recipe => {
        console.log(`Полученный рецепт: ${recipe.id}`);
    })
    .catch(error => {
        console.error(`Произошла ошибка: ${error.message}`);
    })
}

function createRecipeCard(recipe) {
    const recipeTemplate = document.getElementById('recipe-item-template');
    const ingredientTemplate = document.getElementById('ingredient-item-template');
    const newRecipe = recipeTemplate.content.cloneNode(true).firstElementChild;

    newRecipe.dataset.id = recipe.id;
    newRecipe.querySelector('.recipe-image').src = recipe.imageUrl
    newRecipe.querySelector('h3').innerText = recipe.name;
    newRecipe.querySelector('p').innerText = `Время приготовления: ${recipe.cookingTime}`;
    const ingredientList = newRecipe.querySelector('.ingredient-list');

    for (const ingredient of recipe.ingredients) {
        const newIngredient = ingredientTemplate.content.cloneNode(true).firstElementChild;
        newIngredient.innerText = ingredient;
        ingredientList.append(newIngredient);
    }
    
    newRecipe.addEventListener('click', selectRecipe);
    return newRecipe;
}

async function selectRecipe() {
    // this - элемент списка (li)
    const recipeId = this.dataset.id;
    window.location.href = `recipeDetails.html?id=${recipeId}`;
}