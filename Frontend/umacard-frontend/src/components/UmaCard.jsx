import React from 'react';
import { useNavigate } from 'react-router-dom';
import '../styles/UmaCard.css';

function UmaCard({ card }) {
  const navigate = useNavigate();

  return (
    <div className="uma-card" onClick={() => navigate(`/card/${card.id}`)}>
      <img src={`https://localhost:7288${card.imageUrl}`} alt={card.name} />
      <h3>{card.name}</h3>
      <p><strong>Outfit Type:</strong> {card.outfitType}</p>
      <p><strong>Type:</strong> {card.type}</p>
      <p><strong>Description:</strong> {card.description}</p>
    </div>
  );
}

export default UmaCard;