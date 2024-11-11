import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './Register.css'; // Importing CSS for styling
import axios from 'axios';

const SignUpForm: React.FC = () => {
  const [firstName, setFirstName] = useState<string>('');
  const [lastName, setLastName] = useState<string>('');
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [error, setError] = useState<string>('');
  const [success, setSuccess] = useState<string>('');
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    // Basic validation
    if (!firstName || !lastName || !email || !password) {
        setError('Please fill in all fields.');
        console.log('Validation failed: ', { firstName, lastName, email, password });
        return;
    }

    setError('');
    setSuccess('');
    console.log('Submitting form...', { firstName, lastName, email, password });

    try {
        const response = await axios.post('https://localhost:7157/register', {
            firstName,
            lastName,
            email,
            password,
        });

        // Als de registratie succesvol is
        setSuccess('Registration successful!');
        console.log('Response:', response.data);

        
    } catch (error) {
        // Als er een fout optreedt
        if (axios.isAxiosError(error) && error.response) {
            setError(error.response.data || 'Registration failed. Please try again.');
            console.error('Error response:', error.response.data);
        } else {
            setError('An unexpected error occurred. Please try again.');
            console.error('Error:', error);
        }
    }
};

  return (
    <form onSubmit={handleSubmit}>
      <div className="form-group">
        <label htmlFor="firstName">First Name:</label>
        <input
          type="text"
          id="firstName"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
        />
      </div>
      <div className="form-group">
        <label htmlFor="lastName">Last Name:</label>
        <input
          type="text"
          id="lastName"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
        />
      </div>
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
      <button type="submit">Register</button>
      <button type="button" onClick={() => navigate('/login')} className="login-button">
        Go to Login
      </button>
      {error && <div className="error">{error}</div>}
      {success && <div className="success">{success}</div>}
    </form>
  );
};

export default SignUpForm;