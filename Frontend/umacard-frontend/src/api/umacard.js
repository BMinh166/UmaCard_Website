import axios from 'axios';

// Lấy base URL từ biến môi trường Vite hoặc fallback về localhost:5034
const BASE = import.meta.env.VITE_API_URL || 'http://localhost:5034';

// Tạo axios instance. Mở rộng base để trỏ thẳng tới endpoint /api/umacard
const api = axios.create({
  baseURL: `${BASE}/api/umacard`,
  timeout: 10000,
});

// ✅ Lấy danh sách tất cả thẻ
export async function getAllCards() {
  const res = await api.get('/');
  return res.data;
}

// ✅ Lấy chi tiết một thẻ theo ID
export async function getCardById(id) {
  const res = await api.get(`/${id}`);
  return res.data;
}

// ✅ Thêm một thẻ mới
export async function addCard(cardData) {
  const res = await api.post('/', cardData);
  return res.data;
}

// ✅ Xoá một thẻ theo ID
export async function deleteCard(id) {
  const res = await api.delete(`/${id}`);
  return res.data;
}