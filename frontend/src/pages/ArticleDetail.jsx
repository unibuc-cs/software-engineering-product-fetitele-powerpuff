import { useLocation } from "react-router-dom";

export default function ArticleDetail() {
    const { state } = useLocation();
    const article = state.articlePass;

    return (
        <div>
            <div className="card" style={{ width: '75rem', margin: 'auto', marginTop: '2rem', padding:'2rem' }}>
                <div className="card-body">
                    <h2 className="display-2 text-center" ><b>{article.title}</b></h2>
                    <h6 className="display-6 text-center" >By {article.author}</h6>
                    <p>{article.content}</p>
                </div>
            </div>
        </div>
    );
}