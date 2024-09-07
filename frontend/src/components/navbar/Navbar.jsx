import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

import './navbar.css';

function Navbar() {
    // If the user is not logged in => a login link is displayed
    // After login it changes to a logout link
    const [isLoggedIn, setIsLoggedIn] = useState(!!localStorage.getItem('token'));

    // Set event listeners for login and logout events (they are dispatched from the respective pages)
    useEffect(() => {
        const handleAuthChange = () => {
            setIsLoggedIn(!!localStorage.getItem('token'));
        };

        window.addEventListener('login', handleAuthChange);
        window.addEventListener('logout', handleAuthChange);

        // Remove events when component is unmounted
        return () => {
            window.removeEventListener('login', handleAuthChange);
            window.removeEventListener('logout', handleAuthChange);
        };
    }, []);

    return (
        <nav>
            <ul>
                {isLoggedIn ? 
                    <li><Link to="/logout">Logout</Link></li> : 
                    <li><Link to="/">Register/ Login</Link></li>}
                <li><Link to="/profile">Profile</Link></li>
                <li><Link to="/day">My Day</Link></li>
                <li><Link to="/food">Food</Link></li>
                <li><Link to="/physical-activity">Physical Activities</Link></li>
            </ul>
        </nav>
    );
}

export default Navbar;
