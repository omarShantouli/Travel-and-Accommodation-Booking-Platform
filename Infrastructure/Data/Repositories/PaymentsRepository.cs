using Domain.Entities;
using Domain.Exceptions;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Data.Repositories
{
    public class PaymentsRepository : IRepository<Payments>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public PaymentsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Payments GetById(Guid id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == id);

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

        public void Create(Payments payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void Update(Payments payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            var existingPayment = _context.Payments.FirstOrDefault(p => p.Id == payment.Id);

            if (existingPayment == null)
            {
                throw new EntityNotFoundException($"Payment with ID {payment.Id} not found.");
            }

            existingPayment.BookingId = payment.BookingId;
            existingPayment.Method = payment.Method;
            existingPayment.Status = payment.Status;
            existingPayment.Amount = payment.Amount;

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var paymentToRemove = _context.Payments.FirstOrDefault(p => p.Id == id);

            if (paymentToRemove == null)
            {
                throw new EntityNotFoundException($"Payment with ID {id} not found.");
            }

            _context.Payments.Remove(paymentToRemove);
            _context.SaveChanges();
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
