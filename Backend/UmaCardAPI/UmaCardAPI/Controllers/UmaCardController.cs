using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UmaCardAPI.Data;
using UmaCardAPI.Models;

namespace UmaCardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UmaCardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UmaCardController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Lấy danh sách tất cả thẻ
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UmaCard>>> GetAllCards()
        {
            var cards = await _context.UmaCards.ToListAsync();
            return Ok(cards);
        }

        // ✅ Lấy chi tiết một thẻ theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UmaCard>> GetCardById(int id)
        {
            var card = await _context.UmaCards.FindAsync(id);
            if (card == null)
                return NotFound(new { message = "Không tìm thấy thẻ." });

            return Ok(card);
        }

        // ✅ Thêm một thẻ mới
        [HttpPost]
        public async Task<ActionResult<UmaCard>> AddCard([FromBody] UmaCard newCard)
        {
            if (newCard == null || string.IsNullOrWhiteSpace(newCard.Name))
                return BadRequest(new { message = "Dữ liệu thẻ không hợp lệ." });

            _context.UmaCards.Add(newCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCardById), new { id = newCard.Id }, newCard);
        }

        // ✅ Xoá một thẻ theo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {   
            var card = await _context.UmaCards.FindAsync(id);
            if (card == null)
                return NotFound(new { message = "Không tìm thấy thẻ cần xoá." });

            _context.UmaCards.Remove(card);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Đã xoá thẻ thành công!", deletedId = id });
        }

        // ✅ Upload ảnh và lưu vào wwwroot/uploads
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File không hợp lệ.");

            // Tạo đường dẫn lưu ảnh
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về đường dẫn ảnh để frontend hiển thị
            var imageUrl = $"/uploads/{fileName}";
            return Ok(imageUrl);
        }
    }
}
