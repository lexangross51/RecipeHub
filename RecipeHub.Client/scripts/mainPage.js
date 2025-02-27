const api = 'https://localhost:7244/api/v1/';

let state = {
    searchName: '',
    sortBy: [],
    take: 4,
}

document.addEventListener("DOMContentLoaded", () => {
    attachEventListeners();
    getRecipes(state);
});

function attachEventListeners() {
    // window.addEventListener('click', (e) => {
    //     const sortOptions = document.getElementById('sort-options');

    //     if (sortOptions.classList.contains('show')) {
    //         e.preventDefault();
    //         sortOptions.classList.toggle('toggle');
    //     }
    // });

    const search = document.getElementById('input-search');
    search.addEventListener('input', async (e) => {
        const inputName = e.target.value;
        state.searchName = inputName;
        await getRecipes(state);
    });

    const overlay = document.getElementById('overlay');
    const sortButton = document.getElementById('sort-button');
    const sortOptions = document.getElementById('sort-options');
    sortButton.addEventListener('click', () => {
        sortOptions.classList.toggle('show');
        overlay.style.display = sortOptions.classList.contains('show') ? 'block' : 'none';
    });

    overlay.addEventListener('click', () => {
        if (sortOptions.classList.contains('show')){
            sortOptions.classList.toggle('show');
        }

        if (filterOptions.classList.contains('show')) {
            filterOptions.classList.toggle('show');
        }

        overlay.style.display = 'none';
    });

    const sortRadioButtons = document.querySelectorAll('input[name="sort-field"]');
    sortRadioButtons.forEach(button => {
        button.addEventListener('change', async (e) => {
            const selectedSort = e.target.value;
            const options = selectedSort.split('-');

            state.sortBy = [{
                field: options[0],
                order: options[1]
            }];

            sortOptions.classList.toggle('show');
            await getRecipes(state);
        })
    })

    const filterButton = document.getElementById('filter-button');
    const filterOptions = document.getElementById('filter-options');
    filterButton.addEventListener('click', () => {
        filterOptions.classList.toggle('show');
        overlay.style.display = filterOptions.classList.contains('show') ? 'block' : 'none';
    })
}

async function getRecipes(getOptions) {
    let link = api + `recipes?take=${getOptions.take}`;

    if (getOptions.searchName) {
        link += `&name=${searchName}`;
    }

    if (getOptions.sortBy) {
        for (let i = 0; i < getOptions.sortBy.length; i++) {
            const sort = getOptions.sortBy[i];

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