import React from "react";

function FoodItem(props) {
    return (
        <div className="item">
            <h3>{props.name}</h3>
            <p>Calories: {props.calories}</p>
            <p>Carbohydrates: {props.carbohydrates}</p>
            <p>Fats: {props.fats}</p>
            <p>Proteins: {props.proteins}</p>
        </div>
    );
}

export default FoodItem;