// Upload ảnh lên backend, trả về đường dẫn ảnh
export async function uploadImage(file) {
  const formData = new FormData();
  formData.append('file', file);
  const response = await fetch('https://localhost:7288/api/umacard/upload', {
    method: 'POST',
    body: formData
  });
  if (!response.ok) throw new Error('Upload ảnh thất bại');
  return await response.text(); // hoặc response.json() tuỳ backend trả về
}

// ...existing code...
