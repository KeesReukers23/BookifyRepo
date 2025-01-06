import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios'; // Vergeet niet Axios te importeren
import './Login.css'; // Importing CSS for styling
import apiUrl from '../config/config';

const Login: React.FC = () => {
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [error, setError] = useState<string>('');
  const [success, setSuccess] = useState<string>('');
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
  
    // Basic validation
    if (!email || !password) {
      setError('Please fill in all fields.');
      return;
    }
  
    setError('');
    setSuccess('');
  
    try {
      // Controleer of de juiste URL wordt gebruikt
      const response = await axios.post(`${apiUrl}/api/User/login`, {
        email,
        password,
      });
  
      // Als de login succesvol is
      localStorage.setItem('token', response.data.token); // Sla het token op in localStorage
      localStorage.setItem('user', JSON.stringify(response.data.user)); // Sla de gebruikersgegevens op in localStorage
      setSuccess('Login successful!');
      navigate('/home'); //naar Homepagina navigeren
      
    } catch (error) {
      // Als er een fout optreedt
      if (axios.isAxiosError(error) && error.response) {
        setError(error.response.data || 'Login failed. Please try again.');
        console.error('Error response:', error.response.data);
      } else {
        setError('An unexpected error occurred. Please try again.');
        console.error('Error:', error);
      }
    }
  };
  
  

  return (
    <form onSubmit={handleLogin}>
      <div className="form-group">
        <label htmlFor="email">E-mail:</label>
        <input
          type="email"
          id="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </div>
      <div className="form-group">
        <label htmlFor="password">Password:</label>
        <input
          type="password"
          id="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </div>
      <button type="submit" data-testid="login-button">Login</button>
      <button type="button" onClick={() => navigate('/register')} className="register-button">
        Go to Register
      </button>
      {error && <div className="error">{error}</div>}
      {success && <div className="success">{success}</div>}
    </form>
  );
};

export default Login;
