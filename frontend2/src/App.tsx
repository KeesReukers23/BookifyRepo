import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import UsersPage from './pages/UsersPage';



const App: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/usersPage" element={<UsersPage/>} />
            </Routes>
        </Router>
    );
};

export default App;
