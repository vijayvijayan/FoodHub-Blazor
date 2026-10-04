using Microsoft.EntityFrameworkCore;
using FoodHub.Data;
using FoodHub.Repository.IRepository;

namespace FoodHub.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<OrderHeader> CreateAsync(OrderHeader orderHeader)
        {
            orderHeader.OrderDate = DateTime.Now;
            await _db.OrderHeader.AddAsync(orderHeader);
            await _db.SaveChangesAsync();
            return orderHeader;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId = null)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await _db.OrderHeader.Where(i => i.UserId == userId).ToListAsync();
            }
            return await _db.OrderHeader.ToListAsync();
        }

        public async Task<OrderHeader> GetAsync(int id)
        {
           return await _db.OrderHeader.Include(i=>i.OrderDetails).FirstOrDefaultAsync(u=>u.Id==id);
        }

        public async Task<OrderHeader> UpdateStatusAsync(int orderId, string status)
        {
            var orderHeader = await _db.OrderHeader.FirstOrDefaultAsync(u=>u.Id==orderId);
            if (orderHeader != null)
            { 
                orderHeader.Status= status;
                await _db.SaveChangesAsync();
            }
            return orderHeader;
        }
    }
}
