import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Navbar from './Navbar';
import './CollectionPage.css';
import apiUrl from '../config/config';

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
                const response = await fetch(`${apiUrl}/api/Collection/${collectionId}`, {
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
                console.error(error);
            }
        };

        const fetchPosts = async () => {
            try {
                const response = await fetch(`${apiUrl}/api/Collection/${collectionId}/posts`, {
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
                setPosts(data);
            } catch (error) {
                console.error(error);
            }
        };

        fetchCollection();
        fetchPosts();
    }, [collectionId, token]);

    const renderStars = (rating: number) => {
        return (
            <div className="rating">
                {[1, 2, 3, 4, 5].map((rate) => (
                    <span key={rate} className={`star ${rate <= rating ? 'selected' : ''}`}>
                        ★
                    </span>
                ))}
            </div>
        );
    };

    const handleDeletePostFromCollection = async (postId: string) => {

        if (!token) {
            console.error("User is not authenticated");
            return;
        }

        try {
            const response = await fetch(`${apiUrl}/api/Collection/${collectionId}/post/${postId}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json',
                },
            });

            if (!response.ok) {
                throw new Error('Failed to delete post from collection');
            }

            setPosts(posts.filter(post => post.postId !== postId));
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div>
            <Navbar />
            <div className="container">
                <header>
                    <h1>{collection ? collection.name : 'Loading...'}</h1>
                </header>

                <section className="posts-container">
                    <h2>Posts in Collection</h2>
                    {posts.length === 0 ? (
                        <p>No posts available for this collection.</p>
                    ) : (
                        <ul>
                            {posts.map((post) => (
                                <li key={post.postId} className="post-item">
                                    <h3>{post.title}</h3>
                                    <p>{post.review}</p>
                                    {renderStars(post.rating)}
                                    <div className="button-container">
                                        <button className="button-delete" onClick={() => handleDeletePostFromCollection(post.postId)}>Delete</button>
                                    </div>
                                </li>
                            ))}
                        </ul>
                    )}
                </section>
            </div>
        </div>
    );
};

export default CollectionPage;
