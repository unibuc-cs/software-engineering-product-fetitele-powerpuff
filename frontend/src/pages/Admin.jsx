import React, { useState } from "react";
import Header from "../components/header/Header";
import AdminUser from "../components/admin-user/AdminUser.jsx";
import AdminFood from "../components/admin-food/AdminFood.jsx";
import AdminPhysicalActivity from "../components/admin-physical-activity/AdminPhysicalActivity.jsx";
import AdminRequest from "../components/admin-request/AdminRequest.jsx";
import AdminRecipe from "../components/admin-recipe/AdminRecipe.jsx";
import AdminArticle from "../components/admin-article/AdminArticle.jsx";
import AdminTutorial from '../components/admin-tutorial/AdminTutorial.jsx';

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

                <button
                    className={activeComponent === 'recipes' ? 'active' : ''}
                    onClick={() => setActiveComponent('recipes')}>Edit Recipes</button>
                
                <button
                    className={activeComponent === 'article' ? 'active' : ''}
                    onClick={() => setActiveComponent('article')}>Edit Articles</button>

                <button
                    className={activeComponent === 'tutorial' ? 'active' : ''}
                    onClick={() => setActiveComponent('tutorial')}>Edit Tutorials</button>

                {activeComponent === 'users' && <AdminUser />}
                {activeComponent === 'food' && <AdminFood />}
                {activeComponent === 'physicalActivities' && <AdminPhysicalActivity />}
                {activeComponent === 'requests' && <AdminRequest />}
                {activeComponent === 'recipes' && <AdminRecipe />}
                {activeComponent === 'article' && <AdminArticle />}
                {activeComponent === 'tutorial' && <AdminTutorial />}
            </div>
        </div>
    );
}

export default Admin;