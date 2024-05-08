import { jwtDecode } from "jwt-decode";
import React from "react";

const AdminAuthGuard = ({ children }) => {
    const token = localStorage.getItem('token');

    if (token) {
        const decodedToken = jwtDecode(token);
        var roles = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

        if (roles && roles.includes('admin')) {
            return children;
        } 

    }

    return <p>404 Not Found</p>
};

export default AdminAuthGuard;