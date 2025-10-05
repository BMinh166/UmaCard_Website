import React from 'react';
import { useNavigate } from 'react-router-dom';
import '../styles/UmaCard.css';

function UmaCard({ card }) {
  const navigate = useNavigate();
  const BASE = import.meta.env.VITE_API_URL || 'http://localhost:5034';
  const imgSrc = card?.imageUrl
    ? card.imageUrl.startsWith('http')
      ? card.imageUrl
      : `${BASE}${card.imageUrl}`
    : '';

  return (
    <div className="uma-card" onClick={() => navigate(`/card/${card.id}`)}>
  <img src={imgSrc} alt={card.name} />
      <h3>{card.name}</h3>
      <p><strong>Outfit Type:</strong> {card.outfitType}</p>
      <p><strong>Type:</strong> {card.type}</p>
      <p><strong>Description:</strong> {card.description}</p>
    </div>
  );
}

export default UmaCard;