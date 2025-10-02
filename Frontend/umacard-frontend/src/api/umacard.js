const API_URL = 'https://localhost:7288/api/umacard'; // Đổi cổng nếu backend bạn chạy khác

// ✅ Lấy danh sách tất cả thẻ
export async function getAllCards() {
  const response = await fetch(API_URL);
  if (!response.ok) throw new Error('Không thể lấy danh sách thẻ');
  return await response.json();
}

// ✅ Lấy chi tiết một thẻ theo ID
export async function getCardById(id) {
  const response = await fetch(`${API_URL}/${id}`);
  if (!response.ok) throw new Error('Không tìm thấy thẻ');
  return await response.json();
}

// ✅ Thêm một thẻ mới
export async function addCard(cardData) {
  const response = await fetch(API_URL, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(cardData)
  });
  if (!response.ok) throw new Error('Thêm thẻ thất bại');
  return await response.json();
}

// ✅ Xoá một thẻ theo ID
export async function deleteCard(id) {
  const response = await fetch(`${API_URL}/${id}`, {
    method: 'DELETE'
  });
  if (!response.ok) throw new Error('Xoá thẻ thất bại');
  return await response.json();
}