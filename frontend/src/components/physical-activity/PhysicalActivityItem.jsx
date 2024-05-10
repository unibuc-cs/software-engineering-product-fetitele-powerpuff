import React from "react";

function PhysicalActivityItem(props) {
    const muscles = props.muscles.map(muscle => (
        <li key={muscle.id}>{muscle.name}</li>
    ));

    return (
        <div>
            <h3>{props.name}</h3>
            <p>Muscles: </p>
            <ul>
                {muscles}
            </ul>            
        </div>
    );
}

export default PhysicalActivityItem;