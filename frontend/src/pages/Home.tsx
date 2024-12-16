import React, { useEffect, useState, useCallback } from 'react';
import './Home.css';
import Navbar from './Navbar';
import Post from '../components/Post';

const Home: React.FC = () => {
    const [posts, setPosts] = useState<any[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [firstName, setFirstName] = useState<string | null>(null);
    const [title, setTitle] = useState<string>('');
    const [review, setReview] = useState<string>('');
    const [rating, setRating] = useState<number>(0);
    const [userId, setUserId] = useState<string | null>(null);
    const [token, setToken] = useState<string | null>(null);
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

    // Gebruik useCallback om de fetchUserPosts functie stabiel te maken
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
                    'Authorization': `Bearer ${token}`,
                },
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
    }, [token, userId]);

    // De useEffect afhankelijkheden moeten alleen token en userId zijn
    useEffect(() => {
        if (userId && token) {
            fetchUserPosts(); // Deze functie wordt alleen uitgevoerd als token en userId aanwezig zijn
        }
    }, [token, userId, fetchUserPosts]);


    const validateForm = () => {
        if (title === '' || review === '' || rating === 0) {
            setFormError('Please fill in all fields');
            return false;
        }
        setFormError('');
        return true;
    };

    // Create a new post
    const handleCreatePost = async () => {
        if (!token || !userId) {
            console.error("User is not authenticated");
            return;
        }

        if (!validateForm()) {
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

            const newPost = await response.json();
            console.log("New Post created: ", newPost);

            // Voeg de nieuwe post toe aan de begin van de lijst
            setPosts(prevPosts => {
                const updatedPosts = [newPost, ...prevPosts];
                console.log("Updated posts after new post: ", updatedPosts);  // Log de bijgewerkte lijst
                return updatedPosts;
            });

            // Reset input fields
            setTitle('');
            setReview('');
            setRating(0);
        } catch (error) {
            console.error(error);
        }
    };

    // Delete a post
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

            setPosts(posts.filter(post => post.postId !== postId));
        } catch (error) {
            console.error(error);
        }
    };

    // Rating
    const handleRating = (rate: number) => {
        setRating(rate);
    };

    // Page layout
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
                        {formError && <p className="error">{formError}</p>} {/* Foutmelding als er een validatiefout is */}
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
                                    <li key={post.postId}>
                                        <Post
                                            postId={post.postId}
                                            title={post.title}
                                            review={post.review}
                                            rating={post.rating}
                                            onDelete={() => { handleDeletePost(post.postId); }}
                                            onAddToCollection={() => { console.log('Add to collection'); }}
                                        />
                                    </li>
                                ))}
                            </ul>
                        )}
                    </section>
                </div>
            </div>
        </div>
    );
};

export default Home;