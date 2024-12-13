import React, { useEffect, useState } from 'react';
import axios from 'axios';
import Header from '../components/header/Header';
import { ArticleDisplay } from '../components/article/ArticleDisplay';

export default function Articles() {
    const [articles, setArticles] = useState([]);
    const [errorFetch, setErrorFetch] = useState(false);

    const [articleName, setArticleName] = useState('');
    const [filteredArticles, setFilteredArticles] = useState([]);
    const [nameError, setNameError] = useState(null);

    useEffect(() => {
        async function getArticles() {
            try {
                const token = localStorage.getItem('token');
                const response = await axios.get('https://localhost:7094/api/Article', {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });
                setArticles(response.data);
            } catch (error) {
                setErrorFetch(true);
            }
        }
        getArticles();
    }, []);

    const filterArticles = async() => {
        setNameError(null);
        setFilteredArticles([]);

        if (!articleName) {
            setNameError('No article with this name');
            setTimeout(() => {
                setNameError(null);
            }, 3000);
            return;
        }

        try {
            const token = localStorage.getItem('token');
            const response = await axios.get(`https://localhost:7094/api/Article/filter`, {
                params: { name: articleName },
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            setFilteredArticles(response.data);
        } catch (error) {
            setNameError('No article with this name');
            setTimeout(() => {
                setNameError(null);
            }, 3000);
        }
    }

    return (
        <div>
            <Header page='articles' />
            <div className='item-container article-container'>
                <input className='search-item-input' type='text' placeholder='Search articles' value={articleName} onChange={(e) => setArticleName(e.target.value)} />
                <button className='search-item-button small-button' onClick={filterArticles}>Search</button>

                {nameError && <p className='error'>{nameError}</p>}
                {filteredArticles.length > 0 && filteredArticles.map(article => <div key={article.name} ><ArticleDisplay article={article}></ArticleDisplay></div>)}

                <h2>All Articles</h2>
                {errorFetch && <p>Error fetching articles</p>}
                {articles.map(article => <div key={crypto.randomUUID()}><ArticleDisplay article={article}></ArticleDisplay></div>)}
            </div>
        </div>
    );
}