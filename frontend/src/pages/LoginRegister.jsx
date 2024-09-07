import React from "react";
import Login from "../components/login/Login";
import Register from "../components/register/Register";
import Header from "../components/header/Header";

// Render both login and register forms

function LoginRegister() {
    return (
        <div>
            <Header page='login-register'/>
            <Login/>
            <Register/>
        </div>
    );
}

export default LoginRegister;