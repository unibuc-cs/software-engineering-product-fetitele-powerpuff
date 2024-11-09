import React, { useState } from 'react';
import axios from 'axios';

// Recipe component for admin page
function AdminRecipe() {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [createError, setCreateError] = useState('');
    const [createSuccess, setCreateSuccess] = useState('');

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
        </div>
    )
}

export default AdminRecipe;