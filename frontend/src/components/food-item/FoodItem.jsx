import React from "react";

function FoodItem(props) {
    console.log(props.public);
    return (
        <div>
            <h3>{props.name}</h3>
            <p>Calories: {props.calories}</p>
            <p>Carbohydrates: {props.carbohydrates}</p>
            <p>Fats: {props.fats}</p>
            <p>Proteins: {props.proteins}</p>
            <p>Public: {props.public.toString()}</p>
        </div>
    );
}

export default FoodItem;