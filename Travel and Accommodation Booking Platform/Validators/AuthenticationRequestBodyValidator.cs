using FluentValidation;
using Travel_and_Accommodation_Booking_Platform.Controllers;
using static Travel_and_Accommodation_Booking_Platform.Controllers.AuthenticationController;

namespace Travel_and_Accommodation_Booking_Platform.Validators
{
    /// <summary>
    /// Validator for <see cref="AuthenticationRequestBody"/>.
    /// </summary>
    public class AuthenticationRequestBodyValidator : AbstractValidator<AuthenticationRequestBody>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationRequestBodyValidator"/> class.
        /// </summary>
        public AuthenticationRequestBodyValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
