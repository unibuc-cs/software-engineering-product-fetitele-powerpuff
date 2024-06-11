import React, { act, useEffect } from "react";
import Header from "../components/header/Header";
import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Day() {
    const navigate = useNavigate();
    const [completeDay, setCompleteDay] = useState(null);
    const [date, setDate] = useState(new Date());
    const [grams, setGrams] = useState({});
    const [updateGramsError, setUpdateGramsError] = useState(null);
    const [minutes, setMinutes] = useState({});
    const [updateMinutesError, setUpdateMinutesError] = useState(null);
    const [dayError, setDayError] = useState(null);

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
        catch (error) {
            console.log(error);
            throw error;
        }
    };

    const getWeight = async (dateString) => {
        const formattedDate = formatDate(new Date(dateString));

        try {
            const response = await axios.get(
                `https://localhost:7094/api/WeightEvolutions/weight?date=${formattedDate}`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            return response.data;
        } catch (error) {
            console.log(error);
        }
    }

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
            
            const weight = await getWeight(date);

            const completeDayData = { ...day, dayFoods: dayFoods, 
                dayPhysicalActivities: dayActivities, weight: weight };
            setCompleteDay(completeDayData);

            return completeDayData; 
        } 
        catch (error) {
            console.log(error);
            throw error;
        }
    };

    const updateGrams = async (foodName, grams) => {
        setUpdateGramsError(null);
        if (grams <= 0) {
            setUpdateGramsError('Grams must be greater than 0');
            return;
        }
        try {
            const formattedDate = formatDate(date);
            const response = await axios.put(`https://localhost:7094/api/Days/change-grams`, {
                date: formattedDate,
                foodName: foodName,
                grams: grams
            }, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            getCompleteDay(date).then((day) => updateGramsState(day));
            return response.data;
        }
        catch (error) {
            console.log(error);
            setUpdateGramsError(error.response.data);
        }
    };

    const updateMinutes = async (activityName, minutes) => {
        setUpdateMinutesError(null);
        if (minutes <= 0) { 
            setUpdateMinutesError('Minutes must be greater than 0');
            return;
        }
        try {
            const formattedDate = formatDate(date);
            const response = await axios.put(`https://localhost:7094/api/Days/change-minutes`, {
                date: formattedDate,
                activityName: activityName,
                minutes: minutes
            }, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            getCompleteDay(date).then((day) => updateMinutesState(day));
            return response.data;
        }
        catch (error) {
            console.log(error);
            setUpdateMinutesError(error.response.data);
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
            getCompleteDay(date);
        }
        catch (error) {
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
            getCompleteDay(date);
        }
        catch (error) {
            console.log(error);
        }
    };

    const updateGramsState = (day) => {
        const initialGrams = {};
        day.dayFoods.forEach(food => {
            initialGrams[food.details.name] = food.grams;
        });
        setGrams(initialGrams);
    };

    const updateMinutesState = (day) => {
        const initialMinutes = {};
        day.dayPhysicalActivities.forEach(activity => {
            initialMinutes[activity.details.name] = activity.minutes;
        });
        setMinutes(initialMinutes);
    };

    // Runs when the date changes (when the user clicks on the previous or next day buttons)
    useEffect(() => {
        setDayError(null);
        if (date) {
            getCompleteDay(date)
                .then((day) => {
                    if (day && day.dayFoods) {
                        updateGramsState(day);
                    }
                    if (day && day.dayPhysicalActivities) {
                        updateMinutesState(day);
                    }
                })
                .catch((error) => {
                    console.error('Error fetching day:', error);
                    setDayError('No data from this date');
                });
        }
    }, [date]);

    const foodCalories = (food) => Math.floor(food.details.calories * food.grams / 100); 
    const activityCalories = (activity, weight) => { 
        return Math.floor(activity.details.calories * (activity.minutes / 60) * weight); 
    };

    const sumCalories = (dayFoods) => {
        return Math.floor(dayFoods.reduce((acc, food) => acc + food.details.calories * food.grams / 100, 0));
    }

    const activeCalories = (dayActivities, weight) => {
        return Math.floor(dayActivities.reduce((acc, activity) => acc + activityCalories(activity, weight), 0));
    }

    return (
        <div>
            <Header page='day'/>
            <div id="day-container">
                {!dayError && <div id="day">
                    <h1>{formatDate(date)}</h1>
                    {completeDay && <h3>Calories: {sumCalories(completeDay.dayFoods)} / {completeDay.calories}</h3>}
                    {completeDay && <h3>
                        Calories left: {sumCalories(completeDay.dayFoods) <= completeDay.calories ? 
                        completeDay.calories - sumCalories(completeDay.dayFoods) : 0}
                    </h3>}
                    {completeDay && <h3>
                        Active calories: {activeCalories(completeDay.dayPhysicalActivities, completeDay.weight)}</h3>}

                    <h2>Food</h2>
                    {isCurrentDate && <button onClick={() => navigate('/food')}>Add Food</button>}
                    <ul>
                        {completeDay && completeDay.dayFoods.map((food, index) => {
                            return (
                                <li key={index}>
                                    <h3>{food.details.name}</h3>
                                    {!isCurrentDate && <p>Grams: {food.grams}</p>}
                                    {isCurrentDate && <p>Grams: 
                                        <input 
                                            type="number" 
                                            value={grams[food.details.name]} 
                                            onChange={(e) => updateGrams(food.details.name, e.target.value)}
                                        />
                                    </p>}
                                    {updateGramsError && <p>{updateGramsError}</p>}
                                    <p>Calories: {foodCalories(food)}</p>
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
                                    {!isCurrentDate  && <p>Minutes: {activity.minutes}</p>}
                                    {isCurrentDate && <p>Minutes:
                                        <input 
                                            type="number" 
                                            value={minutes[activity.details.name]} 
                                            onChange={(e) => updateMinutes(activity.details.name, e.target.value)}
                                        />
                                    </p>}
                                    <p>Calories burned: {activityCalories(activity, completeDay.weight)}</p>
                                    {isCurrentDate && <button onClick={() => {deleteActivity(date, activity.details.name)}}>Delete Activity</button>}
                                </li>
                            );
                        })}
                    </ul>
                </div>}

                {dayError && <h3 className="error">{dayError}</h3>}

                <div>
                    <button className="change-day" onClick={() => setDate(new Date(date.setDate(date.getDate() - 1)))}>Previous Day</button>
                    {!isCurrentDate && <button className="change-day" onClick={() => setDate(new Date(date.setDate(date.getDate() + 1)))}>Next Day</button>}
                </div>
            </div>
        </div>
    );
}

export default Day;