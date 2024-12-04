import React, { useState } from 'react';
import axios from 'axios';

// Recipe component for admin page
function AdminRecipe() {
    // Create recipe
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [createError, setCreateError] = useState('');
    const [createSuccess, setCreateSuccess] = useState('');

    // Add/ update food to recipe
    const [recipeName, setRecipeName] = useState('');
    const [foodName, setFoodName] = useState('');
    const [grams, setGrams] = useState('');
    const [addFoodError, setAddFoodError] = useState('');
    const [addFoodSuccess, setAddFoodSuccess] = useState('');

    // Remove food from recipe
    const [recipeName2, setRecipeName2] = useState('');
    const [foodName2, setFoodName2] = useState('');
    const [removeFoodError, setRemoveFoodError] = useState('');
    const [removeFoodSuccess, setRemoveFoodSuccess] = useState('');

    // Delete recipe by name
    const [recipeName3, setRecipeName3] = useState('');
    const [deleteRecipeError, setDeleteRecipeError] = useState('');
    const [deleteRecipeSuccess, setDeleteRecipeSuccess] = useState('');

    // Get recipes
    const [recipes, setRecipes] = useState([]);

    // Create recipe
    const createRecipe = async (event) => {
        event.preventDefault();
        setCreateError('');

        if (!name || !description) {
            setCreateError('Name and description must not be empty');
            setTimeout(() => {
                setCreateError('');
            }, 3000);
            return;
        }

        try {
            const response = await axios.post('https://localhost:7094/api/Recipe', {
                name: name,
                description: description
            }, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setCreateSuccess(response.data);
            setTimeout(() => {
                setCreateSuccess('');
            }, 3000);
            setName('');
            setDescription('');
        } 
        catch (error) {
            console.log(error);
            if (error.response) {
                console.log("Error creating recipe: ", error.response.data);
                setCreateError(error.response.data);
                setTimeout(() => {
                    setCreateError('');
                }, 3000);
            }
        }
    }

    // Add food to recipe
    const addFoodToRecipe = async (event) => {
        event.preventDefault();
        setAddFoodError('');

        if (!recipeName || !foodName || !grams) {
            setAddFoodError('All fields must be filled');
            setTimeout(() => {
                setAddFoodError('');
            }, 3000);
            return;
        }

        try {
            const response = await axios.post(`https://localhost:7094/api/RecipeFood/${recipeName}/${foodName}/${grams}`, {}, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setAddFoodSuccess(response.data);
            setTimeout(() => {
                setAddFoodSuccess('');
            }, 3000);
            setRecipeName('');
            setFoodName('');
            setGrams('');
        }
        catch (error) {
            console.log(error);
            if (error.response) {
                console.log("Error adding food to recipe: ", error.response.data);
                setAddFoodError(error.response.data);
                setTimeout(() => {
                    setAddFoodError('');
                }, 3000);
            }
        }
    }

    const removeFoodFromRecipe = async (event) => {
        event.preventDefault();
        setRemoveFoodError('');

        if (!recipeName2 || !foodName2) {
            setRemoveFoodError('All fields must be filled');
            setTimeout(() => {
                setRemoveFoodError('');
            }, 3000);
            return;
        }

        try {
            const response = await axios.delete('https://localhost:7094/api/RecipeFood', {
                params: {
                    recipeName: recipeName2,
                    foodName: foodName2,
                },
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setRemoveFoodSuccess(response.data);
            setTimeout(() => {
                setRemoveFoodSuccess('');
            }, 3000);
            setRecipeName2('');
            setFoodName2('');
        }
        catch (error) {
            console.log(error);
            if (error.response) {
                console.log("Error removing food from recipe: ", error.response.data);
                setRemoveFoodError(error.response.data);
                setTimeout(() => {
                    setRemoveFoodError('');
                }, 3000);
            }
        }
    }

    const deleteRecipe = async (event) => {
        event.preventDefault();

        if (!recipeName3) {
            setDeleteRecipeError('This field must not be empty');
            setTimeout(() => {
                setDeleteRecipeError('');
            }, 3000);
            return;   
        }

        try {
            const response = await axios.delete(`https://localhost:7094/api/Recipe/${recipeName3}`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setDeleteRecipeSuccess(response.data);
            setTimeout(() => {
                setDeleteRecipeSuccess('');
            }, 3000);
            setRecipeName3('');
        }
        catch (error) {
            console.log(error);
            if (error.response) {
                console.log("Error deleting recipe: ", error.response.data);
                setDeleteRecipeError(error.response.data);
                setTimeout(() => {
                    setDeleteRecipeError('');
                }, 3000);
            }
        }
    }

    const getRecipes = async (event) => {
        if (recipes.length !== 0) {
            setRecipes([]);
            return;
        }

        try {
            const response = await axios.get('https://localhost:7094/api/Recipe/for-admin', {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });

            for (let i = 0; i < response.data.length; i++) {
                for (let j = 0; j < response.data[i].recipeFoods.length; j++) {
                    let foodId = response.data[i].recipeFoods[j].foodId;
                    try {
                        const foodResponse = await axios.get(`https://localhost:7094/api/Food/by-id/${foodId}`, {
                            headers: {
                                Authorization: `Bearer ${localStorage.getItem('token')}`
                            }
                        });

                        response.data[i].recipeFoods[j].food = foodResponse.data.name;
                    } catch (error) {
                        console.log(error);
                    }
                }
            }

            setRecipes(response.data);
        } 
        catch (error) {
            setRecipes([]);
            console.log(error);
        }
    }

    return (
        <div className='edit-container'>
            <h1>Edit Recipes</h1>

            <div id='add-recipe-admin' className='form-container'>
                <h3>Add Recipe</h3>
                <form onSubmit={createRecipe}>
                    <div>
                        <label htmlFor='recipe-name'>Name</label>
                        <input placeholder='Name' type='text' id='recipe-name' value={name} onChange={(e) => setName(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor='recipe-description'>Description</label>
                        <input placeholder='Description' id='recipe-description' value={description} onChange={(e) => setDescription(e.target.value)} />
                    </div>
                    <button type='submit'>Create Recipe</button>
                </form>
                {createError && <p className='error'>{createError}</p>}
                {createSuccess && <p className='success'>{createSuccess}</p>}
            </div>

            <div id='add-food-to-recipe-admin' className='form-container'>
                <h3>Add Food to Recipe</h3>
                <form onSubmit={addFoodToRecipe}>
                    <div>
                        <label htmlFor='recipe-name'>Recipe Name</label>
                        <input placeholder='Recipe Name' type='text' id='recipe-name' value={recipeName} onChange={(e) => setRecipeName(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor='food-name'>Food Name</label>
                        <input placeholder='Food Name' type='text' id='food-name' value={foodName} onChange={(e) => setFoodName(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor='grams'>Grams</label>
                        <input placeholder='Grams' type='number' id='grams' value={grams} onChange={(e) => setGrams(e.target.value)} />
                    </div>
                    <button type='submit'>Add Food to Recipe</button>
                </form>
                {addFoodError && <p className='error'>{addFoodError}</p>}
                {addFoodSuccess && <p className='success'>{addFoodSuccess}</p>}
            </div>

            <div id='remove-food-from-recipe-admin' className='form-container'>
                <h3>Remove Food from Recipe</h3>
                <form onSubmit={removeFoodFromRecipe}>
                    <div>
                        <label htmlFor='recipe-name2'>Recipe Name</label>
                        <input placeholder='Recipe Name' type='text' id='recipe-name2' value={recipeName2} onChange={(e) => setRecipeName2(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor='food-name2'>Food Name</label>
                        <input placeholder='Food Name' type='text' id='food-name2' value={foodName2} onChange={(e) => setFoodName2(e.target.value)} />
                    </div>
                    
                    <button type='submit'>Remove Food from Recipe</button>
                </form>
                {removeFoodError && <p className='error'>{removeFoodError}</p>}
                {removeFoodSuccess && <p className='success'>{removeFoodSuccess}</p>}
            </div>

            <div id='delete-recipe-admin' className='form-container'>
                <h3>Delete Recipe</h3>
                <form onSubmit={deleteRecipe}>
                    <div>
                        <label htmlFor='recipe-name3'>Recipe Name</label>
                        <input placeholder='Recipe Name' type='text' id='recipe-name3' value={recipeName3} onChange={(e) => setRecipeName3(e.target.value)} />
                    </div>
                    
                    <button type='submit'>Delete Recipe</button>
                </form>
                {deleteRecipeError && <p className='error'>{deleteRecipeError}</p>}
                {deleteRecipeSuccess && <p className='success'>{deleteRecipeSuccess}</p>}
            </div>

            <button onClick={getRecipes}>Get Recipes</button>
            <div id="admin-recipes">
                {recipes && recipes.map(recipe => {
                    return (
                        <div key={recipe.id}>
                            <h3>Name: {recipe.name}</h3>
                            <p>Id: {recipe.id}</p>
                            <p>Description: {recipe.description}</p>
                            <p>Foods</p>
                            {recipe.recipeFoods && recipe.recipeFoods.map(recipeFood => {
                                return (
                                    <p key={recipeFood.foodId}>{recipeFood.food}</p>
                                );
                            })}
                        </div>
                    );
                })}
            </div>
        </div>
    )
}

export default AdminRecipe;