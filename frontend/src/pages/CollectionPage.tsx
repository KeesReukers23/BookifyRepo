import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Navbar from './Navbar';

interface Collection {
    collectionId: string;
    name: string;
    userId: string;
}

const CollectionPage = () => {
    const { collectionId } = useParams<{ collectionId: string }>(); 
    const [collection, setCollection] = useState<Collection | null>(null);
    const [posts, setPosts] = useState<any[]>([]);

    const token = localStorage.getItem('token'); // Haal het token uit de localStorage (of andere plaats waar je het opslaat)


    useEffect(() => {
        const fetchCollection = async () => {
            try {
                const response = await fetch(`http://localhost:5169/api/Collection/${collectionId}`, {
                    method: 'GET',
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json',
                    },
                });

                if (!response.ok) {
                    throw new Error('Fout bij het ophalen van de collectie');
                }

                const data = await response.json();
                setCollection(data); 
            } catch (error) {
                console.error("Er is een fout opgetreden bij het ophalen van de collectie:", error);
            }
        };

        const fetchPosts = async () => {
            try {
                const response = await fetch(`http://localhost:5169/api/Collection/${collectionId}/posts`, {
                    method: 'GET',
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json',
                    },
                });

                if (!response.ok) {
                    throw new Error('Fout bij het ophalen van de posts');
                }

                const data = await response.json();
                setPosts(data); // Stel de posts in
            } catch (error) {
                console.error("Er is een fout opgetreden bij het ophalen van de posts:", error);
            }
        };

        // Roep de fetch-functies aan
        fetchCollection();
        fetchPosts();
    }, [collectionId, token]);

    return (
        <div>
            <Navbar />
            <h1>Posts in Collection: {collection ? collection.name : 'Loading...'}</h1>
            {posts.length === 0 ? (
                <p>No posts available for this collection.</p>
            ) : (
                <ul>
                    {posts.map((post) => (
                        <li key={post.id}>
                            <h3>{post.title}</h3>
                            <p>{post.content}</p>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default CollectionPage;
