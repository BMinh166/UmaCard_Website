import React from 'react';
import '../styles/UmaDetail.css';

function UmaDetail({ card }) {
  if (!card) return <p>Không có dữ liệu để hiển thị.</p>;

  return (
    <div className="uma-detail">
      <img src={`https://localhost:7288${card.imageUrl}`} alt={card.name} />
      <h2>{card.name}</h2>
      <p><strong>Loại:</strong> {card.type}</p>
      <p><strong>Trang phục:</strong> {card.outfitType}</p>
      <p><strong>Mô tả:</strong> {card.description}</p>
    </div>
  );
}

export default UmaDetail;