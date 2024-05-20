import React from "react";
import { useNavigate } from "react-router-dom";

import './logout.css';
import Header from "../header/Header";

function Logout() {
    const navigate = useNavigate('');

    function handleClick() {
        localStorage.removeItem('token');
        localStorage.removeItem('expiration');
        window.dispatchEvent(new Event('logout'));

        navigate('/');
    }

    return (
        <div>
            <Header page='logout'/>
            <div className="logout-container">
                <button onClick={handleClick}>Logout</button>
            </div>
        </div>
    );
}

export default Logout;