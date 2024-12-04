import React, { useState } from "react";

import axios from "axios";

import Header from "../components/header/Header";
import PhysicalActivityItem from '../components/physical-activity-item/PhysicalActivityItem.jsx';

function PhysicalActivity() {
    // Get all activities
    const [physicalActivities, setPhysicalActivities] = useState([]);
    const [minutesObj, setMinutesObj] = useState({});
    const [allError, setAllError] = useState(null);

    // Get activity by name
    const [activityName, setActivityName] = useState('');
    const [activity, setActivity] = useState(null);
    const [nameError, setNameError] = useState(null);

    // Get activities by targeted muscle
    const [muscleName, setMuscleName] = useState('');
    const [activitiesByMuscle, setActivitiesByMuscle] = useState([]);
    const [minutesMuscleObj, setMinutesMuscleObj] = useState({});
    const [muscleError, setMuscleError] = useState(null);

    // Add an activity to a day
    const [minutes, setMinutes] = useState(0);  
    const [addToDayError, setAddToDayError] = useState(null);

    const formatDate = (date) => {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        return `${year}-${month}-${day}`;
    };

    const getAllActivites = async () => {
        setAllError(null);

        // Toggle see all activities
        if (physicalActivities.length !== 0) {
            setPhysicalActivities([]);
            return;
        }

        const token = localStorage.getItem('token');

        try {
            const response = await axios.get('https://localhost:7094/api/PhysicalActivities',{
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setPhysicalActivities(response.data);
        } catch (error) {
            setPhysicalActivities([]);
            setAllError('No activites found');
            console.log(error);
        }
    };

    const getByName = async () => {
        setNameError(null);
        const token = localStorage.getItem('token');

        if (!activityName) {
            setNameError('No activity found');
            setTimeout(() => {
                setNameError(null);
            }, 3000);
            return;
        }

        try {
            const response = await axios.get(`https://localhost:7094/api/PhysicalActivities/${activityName}`,{
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            setActivity(response.data);
        } catch (error) {
            setActivity(null);
            setNameError('No activity with this name');
            setTimeout(() => {
                setNameError(null);
            }, 3000);
            console.log(error);
        }
    };

    // Get all activities that target the specified muscle
    const getByMuscle = async () => {
        setMuscleError(null);
        const token = localStorage.getItem('token');

        try {
            const response = await axios.get(`https://localhost:7094/api/PhysicalActivities/target-${muscleName}`,{
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            setActivitiesByMuscle(response.data);
        } catch (error) {
            setActivitiesByMuscle([]);
            setMuscleError('No activities targeting this muscle');
            setTimeout(() => {
                setMuscleError(null);
            }, 3000);
            console.log(error);
        }
    };

    // Add an activity to a day, minutes must be positive
    const addActivityToDay = async (activityNameDay, minutes) => {
        setAddToDayError(null);
        if (minutes <= 0) {
            setAddToDayError('Minutes must be greater than 0');
            setTimeout(() => {
                setAddToDayError(null);
            }, 3000);
            return;
        }
        try {
            const token = localStorage.getItem('token');
            await axios.put('https://localhost:7094/api/Days/add-activity', {
                date: formatDate(new Date()),
                activityName: activityNameDay,
                minutes: minutes
            }, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setMinutes(0);
        } 
        catch (error) {
            console.log(error);
        }
    }

    // Add to day for the get all activities section
    const addActivityToDayObj = async (activityNameDay, minutes) => {
        if (minutes <= 0) {
            return;
        }

        try {
            const token = localStorage.getItem('token');
            await axios.put('https://localhost:7094/api/Days/add-activity', {
                date: formatDate(new Date()),
                activityName: activityNameDay,
                minutes: minutes
            }, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            handleMinutesObj(activityNameDay, 0);
        } 
        catch (error) {
            console.log(error);
        }
    }

    // Add to day for the get by muscle section
    const addActivityToDayMuscleObj = async (activityNameDay, minutes) => {
        if (minutes <= 0) {
            return;
        }
        
        try {
            const token = localStorage.getItem('token');
            await axios.put('https://localhost:7094/api/Days/add-activity', {
                date: formatDate(new Date()),
                activityName: activityNameDay,
                minutes: minutes
            }, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            handleMinutesMuscleObj(activityNameDay, 0);
        } 
        catch (error) {
            console.log(error);
        }
    }

    // For the get all activities section, update
    // the minutes value for an activity
    const handleMinutesObj = (activityName, value) => {
        setMinutesObj({
            ...minutesObj,
            [activityName]: value
        });
    }

    // Same as above for the get by muscle section
    const handleMinutesMuscleObj = (activityName, value) => {
        setMinutesMuscleObj({
            ...minutesMuscleObj,
            [activityName]: value
        });
    }

    return (
        <div>
            <Header page='physical-activity'/>
            <div className="item-container">
                <h3>See All</h3>
                <button className="small-button" onClick={getAllActivites}>See All</button>

                <div>
                    {physicalActivities && physicalActivities.map(physicalActivity => {
                        return (
                            <div> 
                                <PhysicalActivityItem
                                    key={physicalActivity.name}
                                    name={physicalActivity.name}
                                    muscles={physicalActivity.muscles}
                                />
                                <label htmlFor="minutes">Minutes</label>
                                <input className="minutes" 
                                    type="number" 
                                    placeholder="Minutes" 
                                    value={minutesObj[physicalActivity.name]} 
                                    onChange={(event) => {handleMinutesObj(physicalActivity.name, event.target.value)}} />
                                <button className="add-item-day" 
                                    onClick={() => {addActivityToDayObj(physicalActivity.name, minutesObj[physicalActivity.name])}}>Add to Day</button>
                            </div>);
                    })}
                </div>

                {allError && <p className="error">{allError}</p>}

                <h2>Search Activity To Add To Day</h2>                
                <input className="search-item-input" type="text" placeholder="Activity Name" 
                    value={activityName} onChange={(event) => {setActivityName(event.target.value)}} />
                <button className="search-item-button small-button" onClick={getByName}>Search</button>

                {activity && <PhysicalActivityItem key={activity.name} name={activity.name} muscles={activity.muscles} />}
                
                {activity && <div>
                    <label htmlFor="minutes">Minutes</label>
                    <input className="minutes" type="number" placeholder="Minutes" value={minutes} onChange={(event) => {setMinutes(event.target.value)}} />
                    <button className="add-item-day" onClick={() => {addActivityToDay(activity.name, minutes)}}>Add to Day</button>
                </div>}

                {nameError && <p className="error">{nameError}</p>}
                {addToDayError && <p className="error">{addToDayError}</p>}

                <input className="search-item-input" type="text" placeholder="Search by Muscle Name"
                    value={muscleName} onChange={(event) => {setMuscleName(event.target.value)}} />
                <button className="search-item-button small-button" onClick={getByMuscle}>Search</button>

                {activitiesByMuscle && activitiesByMuscle.map(activity => {
                    return (
                        <div>
                            <PhysicalActivityItem
                                key={activity.name}
                                name={activity.name}
                                muscles={activity.muscles}
                            />
                            <label htmlFor="minutes">Minutes</label>
                                <input className="minutes" 
                                    type="number" 
                                    placeholder="Minutes" 
                                    value={minutesMuscleObj[activity.name]} 
                                    onChange={(event) => {handleMinutesMuscleObj(activity.name, event.target.value)}} />
                                <button className="add-item-day" 
                                    onClick={() => {addActivityToDayMuscleObj(activity.name, minutesMuscleObj[activity.name])}}>Add to Day</button>
                        </div>
                    );
                })}
                {muscleError && <p className="error">{muscleError}</p>}
            </div>
        </div>
    );
}

export default PhysicalActivity;