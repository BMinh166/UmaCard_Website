import axios from 'axios';

// Upload ảnh lên backend, trả về đường dẫn ảnh
const BASE = import.meta.env.VITE_API_URL || 'http://localhost:5034';

export async function uploadImage(file) {
  const formData = new FormData();
  formData.append('file', file);

  const client = axios.create({ baseURL: `${BASE}/api/umacard`, timeout: 20000 });

  const res = await client.post('/upload', formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });

  // Giả sử backend trả về đường dẫn ảnh (string) hoặc object { url }
  return res.data;
}

// ...existing code...
