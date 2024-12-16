import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Register from './pages/Register';
import Login from './pages/Login';
import Home from './pages/Home';
import Collections from './pages/Collections';
import CollectionPage from './pages/CollectionPage';



const App: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Navigate to="/login" replace />} />
                <Route path="/home" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/collections" element={<Collections />} />
                <Route path="/collections/:collectionId" element={<CollectionPage />} />
            </Routes>
        </Router>
    );
};

export default App;
