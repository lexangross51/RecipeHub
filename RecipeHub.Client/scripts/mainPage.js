const api = 'https://localhost:7244/api/v1/';

document.addEventListener("DOMContentLoaded", () => {
    attachEventListeners();
    getRecipes({});
});

function attachEventListeners() {
    const search = document.getElementById('input-search');
    search.addEventListener('input', async (e) => {
        const inputName = e.target.value;
        await getRecipes({ searchName: inputName });
    })

    const sortSelect = document.getElementById('sort-field-select');
    sortSelect.addEventListener('change', async function() {
        const selectedSort = this.value;
        const options = selectedSort.split('-');
        const name = document.getElementById('input-search').value;
    
        await getRecipes({ searchName: name, sortBy: [{
            field: options[0],
            order: options[1]
        }]});
    })
}

async function getRecipes({ searchName = '', take = 4, sortBy }) {
    let link = api + `recipes?take=${take}`;

    if (searchName) {
        link += `&name=${searchName}`;
    }

    if (sortBy) {
        for (let i = 0; i < sortBy.length; i++) {
            const sort = sortBy[i];

            if (!sort) continue;
            
            link += `&sortBy[${i}].field=${sort.field}&sortBy[${i}].order=${sort.order}`
        }
    }
    
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

function createRecipeCard(recipe) {
    const recipeTemplate = document.getElementById('recipe-item-template');
    const ingredientTemplate = document.getElementById('ingredient-item-template');
    const newRecipe = recipeTemplate.content.cloneNode(true).firstElementChild;
    const recipeImage = newRecipe.querySelector('.recipe-image');
    recipeImage.addEventListener('error', function() {
        this.src = '../assets/images/none.jpg';
    });

    newRecipe.dataset.id = recipe.id;
    recipeImage.src = recipe.imageUrl;
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

function selectRecipe() {
    // this - элемент списка (li)
    const recipeId = this.dataset.id;
    window.location.href = `recipeDetails.html?id=${recipeId}`;
}