const api = 'https://localhost:7244/api/v1/';

document.addEventListener('DOMContentLoaded', async () => {
    var queryParams = new URLSearchParams(window.location.search);
    const recipeId = queryParams.get('id')/*'91367195-a34d-4023-a776-bf3f1c5a7ecd'*/;
    await getRecipe(recipeId);
});

async function getRecipe(id) {
    fetch(api + `recipes/${id}`)
    .then(response => response.json())
    .then(recipe => {
        fillElementsWithRecipeData(recipe);
    });
}

function fillElementsWithRecipeData(recipe) {
    console.log(recipe);
    
    const recipeImage = document.getElementById('recipe-image');
    setDefaultImageWhenError(recipeImage, recipe.imageUrl);
    
    document.getElementById('recipe-name').innerText = recipe.name;
    const ingredientList = document.getElementById('ingredients-list');
    const stepsList = document.getElementById('steps-list');
    const recipeDesc = document.getElementById('recipe-description-block');
    const desc = recipeDesc.querySelector('#recipe-description');
    const cookTime = recipeDesc.querySelector('#recipe-cooking-time');
    
    if (!desc && !cookTime) {
        recipeDesc.display = 'none';
    } else {
        if (recipe.description) {
            desc.innerText = recipe.description;
        }

        if (recipe.cookingTime) {
            cookTime.innerText = `Время приготовления: ${recipe.cookingTime}`;
        }
    }

    // добавляем инфу об ингредиентах
    for (const ingredient of recipe.ingredients) {
        ingredientList.append(createIngredientItem(ingredient));
    }

    // добавляем инфу о шагах приготовления
    if (recipe.steps.length == 1) {
        stepsList.append(createRecipeStepItem(recipe.steps[0], true));
    } else {
        for (const step of recipe.steps) {
            stepsList.append(createRecipeStepItem(step));
        }
    }
}

function createIngredientItem(ingredient) {
    const ingredientItemTemplate = document.getElementById('ingredient-item-template');
    const ingredientElem = ingredientItemTemplate.content.cloneNode(true).firstElementChild;
    ingredientElem.querySelector('.product-name').innerText = ingredient.name;
    ingredientElem.querySelector('.product-measure').innerText = `${ingredient.measure.value} ${ingredient.measure.unit}`;

    return ingredientElem;
}

function createRecipeStepItem(step, isOne = false) {
    const stepItemTemplate = document.getElementById('step-item-template');
    const stepElem = stepItemTemplate.content.cloneNode(true).firstElementChild;
    const stepNumber = stepElem.querySelector('.step-number');

    if (isOne) {
        stepNumber.display = 'none';
    } else {
        stepNumber.innerText = `Шаг ${step.number + 1}`;
    }

    stepElem.querySelector('.step-description').innerText = step.description;

    if (step.imageUrl) {
        const stepImage = stepElem.querySelector('.step-image');
        setDefaultImageWhenError(stepImage, step.imageUrl);
    }

    return stepElem;
}

function setDefaultImageWhenError(image, url) {
    image.addEventListener('error', function () {
        this.src = '../assets/images/none.jpg';
    });

    if (url) {
        image.src = url;
    }
}