import React, { useState } from "react";

import axios from "axios";

import Header from "../components/header/Header";
import PhysicalActivityItem from '../components/physical-activity-item/PhysicalActivityItem.jsx';

function PhysicalActivity() {
    const [physicalActivities, setPhysicalActivities] = useState([]);
    const [allError, setAllError] = useState(null);

    const [activityName, setActivityName] = useState('');
    const [activity, setActivity] = useState(null);
    const [nameError, setNameError] = useState(null);

    const [muscleName, setMuscleName] = useState('');
    const [activitiesByMuscle, setActivitiesByMuscle] = useState([]);
    const [muscleError, setMuscleError] = useState(null);

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
            console.log(error);
        }
    };

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
            console.log(error);
        }
    };

    const addActivityToDay = async (activityNameDay, minutes) => {
        setAddToDayError(null);
        if (minutes <= 0) {
            setAddToDayError('Minutes must be greater than 0');
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

    return (
        <div>
            <Header page='physical-activity'/>
            <div className="item-container">
                <h3>See All</h3>
                <button onClick={getAllActivites}>See All</button>

                <div>
                    {physicalActivities && physicalActivities.map(physicalActivity => {
                        return (<PhysicalActivityItem
                            key={physicalActivity.name}
                            name={physicalActivity.name}
                            muscles={physicalActivity.muscles}
                        />);
                    })}
                </div>

                {allError && <p className="error">{allError}</p>}

                <h2>Search Activity To Add To Day</h2>                
                <input className="search-item-input" type="text" placeholder="Activity Name" 
                    value={activityName} onChange={(event) => {setActivityName(event.target.value)}} />
                <button className="search-item-button" onClick={getByName}>Search</button>

                {activity && <PhysicalActivityItem key={activity.name} name={activity.name} muscles={activity.muscles} />}
                
                {activity && <div>
                    <label htmlFor="minutes">Minutes</label>
                    <input id="minutes" type="number" placeholder="Minutes" value={minutes} onChange={(event) => {setMinutes(event.target.value)}} />
                    <button className="add-item-day" onClick={() => {addActivityToDay(activity.name, minutes)}}>Add to Day</button>
                </div>}

                {nameError && <p className="error">{nameError}</p>}
                {addToDayError && <p className="error">{addToDayError}</p>}

                <input className="search-item-input" type="text" placeholder="Search by Muscle Name"
                    value={muscleName} onChange={(event) => {setMuscleName(event.target.value)}} />
                <button className="search-item-button" onClick={getByMuscle}>Search</button>

                {activitiesByMuscle && activitiesByMuscle.map(activity => {
                    return (<PhysicalActivityItem
                        key={activity.name}
                        name={activity.name}
                        muscles={activity.muscles}
                    />
                    );
                })}
                {muscleError && <p className="error">{muscleError}</p>}
            </div>
        </div>
    );
}

export default PhysicalActivity;