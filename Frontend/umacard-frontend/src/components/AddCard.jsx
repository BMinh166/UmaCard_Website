import React, { useState } from 'react';
import { addCard } from '../api/umacard';
import { uploadImage } from '../api/upload';
import '../styles/AddCard.css';
import { useNavigate } from 'react-router-dom';

function AddCard() {
  const [form, setForm] = useState({
    name: '',
    outfitType: '',
    type: '',
    description: '',
    imageUrl: ''
  });
  const [imageFile, setImageFile] = useState(null);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleChange = e => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleFileChange = e => {
    setImageFile(e.target.files[0]);
  };

  const handleSubmit = async e => {
    e.preventDefault();
    setError('');
    try {
      let imageUrl = form.imageUrl;
      if (imageFile) {
        imageUrl = await uploadImage(imageFile); // trả về đường dẫn ảnh
      }
      await addCard({ ...form, imageUrl });
      navigate('/');
    } catch (err) {
      setError(err.message || 'Thêm thẻ thất bại');
    }
  };

  return (
    <div className="add-card-form">
      <h2>Thêm UmaCard mới</h2>
      {error && <p style={{color: 'red'}}>{error}</p>}
      <form onSubmit={handleSubmit}>
        <div>
          <label>Tên:</label>
          <input name="name" value={form.name} onChange={handleChange} required />
        </div>
        <div>
          <label>Outfit Type:</label>
          <input name="outfitType" value={form.outfitType} onChange={handleChange} required />
        </div>
        <div>
          <label>Type:</label>
          <input name="type" value={form.type} onChange={handleChange} required />
        </div>
        <div>
          <label>Description:</label>
          <textarea name="description" value={form.description} onChange={handleChange} required />
        </div>
        <div>
          <label>Chọn ảnh:</label>
          <input type="file" accept="image/*" onChange={handleFileChange} />
        </div>
        <div>
          <label>Hoặc nhập URL ảnh (nếu có):</label>
          <input name="imageUrl" value={form.imageUrl} onChange={handleChange} />
        </div>
        <button type="submit">Thêm thẻ</button>
      </form>
    </div>
  );
}

export default AddCard;
