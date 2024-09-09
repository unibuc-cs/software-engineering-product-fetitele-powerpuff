import React, { useState } from "react";

import axios from "axios";

import Header from "../components/header/Header";
import FoodItem from "../components/food-item/FoodItem";

function Food() {
    // Get all foods
    const [foods, setFoods] = useState([]);
    const [gramsObj, setGramsObj] = useState({});
    const [allError, setAllError] = useState(null);

    // Get food by name
    const [foodName, setFoodName] = useState(''); 
    const [food, setFood] = useState(null);
    const [nameError, setNameError] = useState(null);

    // Create a new, private food
    const [name, setName] = useState('');
    const [calories, setCalories] = useState(0);
    const [carbohydrates, setCarbohydrates] = useState(0);
    const [fats, setFats] = useState(0);
    const [proteins, setProteins] = useState(0);
    const [addError, setAddError] = useState(null);
    const [addSucces, setAddSucces] = useState(null);

    // Add a food to a day
    const [grams, setGrams] = useState(0);
    const [addToDayError, setAddToDayError] = useState(null);

    // Make a request
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

        if (!foodName) {
            setNameError('No food with this name');
            return;
        }
        
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

        if(!name) {
            setAddError('Name must not be empty');
            return;
        }

        // Validate food data
        if (calories < 0 || carbohydrates < 0 || fats < 0 || proteins < 0) {
            setAddError('Please enter values greater or equal to 0 for all fields.');
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
            // Clear the form
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

    // Add a food to a day, grams must be positive
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

    // Add food to day, for the get all foods section
    const addFoodToDayObj = async (foodNameDay, grams) => {
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

            handleGramsObj(foodNameDay, 0);
            console.log(response.status);
        } 
        catch (error) {
            console.log(error);
        }
    }

    // Users can create requests to make their private foods public
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

    // For the get all foods section, update the 
    // grams value for a food
    const handleGramsObj = (foodName, value) => {
        setGramsObj({
            ...gramsObj,
            [foodName]: value
        });
    }

    return (
        <div>
            <Header page='food'/>
            <div className="item-container">
                <h2>See All</h2>
                <button onClick={getAllFoods}>See All</button>

                <div>
                    {foods.map(food => {
                        return (<div> 
                            <FoodItem
                                key={food.name}
                                name={food.name}
                                calories={food.calories}
                                carbohydrates={food.carbohydrates}
                                fats={food.fats}
                                proteins={food.proteins}
                            />
                            <input className="grams" type="number" placeholder="Grams"
                                value={gramsObj[food.name]} 
                                onChange={(event) => handleGramsObj(food.name, event.target.value)} 
                            />
                            <button className="add-item-day" 
                                onClick={() => addFoodToDayObj(food.name, gramsObj[food.name])}>Add to Day</button>
                        </div>
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
                    <input className="grams" type="number" placeholder="Grams"
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