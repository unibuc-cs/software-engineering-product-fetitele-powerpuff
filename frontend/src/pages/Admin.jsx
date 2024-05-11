import React, { useState } from "react";
import Header from "../components/header/Header";
import AdminUser from "../components/admin-user/AdminUser.jsx";
import AdminFood from "../components/admin-food/AdminFood.jsx";
import AdminPhysicalActivity from "../components/admin-physical-activity/AdminPhysicalActivity.jsx";

function Admin() {
    const [activeComponent, setActiveComponent] = useState('');

    return (
        <div>
            <Header page='admin'/>
            <button onClick={() => setActiveComponent('users')}>Edit Users</button>
            <button onClick={() => setActiveComponent('food')}>Edit Food</button>
            <button onClick={() => setActiveComponent('physicalActivities')}>Edit Physical Activities</button>

            {activeComponent === 'users' && <AdminUser />}
            {activeComponent === 'food' && <AdminFood />}
            {activeComponent === 'physicalActivities' && <AdminPhysicalActivity />}
        </div>
    );
}

export default Admin;