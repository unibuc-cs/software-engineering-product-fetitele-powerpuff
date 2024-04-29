import React from "react";
import Login from "../components/login/Login";
import Register from "../components/register/Register";

function LoginRegister() {
    return (
        <div>
            <Login name="login"/>
            <Register/>
        </div>
    );
}

export default LoginRegister;