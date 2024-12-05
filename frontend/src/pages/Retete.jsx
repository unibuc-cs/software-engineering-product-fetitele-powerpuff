import React, { useEffect, useState } from 'react';
import axios from 'axios';
import Header from '../components/header/Header';
import { RetetaDisplay } from '../components/retete/RetetaDisplay';
export default function ReteteSummary(){
    const [reteteArray, setReteteArray] = useState([]);
    const [errorFetch,setErrorFetch] = useState(false);

    const [recipeName, setRecipeName] = useState('');
    const [filteredRecipes, setFilteredRecipes] = useState([]);
    const [nameError, setNameError] = useState(null);

    const [filters, setFilters] = useState({});
    const [filteredRecipesByNutrients, setFilteredRecipesByNutrients] = useState([]);
    const [filterError, setFilterError] = useState(null);
    
        useEffect(() => {
        async function get_recipes() {
            try {
                const token = localStorage.getItem('token');
                const response = await axios.get('https://localhost:7094/api/Recipe', {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });
                
                setReteteArray(response.data);
             
            } catch (error) {
                setErrorFetch(true)
            }
        }

        get_recipes();
    },[]); 

    const getByName = async () => {
        setNameError(null);
        setFilteredRecipes([]);

        if (!recipeName) {
            setNameError('No recipe with this name');
            setTimeout(() => {
                setNameError(null);
            }, 3000);
            return;
        }

        try {
            const token = localStorage.getItem('token');
            const response = await axios.get(`https://localhost:7094/api/Recipe/filter`, {
                params: { name :recipeName },
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            setFilteredRecipes(response.data);
        } catch (error) {
            setNameError('No recipe with this name');
            setTimeout(() => {
                setNameError(null);
            }, 3000);
            setFilteredRecipes([]);
            console.log(error);
        }
    }

    const filterByNutrients = async () => {
        setFilterError(null);
        setFilteredRecipesByNutrients([]);

        try {
            const token = localStorage.getItem('token');
            const response = await axios.get(`https://localhost:7094/api/Recipe/filter-by-nutrients`, {
                params: {
                    minCalories: filters.minCalories,
                    maxCalories: filters.maxCalories,
                    minProteins: filters.minProteins,
                    maxProteins: filters.maxProteins,
                    minCarbs: filters.minCarbs,
                    maxCarbs: filters.maxCarbs,
                    minFats: filters.minFats,
                    maxFats: filters.maxFats,
                },
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            setFilteredRecipesByNutrients(response.data);
        } catch (error) {
            setFilterError(error.response.data);
            setTimeout(() => {
                setFilterError(null);
            }, 3000);
            setFilteredRecipesByNutrients([]);
            console.log(error);
        }
    }

    //to do pentru mai tarziu : daca tokenul expira, nu mai face query
    return (
        <div>
            <Header page='recipe'/>
            <div className="item-container recipe-container">
                <input className="search-item-input" type="text" placeholder="Search recipe" 
                        value={recipeName} onChange={(event) => {setRecipeName(event.target.value)}} />
                <button className="search-item-button small-button" onClick={getByName}>Search</button>

                {nameError && <p className='error'>{nameError}</p>}
                {filteredRecipes.length > 0 && filteredRecipes.map(recipe => 
                        <div key={recipe.name} ><RetetaDisplay reteta={recipe}></RetetaDisplay></div>)}

                <h2>Filter Recipes By Nutrients</h2>
                <div className='filter-nutrients-container'>
                    <div>
                        <label>Min Calories:</label>
                        <input className='min-max' type="number" 
                            value={filters.minCalories || ''} 
                            onChange={(e) => setFilters({...filters, minCalories: e.target.value})} />
                    </div>
                    <div>
                        <label>Max Calories:</label>
                        <input className='min-max' type="number" 
                            value={filters.maxCalories || ''} 
                            onChange={(e) => setFilters({...filters, maxCalories: e.target.value})} />
                    </div>
                    <div>
                        <label>Min Proteins:</label>
                        <input className='min-max' type="number" 
                            value={filters.minProteins || ''} 
                            onChange={(e) => setFilters({...filters, minProteins: e.target.value})} />
                    </div>
                    <div>
                        <label>Max Proteins:</label>
                        <input className='min-max' type="number" 
                            value={filters.maxProteins || ''} 
                            onChange={(e) => setFilters({...filters, maxProteins: e.target.value})} />
                    </div>
                    <div>
                        <label>Min Carbs:</label>
                        <input className='min-max' type="number" 
                            value={filters.minCarbs || ''} 
                            onChange={(e) => setFilters({...filters, minCarbs: e.target.value})} />
                    </div>
                    <div>
                        <label>Max Carbs:</label>
                        <input className='min-max' type="number" 
                            value={filters.maxCarbs || ''} 
                            onChange={(e) => setFilters({...filters, maxCarbs: e.target.value})} />
                    </div>
                    <div>
                        <label>Min Fats:</label>
                        <input className='min-max' type="number" 
                            value={filters.minFats || ''} 
                            onChange={(e) => setFilters({...filters, minFats: e.target.value})} />
                    </div>
                    <div>
                        <label>Max Fats:</label>
                        <input className='min-max' type="number" 
                            value={filters.maxFats || ''} 
                            onChange={(e) => setFilters({...filters, maxFats: e.target.value})} />
                    </div>
                </div>

                <button className="search-item-button small-button" onClick={filterByNutrients}>Filter</button>
                {filterError && <p className='error'>{filterError}</p>}
                {filteredRecipesByNutrients.length > 0 && filteredRecipesByNutrients.map(recipe => 
                        <div key={recipe.name} ><RetetaDisplay reteta={recipe}></RetetaDisplay></div>)}

                <h2>All Recipes</h2>
                {errorFetch && <p>Nu s-au gasit nici o reteta, incearca sa te logezi din nou</p>}
                {reteteArray.map(reteta=><div key={crypto.randomUUID()}><RetetaDisplay reteta={reteta}></RetetaDisplay></div>)}
            </div>
        </div>
    );
    

}

