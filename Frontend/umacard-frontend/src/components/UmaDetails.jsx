import React from 'react';
import '../styles/UmaDetail.css';

function UmaDetail({ card }) {
  if (!card) return <p>Không có dữ liệu để hiển thị.</p>;
  return (
    <div className="uma-detail">
      {(() => {
        const BASE = import.meta.env.VITE_API_URL || 'http://localhost:5034';
        const src = card.imageUrl
          ? card.imageUrl.startsWith('http')
            ? card.imageUrl
            : `${BASE}${card.imageUrl}`
          : '';
        return <img src={src} alt={card.name} />;
      })()}
      <h2>{card.name}</h2>
      <p><strong>Loại:</strong> {card.type}</p>
      <p><strong>Trang phục:</strong> {card.outfitType}</p>
      <p><strong>Mô tả:</strong> {card.description}</p>
    </div>
  );
}

export default UmaDetail;