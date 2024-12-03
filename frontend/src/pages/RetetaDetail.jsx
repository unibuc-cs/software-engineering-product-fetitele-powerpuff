import { useEffect, useState } from "react";
import { useLocation} from "react-router-dom";
import axios from "axios";
import FoodItem from "../components/food-item/FoodItem";
export default function RetetaDetail(){
    const location = useLocation();
    const  {retetaPass} = location.state || {}
    const [alimente,setAlimente] =useState([])
    async function getFoodDetail(foodId){
        try{
            const token = localStorage.getItem('token');
            const response = await axios.get(`https://localhost:7094/api/Food/by-id/${foodId}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            return response.data
        }
        catch(error){
            console.log(error)
        }
    }
    
    
    
    useEffect(()=>{
        async function getFoods(){
                const foods = retetaPass.recipeFoods
                let rezult = []
                for(const foodIter of foods){
                    const id = foodIter.foodId;
                    const food = await getFoodDetail(id)
                    rezult.push({food:food,grams:foodIter.grams})
                }
                setAlimente(rezult)
            }
        getFoods();

    },[])

    function spawnFoods(alimente){
        return alimente.map(food=>
        {
            const content=food.food;
            const gramaj = food.grams
            return(
                <li>
            <FoodItem {...content} name={`${content.name} (${gramaj} g)`}/>
            </li>
            )
        }
        )
    }

    return (
        <>
        <div className="card" style={{width:'50rem',margin:'auto',marginTop:'2rem',padding:'2rem'}}>
      <div className="card-body">
    <h2 className="display-2 text-center"  ><b>{retetaPass.name}</b></h2>
    <h2>Details:</h2>
    <p className="card-title" >{retetaPass.description}</p>
    <h2>Ingredients:</h2>
    <ul style={{listStyleType:"none"}}>
    {spawnFoods(alimente)}
    </ul>
  </div>  
  </div>
        </>
    )
}