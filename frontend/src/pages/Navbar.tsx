import './Navbar.css';
import UserCountComponent from '../components/UserCountComponent';

const Navbar = () => {
    return (
        <nav className="navbar">
            <ul className="navbar-list">
                <li><a href="/home" className="navbar-item">Home</a></li>
                <li><a href="/collections" className="navbar-item">Collections</a></li>
                <li className="navbar-text"><UserCountComponent /></li>
            </ul>
        </nav>
    );
};

export default Navbar;
