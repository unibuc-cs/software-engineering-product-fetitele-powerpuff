import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

import axios from 'axios';

function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const navigate = useNavigate('');

    const handleSubmit = async (event) => {
        // Prevent browser default of sending Http request
        event.preventDefault();
        setError('');

        if (!email || !password) {
            setError('Both email and password are required.');
            return;
        }

        // Send a Http request to login
        try {
            const response = await axios.post('https://localhost:7094/api/Authentication/login', {
                email: email,
                password: password
            });

            // Put the token in localStorage
            const { token, expiration } = response.data;

            localStorage.setItem('token', token);
            localStorage.setItem('expiration', expiration);
            // For the navbar
            window.dispatchEvent(new Event('login'));

            // Then navigate to user's profile
            navigate('/profile');
        } catch (error) {
            console.log("Login failed with status code", error.response.status);
            console.log("Message:", error.response.data);
            setError(error.response.data);
        }
    } 

    return (
        <div className="form-container">
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="login-email">Email</label>
                    <input type="email" id="login-email" 
                           value={email} onChange={(event) =>  setEmail(event.target.value)} 
                           placeholder="Email" />
                </div>

                <div>
                    <label htmlFor="login-password">Password</label>
                    <input type="password" id="login-password" 
                           value={password} onChange={(event) => setPassword(event.target.value)}
                           placeholder="Password" />
                </div>

                <button type="submit">Login</button>
                {error && <p className="error">{error}</p>}
            </form>
        </div>
    );
}

export default Login;