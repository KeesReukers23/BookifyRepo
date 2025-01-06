import React, { useState, useEffect, useCallback } from 'react';
import './Home.css';
import Navbar from './Navbar';
import CollectionModal from './CollectionModal';
import apiUrl from '../config/config';
import axios from 'axios';

const Home = () => {
    const [firstName, setFirstName] = useState<string | null>(null);
    const [userId, setUserId] = useState<string | null>(null);
    const [token, setToken] = useState<string | null>(null);
    const [posts, setPosts] = useState<any[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [showModal, setShowModal] = useState<boolean>(false);
    const [selectedPostId, setSelectedPostId] = useState<string | null>(null);
    const [title, setTitle] = useState<string>('');
    const [review, setReview] = useState<string>('');
    const [rating, setRating] = useState<number>(0);
    const [formError, setFormError] = useState<string>('');

    useEffect(() => {
        const userData = localStorage.getItem('user');
        const storedToken = localStorage.getItem('token');
        if (userData) {
            const user = JSON.parse(userData);
            setFirstName(user.firstName);
            setUserId(user.userId);
        }
        if (storedToken) {
            setToken(storedToken);
        }
    }, []);

    const fetchUserPosts = useCallback(async () => {
        if (!token || !userId) {
            console.error("User is not authenticated");
            return;
        }
    
        setLoading(true);
        try {
            const response = await axios.get(`${apiUrl}/api/Post/User/${userId}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
    
            setPosts(response.data); // axios plaatst de response data standaard onder 'data'
        } catch (error) {
            console.error('Failed to fetch posts', error);
        } finally {
            setLoading(false);
        }
    }, [token, userId]);

    useEffect(() => {
        if (token && userId) {
            fetchUserPosts();
        }
    }, [token, userId, fetchUserPosts]);
    
    const handleAddToCollection = (postId: string) => {
        setSelectedPostId(postId);
        setShowModal(true);
    };

    const handleCloseModal = () => {   
        setShowModal(false);
        setSelectedPostId(null);
    };

    const handleAddPostToCollection = async (collectionId: string) => {
        if (!selectedPostId || !token) return;

        try {
            const response = await fetch(`${apiUrl}/api/Collection/${collectionId}/post`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                },
                body: JSON.stringify({ collectionId, postId: selectedPostId }),
            });

            if (!response.ok) {
                throw new Error('Failed to add post to collection');
            }

            handleCloseModal();
        } catch (error) {
            console.error(error);
        }
    };

    const validateForm = () => {
        if (title === '' || review === '' || rating === 0) {
            setFormError('Please fill in all fields');
            return false;
        }
        setFormError('');
        return true;
    };

    const handleCreatePost = async () => {
        if (!token || !userId) {
            console.error("User is not authenticated");
            return;
        }

        if (!validateForm()) {
            return;
        }

        try {
            const response = await fetch(`${apiUrl}/api/Post`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                },
                body: JSON.stringify({ title, review, rating, userId }),
            });

            if (!response.ok) {
                throw new Error('Failed to create post');
            }

            const newPost = await response.json();
            setPosts([...posts, newPost]);
            setTitle('');
            setReview('');
            setRating(0);
        } catch (error) {
            console.error(error);
        }
    };

    const handleDeletePost = async (postId: string) => {
        if (!token) {
            console.error("User is not authenticated");
            return;
        }

        try {
            const response = await fetch(`${apiUrl}/api/Post/${postId}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (!response.ok) {
                throw new Error('Failed to delete post');
            }

            setPosts(posts.filter(post => post.postId !== postId));
        } catch (error) {
            console.error(error);
        }
    };

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
    
    return (
        <div>
            <Navbar />
            <div className="container">
                <header>
                    <h1>Welcome{firstName ? `, ${firstName}` : ""}!</h1>
                </header>

                <div className="flex-container">
                    <section className="new-post-container">
                        <h2>Create a New Post</h2>
                        {/* Form for creating a new post */}
                        <div className="form-group">
                            <input
                                type="text"
                                placeholder="Title"
                                value={title}
                                onChange={(e) => setTitle(e.target.value)}
                            />
                        </div>
                        <div className="form-group">
                            <textarea
                                placeholder="Review"
                                value={review}
                                onChange={(e) => setReview(e.target.value)}
                            />
                        </div>
                        {formError && <p className="error">{formError}</p>}
                        <div className="form-group rating">
                            {[1, 2, 3, 4, 5].map((rate) => (
                                <span
                                    key={rate}
                                    className={`star ${rate <= rating ? 'selected' : ''}`}
                                    onClick={() => setRating(rate)}
                                >
                                    ★
                                </span>
                            ))}
                        </div>
                        <button onClick={handleCreatePost}>Submit</button>
                    </section>

                    <section className="posts-container">
                        <h2>Your Posts</h2>
                        {loading ? (
                            <p>Loading...</p>
                        ) : (
                            <ul>
                                {posts.map((post) => (
                                    <li key={post.postId} className="post-item">
                                        <h3>{post.title}</h3>
                                        <p>{post.review}</p>
                                        {renderStars(post.rating)}
                                        <div className="button-container">
                                            <button className="button-add" onClick={() => handleAddToCollection(post.postId)}>Add to Collection</button>
                                            <button className="button-delete" onClick={() => handleDeletePost(post.postId)}>Delete</button>
                                        </div>
                                    </li>
                                ))}
                            </ul>
                        )}
                    </section>
                </div>
            </div>
            <CollectionModal
                show={showModal}
                onClose={handleCloseModal}
                onAddToCollection={handleAddPostToCollection}
            />
        </div>
    );
};

export default Home;