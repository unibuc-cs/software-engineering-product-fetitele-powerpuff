import { useNavigate } from "react-router-dom";

export function TutorialDisplay({ tutorial }) {
    const navigate = useNavigate();

    function redirectDetail(tutorialPass) {
        navigate('/tutorial-detail', { state: { tutorialPass } });
    }

    return (
        <>
            <div className="card" style={{ width: '18rem', margin: 'auto', marginTop: '2rem' }}>
                <div className="card-body">
                    <h5 className="card-title" ><b>{tutorial.title}</b></h5>
                    <p className="card-text">Grams: {tutorial.grams}</p>
                    <p className="card-text">Calories: {tutorial.calories}</p>
                    <p className="card-text">Carbohydrates: {tutorial.carbohydrates}</p>
                    <p className="card-text">Proteins: {tutorial.proteins}</p>
                    <p className="card-text">Fats: {tutorial.fats}</p>
                    <button onClick={() => redirectDetail(tutorial)} className="btn btn-primary">Check out</button>
                </div>
            </div>
        </>
    )
}