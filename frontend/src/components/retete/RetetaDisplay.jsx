
import { useNavigate } from "react-router-dom";
export function RetetaDisplay({reteta}){
  const navigate = useNavigate()

  function redirectDetail(retetaPass){
    navigate('/RetetaDetail',{state:{retetaPass}}
    )
  }

    return (
      <>
      <div className="card" style={{width:'18rem',margin:'auto',marginTop:'2rem'}}>
      <div className="card-body">
    <h5 className="card-title" ><b>{reteta.name}</b></h5>
    <p className="card-text">{reteta.description.slice(0,100)}</p>
    <button onClick={()=>redirectDetail(reteta)} className="btn btn-primary">Check out</button>
  </div>  
  </div>
      </>
    )
    
}