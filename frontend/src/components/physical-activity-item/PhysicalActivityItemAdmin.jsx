import React from "react";

// COmponent for the admin page
function PhysicalActivityItemAdmin(props) {
    const muscles = props.muscles.map(muscle => (
        <p key={muscle.id}>{muscle.name}</p>
    ));

    return (
        <div className="admin-activity-item">
            <h3>{props.name}</h3>
            <p>Id: {props.id}</p>
            <p>Calories: {props.calories}</p>
            <p>Muscles: </p>
            <div>
                {muscles}
            </div>            
        </div>
    );
}

export default PhysicalActivityItemAdmin;