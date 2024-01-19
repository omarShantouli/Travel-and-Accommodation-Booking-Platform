using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a booking conflict is detected.
    /// </summary>
    public class BookingConflictException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingConflictException"/> class.
        /// </summary>
        public BookingConflictException() : base("Booking conflict detected.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingConflictException"/> class with a specific error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public BookingConflictException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingConflictException"/> class with a specific error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public BookingConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
