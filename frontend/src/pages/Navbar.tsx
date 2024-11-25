import React from 'react';
import { Link } from 'react-router-dom';
import './Navbar.css';

const Navbar: React.FC = () => {
  return (
    <nav className="navbar">
      <ul className="navbar-list">
        <li className="navbar-item">
          <Link to="/your-posts">Your Posts</Link>
        </li>
        <li className="navbar-item">
          <Link to="/friends">Friends</Link>
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;
