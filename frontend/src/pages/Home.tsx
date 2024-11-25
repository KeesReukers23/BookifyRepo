import React, { useEffect, useState, useCallback } from 'react';

import './Home.css';

const Home: React.FC = () => {
    const [posts, setPosts] = useState<any[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [firstName, setFirstName] = useState<string | null>(null);
    const [title, setTitle] = useState<string>('');
    const [review, setReview] = useState<string>('');
    const [rating, setRating] = useState<number>(0);
    const [userId, setUserId] = useState<string | null>(null);
    const [token, setToken] = useState<string | null>(null);

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
            const response = await fetch(`http://localhost:5169/api/Post/User/${userId}`, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (!response.ok) {
                throw new Error('Failed to fetch posts');
            }

            const data = await response.json();
            setPosts(data);
        } catch (error) {
            console.error(error);
        } finally {
            setLoading(false);
        }
    },[token, userId]);

    useEffect(() => {
        fetchUserPosts();
    }, [token, userId, fetchUserPosts]);

    const handleCreatePost = async () => {
        if (!token || !userId) {
            console.error("User is not authenticated");
            return;
        }

        try {
            const response = await fetch(`http://localhost:5169/api/Post`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify({ title, review, rating, userId })
            });

            if (!response.ok) {
                throw new Error('Failed to create post');
            }

            setTitle('');
            setReview('');
            setRating(0);
            fetchUserPosts();
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
            const response = await fetch(`http://localhost:5169/api/Post/${postId}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (!response.ok) {
                throw new Error('Failed to delete post');
            }

            // Hier is de wijziging: we gebruiken Number(postId) om het te vergelijken met post.id
            setPosts(posts.filter(post => post.id !== Number(postId)));
        } catch (error) {
            console.error(error);
        }
    };

    const handleRating = (rate: number) => {
        setRating(rate);
    };

    return (
        <div className="container">
            <header>
                <h1>Welcome{firstName ? `, ${firstName}` : ""}!</h1>
            </header>

            <div className="flex-container">
                <section className="new-post-container">
                    <h2>Create a New Post</h2>
                    <div className="form-group">
                        <input 
                            type="text" 
                            placeholder="Book Title" 
                            value={title} 
                            onChange={(e) => setTitle(e.target.value)} 
                            required
                        />
                    </div>
                    <div className="form-group">
                        <textarea 
                            placeholder="What did you think of the book?" 
                            value={review} 
                            onChange={(e) => setReview(e.target.value)} 
                            required
                        />
                    </div>
                    <div className="form-group rating">
                        {[1, 2, 3, 4, 5].map((rate) => (
                            <span 
                                key={rate} 
                                className={`star ${rate <= rating ? 'selected' : ''}`} 
                                onClick={() => handleRating(rate)}
                            >
                                ★
                            </span>
                        ))}
                    </div>
                    <button onClick={handleCreatePost}>Submit</button>
                </section>

                <section className="posts-container">
                    <h1>Your Posts</h1>
                    {loading ? (
                        <p>Loading...</p>
                    ) : (
                        <ul>
                            {posts.map((post) => (
                                <li key={post.id}>
                                    <h2>{post.title}</h2>
                                    <p>{post.review}</p>
                                    <small>Rating: {post.rating}</small>
                                    <button
                                        className="delete-link"
                                        onClick={() => handleDeletePost(post.id.toString())}
                                        aria-label="Delete post"
                                    >
                                        Delete
                                    </button>

                                </li>
                            ))}
                        </ul>
                    )}
                </section>

                <section className="friends-container">
                    <h1>Your Friends</h1>
                    <p>List of friends will be displayed here.</p>
                </section>
            </div>
        </div>
    );
};

export default Home;
