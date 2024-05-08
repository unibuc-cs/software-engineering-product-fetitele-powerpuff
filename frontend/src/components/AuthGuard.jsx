import React from "react";
import { Navigate } from "react-router-dom";

// This component chekcs authentication by checking if a token exists in localStorage
// and receives which component it should render if user is authenticated
const AuthGuard = ({ children }) => {
    if (localStorage.getItem('token')) {
        return children;
    } else {
        return <Navigate to='/' replace />;
    }
};

export default AuthGuard;