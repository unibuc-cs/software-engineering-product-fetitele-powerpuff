import React, { useState } from 'react';
import axios from 'axios';
import PhysicalActivityItemAdmin from '../physical-activity-item/PhysicalActivityItemAdmin.jsx';

function AdminPhysicalActivity() {
    const [physicalActivities, setPhysicalActivities] = useState([]);
    const [muscles, setMuscles] = useState([]);

    const [getError, setGetError] = useState('');

    const [createName, setCreateName] = useState('');
    const [createCalories, setCreateCalories] = useState('');
    const [createError, setCreateError] = useState('');
    const [createSuccess, setCreateSuccess] = useState('');

    const [deleteName, setDeleteName] = useState('');
    const [deleteError, setDeleteError] = useState('');
    const [deleteSuccess, setDeleteSuccess] = useState('');

    const [pairMuscleName, setPairMuscleName] = useState('');
    const [pairActivityName, setPairActivityName] = useState('');
    const [pairError, setPairError] = useState('');
    const [pairSuccess, setPairSuccess] = useState('');

    const [unpairMuscleName, setUnpairMuscleName] = useState('');
    const [unpairActivityName, setUnpairActivityName] = useState('');
    const [unpairError, setUnpairError] = useState('');
    const [unpairSuccess, setUnpairSuccess] = useState('');

    const [getMusclesError, setGetMusclesError] = useState('');

    const [muscleCreateName, setMuscleCreateName] = useState('');
    const [muscleCreateError, setMuscleCreateError] = useState('');
    const [muscleCreateSuccess, setMuscleCreateSuccess] = useState('');

    const [muscleDeleteName, setMuscleDeleteName] = useState('');
    const [muscleDeleteError, setMuscleDeleteError] = useState('');
    const [muscleDeleteSuccess, setMuscleDeleteSuccess] = useState('');

    // Get all physical activities
    const getPhysicalActivities = async () => {
        setGetError('');

        if (physicalActivities.length !== 0) {
            setPhysicalActivities([]);
            return;
        }

        try {
            const response = await axios.get('https://localhost:7094/api/PhysicalActivities/for-admin', {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setPhysicalActivities(response.data);
        } 
        catch (error) {
            setPhysicalActivities([]);
            console.log(error);
            setGetError('No physical activities found');
        }
    };

    // Create a physical activity
    const createPhysicalActivity = async (event) => {
        event.preventDefault();
        setCreateError('');
        setCreateSuccess('');

        try {
            const response = await axios.post('https://localhost:7094/api/PhysicalActivities', {
                name: createName,
                calories: createCalories
            }, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setCreateSuccess(response.data);
            setTimeout(() => {
                setCreateSuccess('');
            }, 3000);
            setCreateName('');
            setCreateCalories('');
        } 
        catch (error) {
            console.log("Error creating physical activity: ", error.response.data);
            setCreateError(error.response.data);
        }
    }

    // Delete a physical activity by name
    const deletePhysicalActivity = async (event) => {
        event.preventDefault();
        setDeleteError('');
        setDeleteSuccess('');

        try {
            const response = await axios.delete(`https://localhost:7094/api/PhysicalActivities/${deleteName}`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setDeleteSuccess(response.data);
            setTimeout(() => {
                setDeleteSuccess('');
            }, 3000);
            setDeleteName('');
        }
        catch (error) {
            console.log("Error deleting physical activity: ", error.response.data);
            setDeleteError(error.response.data);
        }
    }

    // Pair a muscle with a physical activity
    const pairMuscleActivity = async (event) => {
        event.preventDefault();
        setPairError('');
        setPairSuccess('');

        try {
            const response = await axios.put(`https://localhost:7094/api/PhysicalActivitiesMuscles/${pairMuscleName}/${pairActivityName}`, {}, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setPairSuccess(response.data);
            setTimeout(() => {
                setPairSuccess('');
            }, 3000);
            setPairMuscleName('');
            setPairActivityName('');
        }
        catch (error) {
            console.log("Error pairing muscle with activity: ", error.response.data);
            setPairError(error.response.data);
        }
    }

    // Unpair a muscle with a physical activity
    const unpairMuscleActivity = async (event) => {
        event.preventDefault();
        setUnpairError('');
        setUnpairSuccess('');

        try {
            const response = await axios.delete(`https://localhost:7094/api/PhysicalActivitiesMuscles/${unpairMuscleName}/${unpairActivityName}`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setUnpairSuccess(response.data);
            setTimeout(() => {
                setUnpairSuccess('');
            }, 3000);
            setUnpairMuscleName('');
            setUnpairActivityName('');
        }
        catch (error) {
            console.log("Error unpairing muscle with activity: ", error.response.data);
            setUnpairError(error.response.data);    
        }
    }

    // Get all muscles
    const getMuscles = async () => {
        setGetMusclesError('');

        if (muscles.length !== 0) {
            setMuscles([]);
            return;
        }

        try {
            const response = await axios.get('https://localhost:7094/api/Muscles', {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setMuscles(response.data);
        }
        catch (error) {
            setMuscles([]);
            console.log(error);
            setGetMusclesError('No muscles found');
        }
    }

    // Create a muscle
    const createMuscle = async (event) => {
        event.preventDefault();
        setMuscleCreateError('');
        setMuscleCreateSuccess('');

        try {
            const response = await axios.post('https://localhost:7094/api/Muscles', {
                name: muscleCreateName
            }, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setMuscleCreateSuccess(response.data);
            setTimeout(() => {
                setMuscleCreateSuccess('');
            }, 3000);
            setMuscleCreateName('');
        }
        catch (error) {
            console.log("Error creating muscle: ", error.response.data);
            setMuscleCreateError(error.response.data);
        }
    }

    // Delete a muscle by name
    const deleteMuscle = async (event) => {
        event.preventDefault();
        setMuscleDeleteError('');
        setMuscleDeleteSuccess('');

        try {
            const response = await axios.delete(`https://localhost:7094/api/Muscles/${muscleDeleteName}`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setMuscleDeleteSuccess(response.data);
            setTimeout(() => {
                setMuscleDeleteSuccess('');
            }, 3000);
            setMuscleDeleteName('');
        }
        catch (error) {
            console.log("Error deleting muscle: ", error.response.data);
            setMuscleDeleteError(error.response.data);
        }
    }

    return (
        <div className='edit-container'>
            <h1>Edit Physical Activities</h1>

            <div id="add-physical-activity-admin" className='form-container'>
                <h3>Add a physical activity</h3>
                <form onSubmit={createPhysicalActivity}>
                    <div>
                        <label htmlFor="create-name">Name</label>
                        <input type="text" id="create-name" value={createName} placeholder='Name'
                               onChange={(event) => setCreateName(event.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="create-calories">Calories</label>
                        <input type="number" id="create-calories" value={createCalories} placeholder='Calories'
                               onChange={(event) => setCreateCalories(event.target.value)} />
                    </div>
                    <button type="submit">Add Physical Activity</button>
                    {createError && <p className='error'>{createError}</p>}
                    {createSuccess && <p className='success'>{createSuccess}</p>}
                </form>
            </div>

            <div id="delete-physical-activity-admin" className='form-container'>
                <h3>Delete a physical activity</h3>
                <form onSubmit={deletePhysicalActivity}>
                    <div>
                        <label htmlFor="delete-name">Name</label>
                        <input type="text" id="delete-name" value={deleteName} placeholder='Name'
                               onChange={(event) => setDeleteName(event.target.value)} />
                    </div>
                    <button type="submit">Delete Physical Activity</button>
                    {deleteError && <p className='error'>{deleteError}</p>}
                    {deleteSuccess && <p className='success'>{deleteSuccess}</p>}
                </form>
            </div>

            <div id="pair-muscle-activity-admin" className='form-container'>
                <h3>Pair a muscle with a physical activity</h3>
                <form onSubmit={pairMuscleActivity}>
                    <div>
                        <label htmlFor="pair-muscle-name">Muscle Name</label>
                        <input type="text" id="pair-muscle-name" value={pairMuscleName} placeholder='Muscle Name'
                                onChange={(event) => setPairMuscleName(event.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="pair-activity-name">Activity Name</label>
                        <input type="text" id="pair-activity-name" value={pairActivityName} placeholder='Activity Name'
                                onChange={(event) => setPairActivityName(event.target.value)} />
                    </div>
                    <button type="submit">Pair Muscle with Activity</button>
                    {pairError && <p className='error'>{pairError}</p>}
                    {pairSuccess && <p className='success'>{pairSuccess}</p>}
                </form>
            </div>

            <div id="unpair-muscle-activity-admin" className='form-container'>
                <h3>Unpair a muscle with a physical activity</h3>
                <form onSubmit={unpairMuscleActivity}>
                    <div>
                        <label htmlFor="unpair-muscle-name">Muscle Name</label>
                        <input type="text" id="unpair-muscle-name" value={unpairMuscleName} placeholder='Muscle Name'
                                onChange={(event) => setUnpairMuscleName(event.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="unpair-activity-name">Activity Name</label>
                        <input type="text" id="unpair-activity-name" value={unpairActivityName} placeholder='Activity Name'
                                onChange={(event) => setUnpairActivityName(event.target.value)} />
                    </div>
                    <button type="submit">Unpair Muscle with Activity</button>
                    {unpairError && <p className='error'>{unpairError}</p>}
                    {unpairSuccess && <p className='success'>{unpairSuccess}</p>}
                </form>
            </div>

            <button onClick={getPhysicalActivities}>Get Physical Activities</button>
            <div id="admin-physical-activities">
                {physicalActivities && physicalActivities.map(physicalActivity => {
                    return (<PhysicalActivityItemAdmin
                        key={physicalActivity.id}
                        id={physicalActivity.id}
                        name={physicalActivity.name}
                        calories={physicalActivity.calories}
                        muscles={physicalActivity.muscles}
                    />);
                })}
                {getError && <p className='error'>{getError}</p>}
            </div>

            <div id="add-muscle-admin" className='form-container'>
                <h3>Add a muscle</h3>
                <form onSubmit={createMuscle}>
                    <div>
                        <label htmlFor="muscle-create-name">Name</label>
                        <input type="text" id="muscle-create-name" value={muscleCreateName} placeholder='Name'
                                onChange={(event) => setMuscleCreateName(event.target.value)} />
                    </div>
                    <button type="submit">Add Muscle</button>
                    {muscleCreateError && <p className='error'>{muscleCreateError}</p>}
                    {muscleCreateSuccess && <p className='success'>{muscleCreateSuccess}</p>}
                </form>
            </div>

            <div id="delete-muscle-admin" className='form-container'>
                <h3>Delete a muscle</h3>
                <form onSubmit={deleteMuscle}>
                    <div>
                        <label htmlFor="muscle-delete-name">Name</label>
                        <input type="text" id="muscle-delete-name" value={muscleDeleteName} placeholder='Name'
                                onChange={(event) => setMuscleDeleteName(event.target.value)} />
                    </div>
                    <button type="submit">Delete Muscle</button>
                    {muscleDeleteError && <p className='error'>{muscleDeleteError}</p>}
                    {muscleDeleteSuccess && <p className='success'>{muscleDeleteSuccess}</p>}
                </form>
            </div>

            <button onClick={getMuscles}>Get Muscles</button>
            <div id="admin-muscles">
                {muscles && muscles.map(muscle => {
                    return (
                        <div key={muscle.id}>
                            <h3>Name: {muscle.name}</h3>
                            <p>Id: {muscle.id}</p>
                        </div>
                    );
                })}
                {getMusclesError && <p className='error'>{getMusclesError}</p>}
            </div>
        </div>
    );
}

export default AdminPhysicalActivity;