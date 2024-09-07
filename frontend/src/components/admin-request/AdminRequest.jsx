import React, { useState } from 'react';
import axios from 'axios';

// Request component for the admin page
function AdminRequest() {
    const [requests, setRequests] = useState([]);
    const [approveRequestError, setApproveRequestError] = useState(null);
    const [approveRequestSuccess, setApproveRequestSuccess] = useState(null);
    const [denyRequestError, setDenyRequestError] = useState(null);
    const [denyRequestSuccess, setDenyRequestSuccess] = useState(null);

    // Get all requests
    const getAllRequests = async () => {
        // Toggle see all requests
        if (requests.length !== 0) {
            setRequests([]);
            return;
        }

        try {
            const token = localStorage.getItem('token');
            const response = await axios.get('https://localhost:7094/api/Request', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            return response.data;
        } 
        catch(error) {
            console.log(error);
        }
    }

    // Get food info (calories, carbs etc) for the food in the request
    const getFoodDetails = async (foodId) => {
        try {
            const token = localStorage.getItem('token');
            const response = await axios.get(`https://localhost:7094/api/Food/by-id/${foodId}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            return response.data;
        } 
        catch(error) {
            console.log(error);
        }
    }

    // Combine all request information
    const getCompleteRequests = async () => {
        try {
            const requests = await getAllRequests();
            const completeRequests = await Promise.all(requests.map(async (request) => {
                const foodDetails = await getFoodDetails(request.foodId);
                return {...request, foodDetails};
            }));
            setRequests(completeRequests);
        }
        catch(error) {
            console.log(error);
        }
    }

    // Request is approved, food becomes visible to all users
    // and no longer belongs to a user
    const approveRequest = async (requestId) => {
        setApproveRequestError(null);
        try {
            const token = localStorage.getItem('token');
            const response = await axios.put(`https://localhost:7094/api/Request/${requestId}`, {}, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setApproveRequestSuccess(response.data);
            setTimeout(() => setApproveRequestSuccess(null), 3000);
            getCompleteRequests();
        }
        catch(error) {
            console.log(error);
            setApproveRequestError(error.response.data);
        }
    }

    // Deny a request, food remains private
    const deleteRequest = async (requestId) => {
        setDenyRequestError(null);
        try {
            const token = localStorage.getItem('token');
            const response = await axios.delete(`https://localhost:7094/api/Request/${requestId}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setDenyRequestSuccess(response.data);
            setTimeout(() => setDenyRequestSuccess(null), 3000);
            getCompleteRequests();
        }
        catch(error) {
            console.log(error);
            setDenyRequestError(error.response.data);
        }
    }

    return (
        <div className='edit-container'>
            <h1>Edit Requests</h1>

            <h2>Approve a Request</h2>
            <input type="text" placeholder="Request Id" id="approve-request-id"/>
            <button onClick={() => approveRequest(document.getElementById('approve-request-id').value)}>Approve</button>
            {approveRequestError && <p className='error'>{approveRequestError}</p>}
            {approveRequestSuccess && <p className='success'>{approveRequestSuccess}</p>}

            <h2>Deny a Request</h2>
            <input type="text" placeholder="Request Id" id="deny-request-id"/>
            <button onClick={() => deleteRequest(document.getElementById('deny-request-id').value)}>Deny</button>
            {denyRequestError && <p className='error'>{denyRequestError}</p>}
            {denyRequestSuccess && <p className='success'>{denyRequestSuccess}</p>}

            <div className="button-container">
                <button id='requests-button' onClick={getCompleteRequests}>Get All Requests</button>
            </div>
            
            <div id="admin-requests">
                {requests.map(request => (
                    <div key={request.id}>
                        <h3>Request Id: {request.id}</h3>
                        <p>Food Id: {request.foodId}</p>
                        <p>Food Name: {request.foodDetails.name}</p>
                        <p>Calories: {request.foodDetails.calories}</p>
                        <p>Carbohydrates: {request.foodDetails.carbohydrates}</p>
                        <p>Fats: {request.foodDetails.fats}</p>
                        <p>Proteins: {request.foodDetails.proteins}</p>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default AdminRequest;