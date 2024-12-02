import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Register from './pages/Register';
import Login from './pages/Login';
import Home from './pages/Home';
import Navbar from './pages/Navbar';



const App: React.FC = () => {
    return (
        <Router>
           <Navbar />
            <Routes>
                <Route path="/" element={<Navigate to="/Login" replace />} />
                <Route path="/Home" element={<Home />} />
                <Route path="/Login" element={<Login />} />
                <Route path="/Register" element={<Register />} />
            </Routes>
        </Router>
    );
};

export default App;
