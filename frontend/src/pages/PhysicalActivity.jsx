import React from "react";

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
            <p>PhysicalActivity page</p>
        </div>
    );
}

export default PhysicalActivity;