
import { Link } from "react-router-dom";

export function RetetaDisplay({reteta}){
    return (
      <>
      <div className="card" style={{width:'18rem',margin:'auto',marginTop:'2rem'}}>
      <div className="card-body">
    <h5 className="card-title" ><b>{reteta.name}</b></h5>
    <p className="card-text">{reteta.description.slice(0,100)}</p>
    <a href="#" className="btn btn-primary">Afla mai multe</a>
  </div>  
  </div>
      </>
    )
    
}