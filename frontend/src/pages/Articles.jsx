import React, { useEffect, useState } from 'react';
import axios from 'axios';
import Header from '../components/header/Header';
import { ArticleDisplay } from '../components/article/ArticleDisplay';

export default function Articles() {
    const [articles, setArticles] = useState([]);
    const [errorFetch, setErrorFetch] = useState(false);

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

    return (
        <div>
            <Header page='articles' />
            <div className='item-container article-container'>
                <h2>All Articles</h2>
                {errorFetch && <p>Error fetching articles</p>}
                {articles.map(article=><div key={crypto.randomUUID()}><ArticleDisplay article={article}></ArticleDisplay></div>)}
            </div>
        </div>
    );
}