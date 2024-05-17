import React from "react";

function PhysicalActivityItemAdmin(props) {
    const muscles = props.muscles.map(muscle => (
        <li key={muscle.id}>{muscle.name}</li>
    ));

    return (
        <div>
            <h3>{props.name}</h3>
            <p>Id: {props.id}</p>
            <p>Calories: {props.calories}</p>
            <p>Muscles: </p>
            <ul>
                {muscles}
            </ul>            
        </div>
    );
}

export default PhysicalActivityItemAdmin;