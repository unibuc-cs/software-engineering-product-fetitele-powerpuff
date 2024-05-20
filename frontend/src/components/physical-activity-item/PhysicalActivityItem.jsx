import React from "react";

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