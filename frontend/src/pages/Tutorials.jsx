import React, { useEffect, useState } from 'react';
import axios from 'axios';
import Header from '../components/header/Header';
import { TutorialDisplay } from '../components/tutorial/TutorialDisplay';

export default function Tutorials() {
    const [tutorials, setTutorials] = useState([]);
    const [errorFetch, setErrorFetch] = useState(false);

    const [tutorialName, setTutorialName] = useState('');
    const [filteredTutorials, setFilteredTutorials] = useState([]);
    const [nameError, setNameError] = useState(null);

    useEffect(() => {
        async function getTutorials() {
            try {
                const token = localStorage.getItem('token');
                const response = await axios.get('https://localhost:7094/api/Tutorial', {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });
                setTutorials(response.data);
            } catch (error) {
                setErrorFetch(true);
            }
        }
        getTutorials();
    }, []);

    const filterTutorials = async() => {
        setNameError(null);
        setFilteredTutorials([]);

        if (!tutorialName) {
            setNameError('No tutorial with this name');
            setTimeout(() => {
                setNameError(null);
            }, 3000);
            return;
        }

        try {
            const token = localStorage.getItem('token');
            const response = await axios.get(`https://localhost:7094/api/Tutorial/filter`, {
                params: { name: tutorialName },
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            setFilteredTutorials(response.data);
        } catch (error) {
            setNameError('No tutorial with this name');
            setTimeout(() => {
                setNameError(null);
            }, 3000);
        }
    }

    return (
        <div>
            <Header page='tutorials' />
            <div className='item-container tutorial-container'>
                <input className='search-item-input' type='text' placeholder='Search tutorials' value={tutorialName} onChange={(e) => setTutorialName(e.target.value)} />
                <button className='search-item-button small-button' onClick={filterTutorials}>Search</button>

                {nameError && <p className='error'>{nameError}</p>}
                {filteredTutorials.length > 0 && filteredTutorials.map(tutorial => <div key={tutorial.name} ><TutorialDisplay tutorial={tutorial}></TutorialDisplay></div>)}

                <h2>All Tutorials</h2>
                {errorFetch && <p>Error fetching tutorials</p>}
                {tutorials.map(tutorial => <div key={crypto.randomUUID()}><TutorialDisplay tutorial={tutorial}></TutorialDisplay></div>)}
            </div>
        </div>
    )
}