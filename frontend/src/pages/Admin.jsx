import React, { useState } from "react";
import Header from "../components/header/Header";
import AdminUser from "../components/admin-user/AdminUser.jsx";

function Admin() {
    const [editUsers, setEditUsers] = useState(false);
    const [editFood, setEditFood] = useState(false);
    const [editPhysicalActivities, setEditPhysicalActivities] = useState(false);

    if (editUsers) {
        return (
            <div>
                <Header page='admin'/>
                <button onClick={() => setEditUsers(!editUsers)}>Edit Users</button>
                <button onClick={() => setEditFood(!editFood)}>Edit Food</button>
                <button onClick={() => setEditPhysicalActivities(!editPhysicalActivities)}>Edit Physical Activities</button>
                <AdminUser/>
            </div>
        );
    }

    return (
        <div>
            <Header page='admin'/>
            <button onClick={() => setEditUsers(!editUsers)}>Edit Users</button>
            <button onClick={() => setEditFood(!editFood)}>Edit Food</button>
            <button onClick={() => setEditPhysicalActivities(!editPhysicalActivities)}>Edit Physical Activities</button>
        </div>
    );
}

export default Admin;