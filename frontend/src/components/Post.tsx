import React from 'react';
import './Post.css';

// Definieer de interface voor de props van het PostComponent
interface PostProps {
  postId: string;
  title: string;
  review: string;
  rating: number; 
  onDelete: (id: string) => void; 
  onAddToCollection: (id: string) => void; 
}

const Post: React.FC<PostProps> = ({ postId, title, review, rating, onDelete, onAddToCollection }) => {
  // Functie om sterren weer te geven op basis van de rating
  const renderStars = (rating: number) => {
    const stars = [];
    for (let i = 0; i < 5; i++) {
      stars.push(
        <span key={i} className={i < rating ? 'filled' : 'empty'}>
          ★
        </span>
      );
    }
    return stars;
  };

  return (
    <div className="post">
      <h2>{title}</h2>
      <p>{review}</p>
      <div className="rating">{renderStars(rating)}</div>
      <div className="action-buttons-container">
        <button onClick={() => onDelete(postId)} className="delete-button">
            Delete
        </button>
        <button onClick={() => onAddToCollection(postId)} className="add-to-collection-button">
            Add to Collection
        </button>
      </div>
    </div>
  );
};

export default Post;
export {};
