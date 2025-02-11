using LibraryWebApp.DataAccess.Data;
using LibraryWebApp.DataAccess.Repository.IRepository;
using LibraryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebApp.DataAccess.Repository
{
    // Repository pentru operațiile pe entitatea OrderHeader
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db; // Contextul bazei de date

        // Constructor ce primește ApplicationDbContext
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        // Actualizează un OrderHeader existent
        public void Update(OrderHeader orderHeader)
        {
            _db.OrderHeaders.Update(orderHeader); // Actualizează în baza de date
        }

        // Actualizează statusul comenzii și statusul plății
        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDb.PaymentStatus = paymentStatus; // Actualizează și statusul plății
                }
            }
        }

        // Actualizează ID-ul sesiuni Stripe și PaymentIntent
        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderFromDb.SessionId = sessionId; // Setează sessionId
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                orderFromDb.PaymentIntentId = paymentIntentId; // Setează PaymentIntentId
                orderFromDb.PaymentDate = DateTime.Now; // Actualizează data plății
            }
        }
    }
}
