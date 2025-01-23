import { useLocation } from "react-router-dom";
import FoodItem from "../components/food-item/FoodItem";


export default function TutorialDetail(){
    const location = useLocation()
    const {tutorialPass} = location.state || {}
   
    
    
    return <>
    <div className="item">
    <b style={{fontSize: '4.5rem'}}>Tutorial for {tutorialPass.title}</b>


    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'flex-start' ,borderTop:'5px', paddingTop: '20px',borderBottom:'5px',paddingBottom:'30px' }}>
  <iframe 
    width="800"
    height="450"
    src={tutorialPass.videoLink}
    title="YouTube video player"
    allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
    referrerPolicy="strict-origin-when-cross-origin"
    allowFullScreen
  ></iframe>


</div>

<div>
  
  <span style={{ display: 'block' }}>Number of calories: {tutorialPass.calories}</span>
  <span style={{ display: 'block' }}>Carbs: {tutorialPass.carbohydrates}</span>
  <span style={{ display: 'block' }}>Fats: {tutorialPass.fats}</span>
  <span style={{ display: 'block' }}>Proteins: {tutorialPass.proteins}</span>
</div>
</div>
    </>
}