import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './UsersPage.css'; // Importeer de CSS
import apiUrl from '../config/config';

// Definieer het type voor een gebruiker
interface User {
  email: string;
  firstName: string;
  lastName: string;
}

const UsersPage: React.FC = () => {
  // State voor de lijst van gebruikers
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  // Haal de gebruikers op wanneer de component wordt geladen
  useEffect(() => {
    const fetchUsers = async () => {
      try {
        // Gebruik een API-aanroep om de gebruikers op te halen
        const response = await axios.get(`${apiUrl}/api/User`);
        setUsers(response.data);
        setLoading(false);
      } catch (err) {
        setError('Er is een fout opgetreden bij het laden van de gebruikers.');
        setLoading(false);
      }
    };

    fetchUsers();
  }, []);

  // Functie om een gebruiker te verwijderen (voorbeeld)
  const handleDelete = (email: string) => {
    if (window.confirm(`Weet je zeker dat je de gebruiker met email ${email} wilt verwijderen?`)) {
      // Voeg hier logica toe om de gebruiker te verwijderen via de API
      alert(`Gebruiker ${email} verwijderd`);
      setUsers(users.filter((user) => user.email !== email)); // Verwijder de gebruiker uit de lijst
    }
  };

  // Als de gegevens nog worden geladen of er is een fout
  if (loading) return <div className="loading">Loading...</div>;
  if (error) return <div className="error">{error}</div>;

  return (
    <div className="container">
      <h1>User List</h1>
      <table>
        <thead>
          <tr>
            <th>Email</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr key={user.email}>
              <td>{user.email}</td>
              <td>{user.firstName}</td>
              <td>{user.lastName}</td>
              <td>
                <button className="action-button delete" onClick={() => handleDelete(user.email)}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UsersPage;
