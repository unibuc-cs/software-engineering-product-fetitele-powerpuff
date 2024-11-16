import React, { useState } from 'react';
import axios from 'axios';

// Recipe component for admin page
function AdminRecipe() {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [createError, setCreateError] = useState('');
    const [createSuccess, setCreateSuccess] = useState('');

    const [recipeName, setRecipeName] = useState('');
    const [foodName, setFoodName] = useState('');
    const [grams, setGrams] = useState('');
    const [addFoodError, setAddFoodError] = useState('');
    const [addFoodSuccess, setAddFoodSuccess] = useState('');

    // Create recipe
    const createRecipe = async (event) => {
        event.preventDefault();
        setCreateError('');

        if (!name || !description) {
            setCreateError('Name and description must not be empty');
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
            }
        }
    }

    // Add food to recipe
    const addFoodToRecipe = async (event) => {
        event.preventDefault();
        setAddFoodError('');

        if (!recipeName || !foodName || !grams) {
            setAddFoodError('All fields must be filled');
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
            }
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
        </div>
    )
}

export default AdminRecipe;