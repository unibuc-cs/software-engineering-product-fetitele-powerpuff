import React from "react";
import './header.css'

// Basic header
function Header({page}) {
    return (
        <header>
            {page === 'login-register' && <h2>Welcome to the Healthy Lifestyle App</h2>}
            {page === 'profile' && <h2>My Profile</h2>}
            {page === 'day' && <h2>My Day</h2>}
            {page === 'food' && <h2>Food</h2>}
            {page === 'physical-activity' && <h2>Physical Activities</h2>}
            {page === 'recipe' && <h2>Recipes</h2>}
            {page === 'articles' && <h2>Articles</h2>}
            {page === 'tutorials' && <h2>Tutorials</h2>}
            {page === 'admin' && <h2>Admin Page</h2>}
            {page === 'logout' && <h2>Logout</h2>}
        </header>
    );
}

export default Header;