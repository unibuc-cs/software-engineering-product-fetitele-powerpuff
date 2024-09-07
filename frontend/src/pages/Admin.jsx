import React, { useState } from "react";
import Header from "../components/header/Header";
import AdminUser from "../components/admin-user/AdminUser.jsx";
import AdminFood from "../components/admin-food/AdminFood.jsx";
import AdminPhysicalActivity from "../components/admin-physical-activity/AdminPhysicalActivity.jsx";
import AdminRequest from "../components/admin-request/AdminRequest.jsx";

// Admin page for managing users, foods, activities and requests

function Admin() {
    const [activeComponent, setActiveComponent] = useState('');

    return (
        <div>
            <Header page='admin'/>
            <div className="admin-container">
                <button
                    className={activeComponent === 'users' ? 'active' : ''}
                    onClick={() => setActiveComponent('users')}>Edit Users</button>

                <button 
                    className={activeComponent === 'food' ? 'active' : ''}
                    onClick={() => setActiveComponent('food')}>Edit Food</button>

                <button 
                    className={activeComponent === 'physicalActivities' ? 'active' : ''}
                    onClick={() => setActiveComponent('physicalActivities')}>Edit Physical Activities</button>

                <button 
                    className={activeComponent === 'requests' ? 'active' : ''}
                    onClick={() => setActiveComponent('requests')}>Edit Requests</button>

                {activeComponent === 'users' && <AdminUser />}
                {activeComponent === 'food' && <AdminFood />}
                {activeComponent === 'physicalActivities' && <AdminPhysicalActivity />}
                {activeComponent === 'requests' && <AdminRequest />}
            </div>
        </div>
    );
}

export default Admin;