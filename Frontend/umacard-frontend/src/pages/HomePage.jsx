import React, { useEffect, useState } from 'react';
import { getAllCards } from '../api/umacard';
import UmaCard from '../components/UmaCard';
import { useNavigate } from 'react-router-dom';
import '../styles/HomePage.css';

function HomePage() {
  const [cards, setCards] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    getAllCards()
      .then(setCards)
      .catch(err => console.error(err.message));
  }, []);

  return (
    <div>
  <h1 className="homepage-header">Danh sách UmaCard</h1>
  <button className="add-card-btn" onClick={() => navigate('/add')}>Thêm thẻ mới</button>
      <div style={{ display: 'flex', flexWrap: 'wrap' }}>
        {cards.map(card => (
          <UmaCard key={card.id} card={card} />
        ))}
      </div>
    </div>
  );
}

export default HomePage;