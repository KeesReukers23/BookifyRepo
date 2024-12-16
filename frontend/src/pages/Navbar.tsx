import React from 'react';
import './Navbar.css';

const Navbar = () => {
    return (
        <div className="navbar">
            <a href="/home" className="navbar-item">Home</a>
            <a href="/collections" className="navbar-item">Collections</a>
            <a href="/friends" className="navbar-item">Friends</a>
        </div>
    );
};

export default Navbar;
