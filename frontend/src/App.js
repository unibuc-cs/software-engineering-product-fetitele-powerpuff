import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Profile from './pages/Profile';
import PhysicalActivity from './pages/PhysicalActivity';
import Day from './pages/Day';
import Food from './pages/Food';
import Admin from './pages/Admin';
import LoginRegister from './pages/LoginRegister'; 
import AuthGuard from './components/AuthGuard';
import AdminAuthGuard from './components/AdminAuthGuard';
import Logout from './components/logout/Logout';
import Navbar from './components/navbar/Navbar';

function App() {
  return (
    <Router>
       <Navbar/>
       <Routes>
              <Route exact path="/" element={<LoginRegister />} /> {/* Only route unauthenticated users can access */}
              <Route exact path="/logout"
                     element={<AuthGuard><Logout /></AuthGuard>} />
              <Route exact path="/profile" 
                     element={<AuthGuard><Profile /></AuthGuard>} />
              <Route exact path="/day" 
                     element={<AuthGuard><Day /></AuthGuard>} />
              <Route exact path="/food" 
                     element={<AuthGuard><Food /></AuthGuard>} />
              <Route exact path="/physical-activity" 
                     element={<AuthGuard><PhysicalActivity /></AuthGuard>} />
              <Route exact path="/admin" 
                     element={<AdminAuthGuard><Admin /></AdminAuthGuard>} />
              </Routes>
    </Router>
  );
}

export default App;
