import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { Chart } from 'primereact/chart';
import Header from "../components/header/Header";

function Profile() {
    const [profileInfo, setProfileInfo] = useState(null);
    const [editProfile, setEditProfile] = useState(false);
    const [name, setName] = useState('');
    const [birthdate, setBirthdate] = useState('');
    const [weight, setWeight] = useState('');
    const [height, setHeight] = useState('');
    const [goal, setGoal] = useState('');
    const [error, setError] = useState('');
    const [chartData, setChartData] = useState(null);
    const navigate = useNavigate();

    // Get the profile for the user that is logged in
    const fetchProfileInfo = async () => {
        const token = localStorage.getItem('token');
        if (!token) {
            navigate('/');
        }

        try {
            const response = await axios.get('https://localhost:7094/api/Profiles/user-profile', {
                headers: {
                    Authorization: `Bearer ${token}` // Identify the user that is logged in
                }
            });

            setProfileInfo(response.data);
        } 
        catch (error) {
            console.log("Error fetching profile: ", error.message);
        }
    };

    // Create a profile for the user that is logged in
    const handleCreate = async (event) => {
        event.preventDefault();
        if (!validateForm()) {
            console.log("Invalid form");
            return;
        }
        setError('');

        try {
            await axios.post('https://localhost:7094/api/Profiles', {
                name: name,
                birthdate: birthdate,
                weight: weight,
                height: height,
                goal: goal
            }, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            fetchProfileInfo();
        }
        catch (error) {
            console.log(error);
            if (error.response) {
                console.log("Error creating profile: ", error.response.data);
                setError(error.response.data);
            }
        }
    };

    // Update the profile for the user that is logged in
    const handleUpdate = async (event) => {
        event.preventDefault();
        setError('');
        setEditProfile(false);

        try {
            await axios.put('https://localhost:7094/api/Profiles', {
                name: name,
                birthdate: birthdate,
                weight: weight,
                height: height,
                goal: goal
            }, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            fetchProfileInfo();
        }
        catch (error) {
            console.log(error);
            if (error.response) {
                console.log("Error updating profile: ", error.response.data);
                setError(error.response.data);
            }
        }
    };

    // Delete the profile for the user that is logged in
    const deleteProfile = async () => {
        try {
            await axios.delete('https://localhost:7094/api/Profiles', {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setProfileInfo(null);
            setProfileInfo(null);
            setName('');
            setBirthdate('');
            setWeight('');
            setHeight('');
            setGoal('');
        } 
        catch (error) {
            console.log(error);
            if (error.response) {
                console.log("Error deleting profile: ", error.response.data);
                setError(error.response.data);
            }
        }
    }

    const getWeightEvolution = async () => {
        try {
            const response = await axios.get('https://localhost:7094/api/WeightEvolutions', {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            return response.data;
        } 
        catch (error) {
            console.log(error);
            if (error.response) {
                console.log("Error getting weight evolution: ", error.response.data);
            }
        }
    }

    // Render the form for creating or updating a profile
    const renderForm = (profileInfo) => {
        return (
            <div className="form-container">
                <form onSubmit={profileInfo ? handleUpdate : handleCreate}>
                    <div>
                        <label htmlFor="name">Name</label>
                        <input type="text" id="name"
                            value={name}
                            onChange={(event) => setName(event.target.value)}
                            placeholder="Name" />
                    </div>

                    <div>
                        <label htmlFor="birthdate">Birthdate</label>
                        <input type="date" id="birthdate"
                            value={birthdate} 
                            onChange={(event) => setBirthdate(event.target.value)} />
                    </div>

                    <div>
                        <label htmlFor="weight">Weight</label>
                        <input type="number" id="weight"
                            value={weight} 
                            onChange={(event) => setWeight(event.target.value)}
                            placeholder="Weight" />
                    </div>

                    <div>
                        <label htmlFor="height">Height</label>
                        <input type="number" id="height"
                            value={height} 
                            onChange={(event) => setHeight(event.target.value)}
                            placeholder="Height" />
                    </div>

                    <div>
                        <label htmlFor="goal">Goal</label>
                        <select id="goal" 
                                value={goal} 
                                onChange={(event) => setGoal(event.target.value)}>
                            <option value="0">Lose</option>
                            <option value="1">Gain</option>
                            <option value="2">Maintain</option>
                        </select>
                    </div>

                    <button type="submit">{profileInfo ? "Update Profile" : "Create Profile"}</button>
                    {error && <p className="error">{error}</p>}
                </form>
            </div>
        );
    };

    // Map the goal to the enum number
    const mapGoalToNumber = (goal) => {
        switch (goal) {
            case 'Lose':
                return 0;
            case 'Gain':
                return 1;
            case 'Maintain':
                return 2;
            default:
                return 0;
        }
    };

    // Validate the form
    const validateForm = () => {
        if (!name) {
            setError('Name is required');
            return false;
        }
    
        if (!birthdate) {
            setError('Birthdate is required');
            return false;
        }
    
        if (!weight) {
            setError('Weight is required');
            return false;
        }
    
        if (!height) {
            setError('Height is required');
            return false;
        }

        if (!goal) {
            setError('Goal is required');
            return false;
        }
       
        setError('');
        return true;
    };

    // This runs the first time the component is mounted
    useEffect(() => {
        fetchProfileInfo();
    }, []); // Empty dependency array => runs once on mount

     // This runs every time the profileInfo changes
     useEffect(() => {
        if (profileInfo) {
            getWeightEvolution().then(data => {
                // Sort data by date
                const sortedData = data.sort((a, b) => new Date(a.date) - new Date(b.date));
    
                const chartData2 = {
                    labels: sortedData.map(entry => new Date(entry.date).toLocaleDateString()),
                    datasets: [
                        {
                            label: 'Weight Evolution',
                            data: sortedData.map(entry => entry.weight),
                        },
                    ],
                };
                setChartData(chartData2);
            });
        }
    }, [profileInfo]);

    // This runs every time the editProfile changes
    // Used to put the profile info in the update form
    useEffect(() => {
        if (editProfile) {
            setName(profileInfo.name);
            setBirthdate(profileInfo.birthdate);
            setWeight(profileInfo.weight);
            setHeight(profileInfo.height);
            setGoal(mapGoalToNumber(profileInfo.goal));
        }
    }, [editProfile]);

    const chartOptions = {
        scales: {
            x: {
                title: {
                    display: true,
                    text: 'Date'
                }
            },
            y: {
                title: {
                    display: true,
                    text: 'Weight (kg)'
                }
            }
        }
    };

    // While the request is being processed
    // If the request fails then the profile doesn't exist
    // or if the user clicked on the edit profile button => 
    // user is seeing the profile form
    if (!profileInfo || editProfile) {
        return <div>{renderForm(profileInfo)}</div>;
    }

    return (
        <div>
            <Header page='profile'/>
            <div className="profile-container">
                <h3>{profileInfo.name}</h3>
                <p>Birthdate: {profileInfo.birthdate}</p>
                <p>Height: {profileInfo.height}</p>
                <p>Weight: {profileInfo.weight}</p>
                <p>Goal: {profileInfo.goal}</p>
                <button onClick={() => setEditProfile(true)}>Edit Profile</button>
                <button onClick={() => deleteProfile()}>Delete Profile</button>
                <h4>Weight Evolution</h4>
                <div className="chart-container">
                    {chartData && <Chart type="line" data={chartData} options={chartOptions} />}
                </div>
            </div>
        </div>
    );
}

export default Profile;