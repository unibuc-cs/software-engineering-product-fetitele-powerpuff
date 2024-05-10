import React from "react";
import './header.css'

function Header({page}) {
    return (
        <header>
            {page === 'login-register' && <p>Welcome to the Healthy Lifestyle App</p>}
            {page === 'profile' && <p>My Profile</p>}
            {page === 'day' && <p>My Day</p>}
            {page === 'food' && <p>Food</p>}
            {page === 'physical-activity' && <p>Physical Activities</p>}
            {page === 'admin' && <p>Admin Page</p>}
        </header>
    );
}

export default Header;