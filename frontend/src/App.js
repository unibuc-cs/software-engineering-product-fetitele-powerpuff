import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Profile from './pages/Profile';
import PhysicalActivity from './pages/PhysicalActivity';
import Day from './pages/Day';
import Food from './pages/Food';
import Admin from './pages/Admin';
import LoginRegister from './pages/LoginRegister'; // Ensure this path is correct

function App() {
  return (
    <Router>
      <Routes>
        <Route exact path="/" element={<LoginRegister />} />
        <Route exact path="/profile" element={<Profile />} />
        <Route exact path="/day" element={<Day />} />
        <Route exact path="/food" element={<Food />} />
        <Route exact path="/physical-activity" element={<PhysicalActivity />} />
        <Route exact path="/admin" element={<Admin />} />
      </Routes>
    </Router>
  );
}

export default App;
