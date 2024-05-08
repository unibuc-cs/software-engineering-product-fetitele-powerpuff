import React from "react";
import { useNavigate } from "react-router-dom";

function Logout() {
    const navigate = useNavigate('');

    function handleClick() {
        localStorage.removeItem('token');
        localStorage.removeItem('expiration');

        navigate('/');
    }

    return (
        <div>
            <button onClick={handleClick}>Logout</button>
        </div>
    );
}

export default Logout;