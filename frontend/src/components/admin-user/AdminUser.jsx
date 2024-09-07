import React, { useState } from "react";
import axios from 'axios';

// Users component for the admin page
function AdminUser () {
    const [users, setUsers] = useState([]);
    const [promoteEmail, setPromoteEmail] = useState('');
    const [deleteEmail, setDeleteEmail] = useState('');
    const [getError, setGetError] = useState('');
    const [promoteError, setPromoteError] = useState('');
    const [promoteSuccess, setPromoteSuccess] = useState('');
    const [deleteError, setDeleteError] = useState('');
    const [deleteSuccess, setDeleteSuccess] = useState('');

    // Get all users
    const getUsers = async () => {
        setGetError('');

        // Toggle see all users
        if (users.length !== 0){
            setUsers([]);
            return;
        }

        try {
            const response = await axios.get('https://localhost:7094/api/ApplicationUser', {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setUsers(response.data);
        } 
        catch (error) {
            setUsers([]);
            setGetError('No users found');
            console.log(error);
        }
    }

    // An admin can promote a user to admin by email
    const promoteUser = async (promoteEmail) => {
        setPromoteError('');
        try {
            const response = await axios.put(`https://localhost:7094/api/Authentication/promote/${promoteEmail}`, {}, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setPromoteSuccess(response.data);
            setTimeout(() => {
                setPromoteSuccess('');
            }, 3000);
            setPromoteEmail('');
        } 
        catch (error) {
            console.log(error);
            if (error.response) {
                console.log("Error promoting user: ", error.response.data);
                setPromoteError(error.response.data);
            }
        }
    }

    // Delete a user by email
    const deleteUser = async (deleteEmail) => {
        setDeleteError('');
        try {
            const response = await axios.delete(`https://localhost:7094/api/ApplicationUser/dupa-email/${deleteEmail}`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setDeleteSuccess(response.data);
            setTimeout(() => {
                setDeleteSuccess('');
            }, 3000);
            setDeleteEmail('');
            getUsers();
        } 
        catch (error) {
            console.log("Error deleting user: ", error.response.data);
            setDeleteError(error.response.data);
        }
    }

    return (
        <div className="edit-container">
            <h1>Edit Users</h1>
            
            <div id="promote-user">
                <h3>Promote a User To Admin</h3>
                <input type="email" value={promoteEmail} onChange={(event) => setPromoteEmail(event.target.value)} placeholder="Email" />
                <button onClick={() => promoteUser(promoteEmail)}>Promote User</button>
                {promoteError && <p className="error">{promoteError}</p>}
                {promoteSuccess && <p className="success">{promoteSuccess}</p>}
            </div>

            <div id="delete-user">
                <h3>Delete a User</h3>
                <input type="email" value={deleteEmail} onChange={(event) => setDeleteEmail(event.target.value)} placeholder="Email" />
                <button onClick={() => deleteUser(deleteEmail)}>Delete User</button>
                {deleteError && <p className="error">{deleteError}</p>}
                {deleteSuccess && <p className="success">{deleteSuccess}</p>}
            </div>

            <button onClick={() => getUsers()}>View Users</button>
            <div id="admin-users">
                {users && users.map(user => {
                    return (
                        <div key={user.id}>
                            <h3>Email: {user.email}</h3>
                            <p>Id: {user.id}</p>
                        </div>
                    );
                })}
                {getError && <p className="error">{getError}</p>}
            </div>

        </div>
    );
}

export default AdminUser;