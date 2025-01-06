import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import './Collections.css';
import Navbar from './Navbar';
import apiUrl from '../config/config';

interface Collection {
    collectionId: string;
    name: string;
    userId: string;
}

const Collections = () => {
    const [collections, setCollections] = useState<Collection[]>([]);
    const [newCollectionName, setNewCollectionName] = useState<string>('');
    const [creatingCollection, setCreatingCollection] = useState<boolean>(false);
    const [loading, setLoading] = useState<boolean>(true);
    const [userId, setUserId] = useState<string | null>(null);
    const [token, setToken] = useState<string | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        const storedToken = localStorage.getItem('token');
        const storedUser = localStorage.getItem('user');

        if (storedToken) setToken(storedToken);
        if (storedUser) {
            const parsedUser = JSON.parse(storedUser);
            setUserId(parsedUser.userId); 
        }
    }, []);

    useEffect(() => {
        if (!token || !userId) {
            setLoading(false);
            return;
        }

        const fetchCollections = async () => {
            try {
                const response = await fetch(`${apiUrl}/api/Collection/byUser/${userId}`, {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                    },
                });

                if (!response.ok) throw new Error('Failed to fetch collections');
                const data: Collection[] = await response.json();
                setCollections(data);
            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        };

        if (token) {
            fetchCollections();
        } else {
            setLoading(false);
        }
    }, [token, userId]);

    const handleCreateCollection = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!newCollectionName.trim()) return;

        if (!token || !userId) return;

        setCreatingCollection(true);
        try {
            const response = await fetch(`${apiUrl}/api/Collection`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                },
                body: JSON.stringify({ name: newCollectionName, userId }),
            });

            if (!response.ok) throw new Error('Failed to create collection');
            const newCollection: Collection = await response.json();
            setCollections([...collections, newCollection]);
            setNewCollectionName('');
        } catch (error) {
            console.error(error);
        } finally {
            setCreatingCollection(false);
        }
    };

    const handleViewCollection = (collectionId: string) => {
        navigate(`/collections/${collectionId}`);
    };

    const handleDeleteCollection = async (collectionId: string) => {
        if (!token) return;

        try {
            const response = await fetch(`${apiUrl}/api/Collection/${collectionId}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`,
                },
            });

            if (!response.ok) throw new Error('Failed to delete collection');
            setCollections(collections.filter(collection => collection.collectionId !== collectionId));
        } catch (error) {
            console.error(error);
        }
    };

    if (loading) return <div>Loading...</div>;

    return (
        <div>
            <Navbar />
            <div className="collections-container">
                <h1>Collections</h1>
                <form onSubmit={handleCreateCollection} className="create-collection-form">
                    <input
                        type="text"
                        value={newCollectionName}
                        onChange={(e) => setNewCollectionName(e.target.value)}
                        placeholder="New collection name"
                    />
                    <button type="submit" disabled={creatingCollection}>
                        {creatingCollection ? 'Creating...' : 'Create Collection'}
                    </button>
                </form>
            </div>
            <div className="collections-table-container">
                <table className="collections-table">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {collections.map((collection) => (
                            <tr key={collection.collectionId}>
                                <td>{collection.name}</td>
                                <td className="actions">
                                    <button onClick={() => handleViewCollection(collection.collectionId)}>View</button>
                                    <button onClick={() => handleDeleteCollection(collection.collectionId)}>Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default Collections;