import React, { useState } from "react";

import axios from "axios";

import Header from "../components/header/Header";
import FoodItem from "../components/food-item/FoodItem";

function Food() {
    const [foods, setFoods] = useState([]);
    const [allError, setAllError] = useState(null);

    const [foodName, setFoodName] = useState(''); 
    const [food, setFood] = useState(null);
    const [nameError, setNameError] = useState(null);

    const [name, setName] = useState('');
    const [calories, setCalories] = useState(0);
    const [carbohydrates, setCarbohydrates] = useState(0);
    const [fats, setFats] = useState(0);
    const [proteins, setProteins] = useState(0);
    const [addError, setAddError] = useState(null);

    const getAllFoods = async () => {
        setAllError(null);

        try {
            const token = localStorage.getItem('token');
            const response = await axios.get('https://localhost:7094/api/Food', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setFoods(response.data);
        } catch(error) {
            setAllError('No foods found');
            setFoods([]);
            console.log(error);
        }
    };

    const getByName = async () => {
        setNameError(null);

        try {
            const token = localStorage.getItem('token');
            const response = await axios.get(`https://localhost:7094/api/Food/${foodName}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            setFood(response.data);
        } catch (error) {
            setNameError('No food with this name');
            setFood(null);
            console.log(error);
        }
    }

    const addFood = async (event) => {
        event.preventDefault();
        setAddError(null);

        if (calories <= 0 || carbohydrates <= 0 || fats <= 0 || proteins <= 0) {
            setAddError('Please enter values greater than 0 for all fields.');
            return;
        }

        try {
            const token = localStorage.getItem('token');
            const response = await axios.post('https://localhost:7094/api/Food', {
                name: name,
                calories: calories,
                carbohydrates: carbohydrates,
                proteins: proteins,
                fats: fats
            },
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            console.log(response.status);
        } catch (error) {
            setAddError('Could not add food, please try again later');
            console.log(error);
        }
    };

    return (
        <div>
            <Header page='food'/>
            <button onClick={getAllFoods}>See All</button>

            {foods.map(food => {
                return (<FoodItem
                    key={food.name}
                    name={food.name}
                    calories={food.calories}
                    carbohydrates={food.carbohydrates}
                    fats={food.fats}
                    proteins={food.proteins}    
                />
                );
            })}    
            {allError && <p>{allError}</p>}

            <input type="text" placeholder="Search food" 
                   value={foodName} onChange={(event) => {setFoodName(event.target.value)}} />
            <button onClick={getByName}>Search</button>

            {food && <FoodItem 
                key={food.name}
                name={food.name}
                calories={food.calories}
                carbohydrates={food.carbohydrates}
                fats={food.fats}
                proteins={food.proteins}    
            />}
            {nameError && <p>{{nameError}}</p>}

            <form onSubmit={addFood}>
                <div>
                    <label htmlFor="food-name">Name</label>
                    <input type="text" id="food-name" 
                           value={name} onChange={(event) => setName(event.target.value)}
                           placeholder="Name" />
                </div>

                <div>
                    <label htmlFor="calories">Calories</label>
                    <input type="number" id="calories"
                           value={calories} onChange={(event) => setCalories(event.target.value)}
                           placeholder="Calories per 100g" />
                </div>

                
                <div>
                    <label htmlFor="carbohydrates">Carbohydrates</label>
                    <input type="number" id="carbohydrates"
                           value={carbohydrates} onChange={(event) => setCarbohydrates(event.target.value)}
                           placeholder="Carbohydrates per 100g" />
                </div>

                <div>
                    <label htmlFor="fats">Fats</label>
                    <input type="number" id="fats"
                           value={fats} onChange={(event) => setFats(event.target.value)}
                           placeholder="Fats per 100g" />
                </div>

                <div>
                    <label htmlFor="proteins">Proteins</label>
                    <input type="number" id="proteins"
                           value={proteins} onChange={(event) => setProteins(event.target.value)}
                           placeholder="Proteins per 100g" />
                </div>

                {addError && <p>{addError}</p>}
                <button type="submit">Add Food</button>
            </form>
        </div>
    );
}

export default Food;