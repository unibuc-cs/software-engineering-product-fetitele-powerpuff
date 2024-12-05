import React, { useState } from 'react';
import axios from 'axios';

// Article component for admin
function AdminArticle () {
    const [title, setTitle] = useState('');
    const [author, setAuthor] = useState('');
    const [content, setContent] = useState('')
    const [createError, setCreateError] = useState(null);
    const [createSuccess, setCreateSuccess] = useState(null);

    const [deleteTitle, setDeleteTitle] = useState('');
    const [deleteError, setDeleteError] = useState(null);
    const [deleteSuccess, setDeleteSuccess] = useState(null);

    const createArticle = async (event) => {
        event.preventDefault();
        setCreateError(null);

        if (!title || !author || !content) {
            setCreateError('All fields must be set');
            setTimeout(() => {
                setCreateError(null);
            }, 3000);
            return;
        }
        
        try {
            const response = await axios.post('https://localhost:7094/api/Article', {
                title: title,
                author: author,
                content: content,
            }, 
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                }
            });
            setCreateSuccess(response.data);
            setTitle('');
            setAuthor('');
            setContent('');
            setTimeout(() => {
                setCreateSuccess(null);
            }, 3000);
        } 
        catch (error) {
            console.log(error);
            if (error.response) {
                console.log("Error creating article: ", error.response.data);
                setCreateError(error.response.data);
                setTimeout(() => {
                    setCreateError(null);
                }, 3000);
            }
        }
    }

    const deleteArticle = async (event) => {
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
            const response = await axios.delete(`https://localhost:7094/api/Article/${deleteTitle}`,
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
                console.log("Error deleting article: ", error.response.data);
                setDeleteError(error.response.data);
                setTimeout(() => {
                    setDeleteError(null);
                }, 3000);
            }
        }
    }

    return (
        <div className='edit-container'>
            <h1>Edit Articles</h1>

            <div id="add-article-admin" className='form-container'>
                <h3>Add Article</h3>
                <form onSubmit={createArticle}>
                    <div>
                        <label htmlFor="article-title">Title</label>
                        <input placeholder='Title' type="text" id="article-title" value={title} onChange={e => setTitle(e.target.value)} />
                    </div>

                    <div>
                        <label htmlFor="article-author">Author</label>
                        <input placeholder='Author' type="text" id="article-author" value={author} onChange={e => setAuthor(e.target.value)} />
                    </div>

                    <div>
                        <label htmlFor="content">Enter Content:</label>
                        <textarea id="article-content"
                            rows="10"
                            cols="30"
                            placeholder='Enter article content'
                            value={content}
                            onChange={(e) => setContent(e.target.value)}
                        />
                    </div>

                    <button type="submit">Create Article</button>
                    {createError && <p className='error'>{createError}</p>}
                    {createSuccess && <p className='success'>{createSuccess}</p>}
                </form>
            </div>

            <div id="delete-article-admin" className='form-container'>
                <h3>Delete Article</h3>
                <form onSubmit={deleteArticle}>
                    <label htmlFor="delete-title">Title</label>
                    <input placeholder='Title' type="text" id="delete-title" 
                            value={deleteTitle} onChange={e => setDeleteTitle(e.target.value)} />
                    <button type='submit'>Delete Article</button>
                    {deleteError && <p className='error'>{deleteError}</p>}
                    {deleteSuccess && <p className='success'>{deleteSuccess}</p>}
                </form>
            </div>
        </div>
    );
}

export default AdminArticle;