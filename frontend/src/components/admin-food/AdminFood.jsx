import React, { useState } from 'react';
import axios from 'axios';

// Food component for admin page
function AdminFood () {
    const [foods, setFoods] = useState([]);
    const [getError, setGetError] = useState('');

    const [name, setName] = useState('');
    const [calories, setCalories] = useState('');
    const [carbohydrates, setCarbohydrates] = useState('');
    const [proteins, setProteins] = useState('');
    const [fats, setFats] = useState('');
    const [createError, setCreateError] = useState('');
    const [createSuccess, setCreateSuccess] = useState('');

    // Get all foods
    const getFoods = async () => {
        setGetError('');
        // Toggle see all foods
        if (foods.length !== 0) {
            setFoods([]);
            return;
        }

        try {
            const response = await axios.get('https://localhost:7094/api/Food/for-admin', {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setFoods(response.data);
        } 
        catch (error) {
            setFoods([]);
            console.log(error);
            setGetError('No foods found');
        }
    }

    // Create public food
    const createFood = async (event) => {
        event.preventDefault();
        setCreateError('');
        try {
            const response = await axios.post('https://localhost:7094/api/Food', {
                name: name,
                calories: calories,
                carbohydrates: carbohydrates,
                proteins: proteins,
                fats: fats
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
            setCalories('');
            setCarbohydrates('');
            setProteins('');
            setFats('');
        } 
        catch (error) {
            console.log(error);
            if (error.response) {
                console.log("Error creating food: ", error.response.data);
                setCreateError(error.response.data);
            }
        }
    }

    return (
        <div className='edit-container'>
            <h1>Edit Food</h1>

            <div id="add-food-admin" className='form-container'>
                <h3>Add Food</h3>
                <form onSubmit={createFood}>
                    <div>
                        <label htmlFor="food-name">Name</label>
                        <input placeholder='Name' type="text" id="food-name" value={name} onChange={e => setName(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="food-calories">Calories</label>
                        <input placeholder='Calories' type="number" id="food-calories" value={calories} onChange={e => setCalories(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="food-carbohydrates">Carbohydrates</label>
                        <input placeholder='0' type="number" id="food-carbohydrates" value={carbohydrates} onChange={e => setCarbohydrates(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="food-proteins">Proteins</label>
                        <input placeholder='0' type="number" id="food-proteins" value={proteins} onChange={e => setProteins(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="food-fats">Fats</label>
                        <input placeholder='0' type="number" id="food-fats" value={fats} onChange={e => setFats(e.target.value)} />
                    </div>
                    <button type="submit">Create Food</button>
                    {createError && <p className='error'>{createError}</p>}
                    {createSuccess && <p className='success'>{createSuccess}</p>}
                </form>
            </div>

            <button onClick={getFoods}>Get Foods</button>
            <div id="admin-foods">
                {foods && foods.map(food => {
                    return (
                        <div key={food.id}>
                            <h3>{food.name}</h3>
                            <p>Id: {food.id}</p>
                            <p>Calories: {food.calories}</p>
                            <p>Carbohydrates: {food.carbohydrates}</p>
                            <p>Proteins: {food.proteins}</p>
                            <p>Fats: {food.fats}</p>
                            <p>Is Public: {food.public.toString()}</p>
                            {food.applicationUserId && <p>User: {food.applicationUserId}</p>}
                        </div>
                    );
                })}
                {getError && <p className='error'>{getError}</p>}
            </div>
        </div>
    );
}

export default AdminFood;