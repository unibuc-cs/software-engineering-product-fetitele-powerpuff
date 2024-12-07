import { useNavigate } from "react-router-dom";

export function ArticleDisplay({ article }) {
    const navigate = useNavigate();

    function redirectDetail(articlePass) {
        navigate('/article-detail', { state: { articlePass } });
    }

    return (
        <>
            <div className="card" style={{ width: '18rem', margin: 'auto', marginTop: '2rem' }}>
                <div className="card-body">
                    <h5 className="card-title" ><b>{article.title}</b></h5>
                    <h7 className="card-subtitle">By {article.author}</h7>
                    <p className="card-text">{article.content.slice(0, 100)}</p>
                    <button onClick={() => redirectDetail(article)} className="btn btn-primary">Check out</button>
                </div>
            </div>
        </>
    )
}