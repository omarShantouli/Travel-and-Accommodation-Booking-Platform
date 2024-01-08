using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class PaymentsRepository : IRepository<Payments>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public PaymentsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Payments> GetByIdAsync(Guid id)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
            {
                throw new EntityNotFoundException($"Payment with ID {id} not found.");
            }

            return payment;
        }

        public IEnumerable<Payments> GetAll()
        {
            return _context.Payments;
        }

        public async Task<Payments> CreateAsync(Payments payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task UpdateAsync(Payments payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            var existingPayment = await _context.Payments.FirstOrDefaultAsync(p => p.Id == payment.Id);

            if (existingPayment == null)
            {
                throw new EntityNotFoundException($"Payment with ID {payment.Id} not found.");
            }

            existingPayment.BookingId = payment.BookingId;
            existingPayment.Method = payment.Method;
            existingPayment.Status = payment.Status;
            existingPayment.Amount = payment.Amount;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var paymentToRemove = _context.Payments.FirstOrDefault(p => p.Id == id);

            if (paymentToRemove == null)
            {
                throw new EntityNotFoundException($"Payment with ID {id} not found.");
            }

            _context.Payments.Remove(paymentToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
