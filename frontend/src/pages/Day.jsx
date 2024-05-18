import React, { useEffect } from "react";
import Header from "../components/header/Header";
import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Day() {
    const navigate = useNavigate();
    const [completeDay, setCompleteDay] = useState(null);
    const [date, setDate] = useState(new Date());

    const formatDate = (date) => {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        return `${year}-${month}-${day}`;
    };

    const isCurrentDate = formatDate(date) === formatDate(new Date());

    const getDay = async (dateString) => {
        const formattedDate = formatDate(new Date(dateString));
        try {
            const response = await axios.get(`https://localhost:7094/api/Days/by-date?date=${formattedDate}`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            return response.data;
        } 
        catch(error) {
            console.log(error);
        }
    };

    const getCompleteDay = async (date) => {
        try {
            const day = await getDay(date);
        
            const dayFoodsPromises = day.dayFoods.map(async (food) => {
                const response = await axios.get(`https://localhost:7094/api/Food/by-id/${food.foodId}`, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem('token')}`
                    }
                });
                return { ...food, details: response.data };
            });
        
            const dayActivitiesPromises = day.dayPhysicalActivities.map(async (activity) => {
                const response = await axios.get(`https://localhost:7094/api/PhysicalActivities/by-id/${activity.physicalActivityId}`, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem('token')}`
                    }
                });
                return { ...activity, details: response.data };
            });
        
            const dayFoods = await Promise.all(dayFoodsPromises);
            const dayActivities = await Promise.all(dayActivitiesPromises);
        
            setCompleteDay({ ...day, dayFoods, dayPhysicalActivities: dayActivities });
        }
        catch(error) {
            console.log(error);
        }
    };

    const deleteFood = async (date, foodName) => {
        try {
            const formattedDate = formatDate(date);
            const response = await axios.delete(`https://localhost:7094/api/Days/delete-food/${formattedDate}/${foodName}`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            console.log(response);
            getCompleteDay(date);
        }
        catch(error) {
            console.log(error);
        }
    };

    const deleteActivity = async (date, activityName) => {
        try {
            const formattedDate = formatDate(date);
            const response = await axios.delete(`https://localhost:7094/api/Days/delete-activity/${formattedDate}/${activityName}`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            console.log(response);
            getCompleteDay(date);
        }
        catch(error) {
            console.log(error);
        }
    };
    
    useEffect(() => {
        if (date) {
            getCompleteDay(date);
        }
    }, [date]);

    return (
        <div>
            <Header page='day'/>

            <div id="day">
                <h1>{formatDate(date)}</h1>
                <h2>Food</h2>
                {isCurrentDate && <button onClick={() => navigate('/food')}>Add Food</button>}
                <ul>
                    {completeDay && completeDay.dayFoods.map((food, index) => {
                        return (
                            <li key={index}>
                                <h3>{food.details.name}</h3>
                                <p>Grams: {food.grams}</p>
                                <p>Calories: TO DO CALCULATE CALORIES</p>
                                {isCurrentDate && <button onClick={() => {deleteFood(date, food.details.name)}}>Delete Food</button>}
                            </li>
                        );
                    })}
                </ul>

                <h2>Physical Activities</h2>
                {isCurrentDate && <button onClick={() => navigate('/physical-activity')}>Add Physical Activity</button>}
                <ul>
                    {completeDay && completeDay.dayPhysicalActivities.map((activity, index) => {
                        return (
                            <li key={index}>
                                <h3>{activity.details.name}</h3>
                                <p>Minutes: {activity.minutes}</p>
                                <p>Calories burned: TO DO CALCULATE CALORIES BURNED</p>
                                {isCurrentDate && <button onClick={() => {deleteActivity(date, activity.details.name)}}>Delete Activity</button>}
                            </li>
                        );
                    })}
                </ul>
            </div>

            <button onClick={() => setDate(new Date(date.setDate(date.getDate() - 1)))}>Previous Day</button>
            {!isCurrentDate && <button onClick={() => setDate(new Date(date.setDate(date.getDate() + 1)))}>Next Day</button>}
        </div>
    );
}

export default Day;