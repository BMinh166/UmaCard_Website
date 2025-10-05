import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import UmaDetails from '../components/UmaDetails';
import { getCardById } from '../api/umacard';

function UmaPages() {
  const { id } = useParams();
  const [card, setCard] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function fetchCard() {
      try {
        const data = await getCardById(id);
        setCard(data);
      } catch (err) {
        setError(err.message);
      }
    }

    fetchCard();
  }, [id]);

  if (error) return <p className="error">{error}</p>;
  if (!card) return <p className="loading">Đang tải dữ liệu thẻ...</p>;

  return <UmaDetails card={card} />;
}

export default UmaPages;