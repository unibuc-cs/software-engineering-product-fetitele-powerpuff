import React from "react";

// Physical activity item that a regular user will see
function PhysicalActivityItem(props) {
    const muscles = props.muscles.map(muscle => (
        <div key={muscle.id}>{muscle.name}</div>
    ));

    return (
        <div className="item">
            <h3>{props.name}</h3>
            <p>Muscles: </p>
            <div>
                {muscles}
            </div>            
        </div>
    );
}

export default PhysicalActivityItem;