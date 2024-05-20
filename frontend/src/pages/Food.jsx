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
    const [addSucces, setAddSucces] = useState(null);

    const [grams, setGrams] = useState(0);
    const [addToDayError, setAddToDayError] = useState(null);

    const [requestError, setRequestError] = useState(null);
    const [requestSuccess, setRequestSuccess] = useState(null);

    const formatDate = (date) => {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        return `${year}-${month}-${day}`;
    };

    const getAllFoods = async () => {
        setAllError(null);

        // Toggle showing all foods
        if (foods.length !== 0) {
            setFoods([]);
            return;
        }

        try {
            const token = localStorage.getItem('token');
            const response = await axios.get('https://localhost:7094/api/Food', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setFoods(response.data);
            console.log(response.data);
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
        setAddSucces(null);

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
            setName('');
            setCalories(0);
            setCarbohydrates(0);
            setProteins(0);
            setFats(0);
            setAddSucces('Food added successfully');
        } catch (error) {
            setAddError('Could not add food, please try again later');
            console.log(error);
        }
    };

    const addFoodToDay = async (foodNameDay, grams) => {
        if (grams <= 0) {
            setAddToDayError('Please enter a value greater than 0 for grams.');
            return;
        }

        try {
            const token = localStorage.getItem('token');
            const response = await axios.put('https://localhost:7094/api/Days/add-food', {
                date: formatDate(new Date()),
                foodName: foodNameDay,
                grams: grams
            },
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            setGrams(0);
            console.log(response.status);
        } 
        catch (error) {
            console.log(error);
        }
    }

    const createPublicRequest = async (foodNameRequest) => {
        setRequestError(null);
        try {
            const response = await axios.post(`https://localhost:7094/api/Request/${foodNameRequest}`, {}, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setRequestSuccess(response.data);
            setTimeout(() => {
                setRequestSuccess(null);
            }, 3000);
            console.log(response.status);
        } 
        catch (error) {
            setRequestError(error.response.data);
            console.log(error);
        }
    }

    return (
        <div>
            <Header page='food'/>
            <div className="item-container">
                <h2>See All</h2>
                <button onClick={getAllFoods}>See All</button>

                <div>
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
                </div>
                
                {allError && <p className="error">{allError}</p>}

                <h2>Search Food</h2>
                <input className="search-item-input" type="text" placeholder="Search food" 
                    value={foodName} onChange={(event) => {setFoodName(event.target.value)}} />
                <button className="search-item-button" onClick={getByName}>Search</button>

                {food && <FoodItem 
                    key={food.name}
                    name={food.name}
                    calories={food.calories}
                    carbohydrates={food.carbohydrates}
                    fats={food.fats}
                    proteins={food.proteins}   
                />}

                {food && <div>
                    <label htmlFor="grams">Grams</label>
                    <input id="grams" type="number" placeholder="Grams"
                            value={grams} onChange={(event) => setGrams(event.target.value)} />
                    <button className="add-item-day" onClick={() => addFoodToDay(food.name, grams)}>Add to Day</button>
                </div>
                }
                {food && !food.public && <div>
                    <button onClick={() => createPublicRequest(food.name)}>Create Request To Make Food Public</button>
                </div>}

                {nameError && <p className="error">{nameError}</p>}
                {addToDayError && <p className="error">{addToDayError}</p>}
                {requestError && <p className="error">{requestError}</p>}
                {requestSuccess && <p className="success">{requestSuccess}</p>}

                <h2>Add Food</h2>
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

                    {addError && <p className="error">{addError}</p>}
                    {addSucces && <p className="success">{addSucces}</p>}
                    <button type="submit">Add Food</button>
                </form>
            </div>
        </div>
    );
}

export default Food;