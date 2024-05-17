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

            <input type="text" placeholder="Activity Name" 
                   value={activityName} onChange={(event) => {setActivityName(event.target.value)}} />
            <button onClick={getByName}>Search</button>

            {activity && <PhysicalActivityItem key={activity.name} name={activity.name} muscles={activity.muscles} />}
            {nameError && <p>{nameError}</p>}

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