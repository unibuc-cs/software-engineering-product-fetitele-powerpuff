import React, { useState } from "react";

import axios from "axios";

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

        try {
            const response = await axios.get('https://localhost:7094/api/PhysicalActivities');
            setPhysicalActivities(response.data);
        } catch (error) {
            setPhysicalActivities([]);
            setAllError('No activites found');
            console.log(error);
        }
    };

    const getByName = async () => {
        setNameError(null);

        try {
            const response = await axios.get(`https://localhost:7094/api/PhysicalActivities/${activityName}`);

            setActivity(response.data);
        } catch (error) {
            setActivity(null);
            setNameError('No activity with this name');
            console.log(error);
        }
    };

    const getByMuscle = async () => {
        setMuscleError(null);

        try {
            const response = await axios.get(`https://localhost:7094/api/PhysicalActivities/target-${muscleName}`);

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
        } 
        catch (error) {
            console.log(error);
        }
    }

    return (
        <div>
            <button onClick={getAllActivites}>See All</button>

            {physicalActivities && physicalActivities.map(physicalActivity => {
                return (<PhysicalActivityItem
                    key={physicalActivity.name}
                    name={physicalActivity.name}
                    muscles={physicalActivity.muscles}
                />);
            })}
            {allError && <p>{allError}</p>}

            <h2>Search Activity To Add To Day</h2>                
            <input type="text" placeholder="Activity Name" 
                   value={activityName} onChange={(event) => {setActivityName(event.target.value)}} />
            <button onClick={getByName}>Search</button>

            {activity && <PhysicalActivityItem key={activity.name} name={activity.name} muscles={activity.muscles} />}
            
            {activity && <div>
                <label htmlFor="minutes">Minutes</label>
                <input type="number" placeholder="Minutes" value={minutes} onChange={(event) => {setMinutes(event.target.value)}} />
                <button onClick={() => {addActivityToDay(activity.name, minutes)}}>Add to Day</button>
            </div>}

            {nameError && <p>{nameError}</p>}
            {addToDayError && <p>{addToDayError}</p>}

            <input type="text" placeholder="Search by Muscle Name"
                   value={muscleName} onChange={(event) => {setMuscleName(event.target.value)}} />
            <button onClick={getByMuscle}>Search</button>

            {activitiesByMuscle && activitiesByMuscle.map(activity => {
                return (<PhysicalActivityItem
                    key={activity.name}
                    name={activity.name}
                    muscles={activity.muscles}
                />
                );
            })}
            {muscleError && <p>{muscleError}</p>}
        </div>
    );
}

export default PhysicalActivity;