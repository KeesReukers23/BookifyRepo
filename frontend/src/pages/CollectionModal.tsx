import React, { useEffect, useState } from 'react';
import './CollectionModal.css';

interface Collection {
    collectionId: string;
    name: string;
}

interface CollectionModalProps {
    show: boolean;
    onClose: () => void;
    onAddToCollection: (collectionId: string) => void;
}

const CollectionModal: React.FC<CollectionModalProps> = ({ show, onClose, onAddToCollection }) => {
    const [collections, setCollections] = useState<Collection[]>([]);
    const token = localStorage.getItem('token');
    const user = localStorage.getItem('user');
    const userId = user ? JSON.parse(user).userId : null;
    

    useEffect(() => {
        const fetchCollections = async () => {
            if (!token || !userId) return;

            try {
                const response = await fetch(`http://localhost:5169/api/Collection/byUser/${userId}`, {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                    },
                });

                if (!response.ok) throw new Error('Failed to fetch collections');
                const data: Collection[] = await response.json();
                setCollections(data);
            } catch (error) {
                console.error(error);
            }
        };

        if (show) {
            fetchCollections();
        }
    }, [show, token, userId]);

    if (!show) {
        return null;
    }

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                <h2>Select a Collection</h2>
                <ul>
                    {collections.length > 0 ? (
                        collections.map((collection) => (
                            <li key={collection.collectionId}>
                                <button onClick={() => onAddToCollection(collection.collectionId)}>
                                    {collection.name}
                                </button>
                            </li>
                        ))
                    ) : (
                        <p>No collections available.</p>
                    )}
                </ul>
                <button onClick={onClose}>Close</button>
            </div>
        </div>
    );
};

export default CollectionModal;