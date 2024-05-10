import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import axios from "axios";

import Header from "../components/header/Header";

function Profile() {
    const [profileInfo, setProfileInfo] = useState(null);
    const navigate = useNavigate();

    // This runs the first time the component is mounted
    useEffect(() => {
        const token = localStorage.getItem('token');
        if (!token) {
            navigate('/');
        }

        const fetchProfileInfo = async () => {
            try {
                const response = await axios.get('https://localhost:7094/api/Profiles/user-profile', {
                    headers: {
                        Authorization: `Bearer ${token}` // Identify the user that is logged in
                    }
                });

                setProfileInfo(response.data);
            } catch (error) {
                console.log("Error fetching profile: ", error.response.data);
            }
        };

        fetchProfileInfo();
    });

    // While the request is being processed
    // If the request fails then the profile doesn't exist => user is prompted to create one
    if (!profileInfo) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <Header page='profile'/>
            <h3>{profileInfo.name}</h3>
            <p>Birthdate: {profileInfo.birthdate}</p>
            <p>Height: {profileInfo.height}</p>
            <p>Weight: {profileInfo.weight}</p>
            <p>Goal: {profileInfo.goal}</p>
        </div>
    );
}

export default Profile;