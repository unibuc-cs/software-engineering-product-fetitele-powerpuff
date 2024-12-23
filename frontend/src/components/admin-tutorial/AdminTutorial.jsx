import React, { useState } from 'react';
import axios from 'axios';

// Tutorial component for admin
function TutorialArticle() {
    const [tutorials, setTutorials] = useState([]);
    const [getError, setGetError] = useState(null);

    const [title, setTitle] = useState('');
    const [grams, setGrams] = useState('');
    const [calories, setCalories] = useState('');
    const [carbohydrates, setCarbohydrates] = useState('');
    const [proteins, setProteins] = useState('');
    const [fats, setFats] = useState('');
    const [videoLink, setVideoLink] = useState('');
    const [createError, setCreateError] = useState(null);
    const [createSuccess, setCreateSuccess] = useState(null);

    const [deleteTitle, setDeleteTitle] = useState('');
    const [deleteError, setDeleteError] = useState(null);
    const [deleteSuccess, setDeleteSuccess] = useState(null);

    const getTutorials = async () => {
        setGetError(null);

        if (tutorials.length != 0) {
            setTutorials([]);
            return;
        }

        try {
            const response = await axios.get('https://localhost:7094/api/Tutorial/for-admin', {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setTutorials(response.data);
        } 
        catch (error) {
            setTutorials([]);
            console.log(error);
            setGetError('No tutorials found');
            setTimeout(() => {
                setGetError(null);
            }, 3000);
        }
    }

    // Create a tutorial
    const createTutorial = async (event) => {
        event.preventDefault();
        setCreateError(null);

        if (!title || !grams || !calories || !carbohydrates || !fats || !proteins || !videoLink) {
            setCreateError('All fields must be set');
            setTimeout(() => {
                setCreateError(null);
            }, 3000);
            return;
        }

        if (calories <= 0 || grams < 0 || carbohydrates < 0 || fats < 0 || proteins < 0) {
            setCreateError('Please enter values greater or equal to 0 for calories and greater than 0 for the rest.');
            setTimeout(() => {
                setCreateError(null);
            }, 5000);
            return;
        }

        try {
            const response = await axios.post('https://localhost:7094/api/Tutorial', {
                title: title,
                grams: grams,
                calories: calories,
                carbohydrates: carbohydrates,
                proteins: proteins,
                fats: fats,
                videoLink: videoLink
            }, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setCreateSuccess(response.data);
            setTimeout(() => {
                setCreateSuccess(null);
            }, 3000);
            setTitle('');
            setGrams('');
            setCalories('');
            setCarbohydrates('');
            setProteins('');
            setFats('');
            setVideoLink('');
        } catch (error) {
            console.log(error);
            if (error.response) {
                setCreateError(error.response.data);
            } else {
                setCreateError('Failed to create tutorial');
            }
            setTimeout(() => {
                setCreateError(null);
            }, 3000);
        }
    }

    const deleteTutorial = async (event) => {
        event.preventDefault();
        setDeleteError(null);

        if (!deleteTitle) {
            setDeleteError('Title must be specified');
            setTimeout(() => {
                setDeleteError(null);
            }, 3000);
            return;
        }

        try {
            const response = await axios.delete(`https://localhost:7094/api/Tutorial/${deleteTitle}`,
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setDeleteSuccess(response.data);
            setDeleteTitle('');
            setTimeout(() => {
                setDeleteSuccess(null);
            }, 3000);
        } 
        catch (error) {
            console.log(error);
            if (error.response) {
                setDeleteError(error.response.data);
            } else {
                setDeleteError('Failed to delete tutorial');
            }
            setTimeout(() => {
                setDeleteError(null);
            }, 3000);
        }
    }

    return (
        <div className='edit-container'>
            <h1>Edit Tutorials</h1>

            <div id="add-tutorial-admin" className='form-container'>
                <h3>Add Tutorial</h3>
                <form onSubmit={createTutorial}>
                    <div>
                        <label htmlFor="tutorial-title">Title</label>
                        <input placeholder='Title' type="text" id="tutorial-title" value={title} onChange={e => setTitle(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="tutorial-grams">Grams</label>
                        <input placeholder='Grams' type="number" id="tutorial-grams" value={grams} onChange={e => setGrams(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="tutorial-calories">Calories</label>
                        <input placeholder='Calories' type="number" id="tutorial-calories" value={calories} onChange={e => setCalories(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="tutorial-carbohydrates">Carbohydrates</label>
                        <input placeholder='Carbohydrates' type="number" id="tutorial-carbohydrates" value={carbohydrates} onChange={e => setCarbohydrates(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="tutorial-proteins">Proteins</label>
                        <input placeholder='Proteins' type="number" id="tutorial-proteins" value={proteins} onChange={e => setProteins(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="tutorial-fats">Fats</label>
                        <input placeholder='Fats' type="number" id="tutorial-fats" value={fats} onChange={e => setFats(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor="tutorial-link">Video Link</label>
                        <input placeholder='Video Link' type="text" id="tutorial-link" value={videoLink} onChange={e => setVideoLink(e.target.value)} />
                    </div>
                    <button type="submit">Create Tutorial</button>
                    {createError && <p className='error'>{createError}</p>}
                    {createSuccess && <p className='success'>{createSuccess}</p>}
                </form>
            </div>

            <div id="delete-tutorial-admin" className='form-container'>
                <h3>Delete Tutorial</h3>
                <form onSubmit={deleteTutorial}>
                    <label htmlFor="delete-title">Title</label>
                    <input placeholder='Title' type="text" id="delete-title" 
                            value={deleteTitle} onChange={e => setDeleteTitle(e.target.value)} />
                    <button type='submit'>Delete Tutorial</button>
                    {deleteError && <p className='error'>{deleteError}</p>}
                    {deleteSuccess && <p className='success'>{deleteSuccess}</p>}
                </form>
            </div>

            <button onClick={getTutorials}>Get Tutorials</button>

            <div id="admin-tutorials">
                {tutorials && tutorials.map(tutorial => {
                    return (
                        <div key={tutorial.id}>
                            <h3>{tutorial.title}</h3>
                            <p>Id: {tutorial.id}</p>
                            <p>Grams: {tutorial.grams}</p>
                            <p>Calories: {tutorial.calories}</p>
                            <p>Carbohydrates: {tutorial.carbohydrates}</p>
                            <p>Proteins: {tutorial.proteins}</p>
                            <p>Fats: {tutorial.fats}</p>
                            <p>Video Link: {tutorial.videoLink}</p>
                        </div>
                    );
                })}

                {getError && <p className='error'>{getError}</p>}
            </div>
        </div>
    );
}

export default TutorialArticle;