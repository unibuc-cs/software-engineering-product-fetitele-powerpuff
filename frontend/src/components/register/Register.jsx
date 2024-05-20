import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

import axios from "axios";

function Register() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate('');

    const handleSubmit = async (event) => {
        event.preventDefault();
        setError('');

        try {
            const response = await axios.post('https://localhost:7094/api/Authentication/register-user', {
                email: email,
                password: password,
                confirmPassword: confirmPassword
            });

            const { message, status } = response.data;
            console.log('Message: ' + message);
            console.log('Status: ' + status);

            if (status === 'Success') {
                await handleLogin(email, password);
            }
        } catch (error) {
            console.log("Registration failed with status code", error.response.status);
            // Get message from backend (if there was a validation error (invalid model) then take title)
            console.log("Message:", error.response.data.message || error.response.data.title); 
            setError(error.response.data.message || "Passwords don't match");
        }
    }

    const handleLogin = async (email, password) => {
        setError('');

        try {
            const response = await axios.post('https://localhost:7094/api/Authentication/login', {
                email: email,
                password: password
            });

            const { token, expiration } = response.data;

            localStorage.setItem('token', token);
            localStorage.setItem('expiration', expiration);

            // When registering there will be no profile
            // The user will be prompted to create it
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
                    <label htmlFor="register-email">Email</label>
                    <input type="email" id="register-email" 
                           value={email} onChange={(event) => setEmail(event.target.value)}
                           placeholder="Email" />
                </div>

                <div>
                    <label htmlFor="register-password">Password</label>
                    <input type="password" id="register-password"
                           value={password} onChange={(event) => setPassword(event.target.value)}
                           placeholder="Password" />
                </div>

                <div>
                    <label htmlFor="register-confirmPassword">Confirm Password</label>
                    <input type="password" id="register-confirmPassword"
                           value={confirmPassword} onChange={(event) => setConfirmPassword(event.target.value)}
                           placeholder="Confirm Password" />
                </div>

                <button type="submit">Register</button>
                {error && <p className="error">{error}</p>}
            </form>
        </div>
    );
}

export default Register;