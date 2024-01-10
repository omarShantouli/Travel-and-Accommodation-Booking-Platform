using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform.Models;
using Travel_and_Accommodation_Booking_Platform.Token;
using Travel_and_Accommodation_Booking_Platform.Validators;
using static Domain.Interfaces.IRepository;

namespace Travel_and_Accommodation_Booking_Platform.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related actions.
    /// </summary>
    [ApiController]
    [Route("api/Authentication")]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Represents the request body structure for authentication.
        /// </summary>
        public class AuthenticationRequestBody
        {
            /// <summary>
            /// Gets or sets the user's email.
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the user's password.
            /// </summary>
            public string Password { get; set; }
        }

        private readonly JwtTokenGenerator _tokenGenerator;
        private string _token = "";
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="appUserRepository">The repository for <see cref="AppUser"/>.</param>
        public AuthenticationController(IConfiguration configuration, IRepository<AppUser> appUserRepository
            , ILogger<AuthenticationController> logger)
        {
            _configuration = configuration;
            _tokenGenerator = new JwtTokenGenerator(configuration, appUserRepository);
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user based on the provided credentials.
        /// </summary>
        /// <param name="authenticationRequestBody">The request body containing user credentials.</param>
        /// <returns>An action result containing the authentication token or an error response.</returns>
        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            var validator = new AuthenticationRequestBodyValidator();
            var results = validator.Validate(authenticationRequestBody);

            if (!results.IsValid)
            {
                List<ErrorModel> errors = new List<ErrorModel>();
                foreach (var failure in results.Errors)
                {
                    errors.Add(new ErrorModel()
                    {
                        FieldName = failure.PropertyName,
                        Message = failure.ErrorMessage
                    });
                    Console.WriteLine(failure.ErrorMessage);
                }
                return BadRequest(errors);
            }

            var user = _tokenGenerator.ValidateUserCredentials(authenticationRequestBody.Email, authenticationRequestBody.Password);

            if (user == null)
                return Unauthorized($"User with email {authenticationRequestBody.Email} is Not Authorized");

            var secretKey = _configuration["Authentication:SecretForKey"];
            var issuer = _configuration["Authentication:Issuer"];
            var audience = _configuration["Authentication:Audience"];
            var token = _tokenGenerator.GenerateToken(user.Email, user.PasswordHash, secretKey, issuer, audience, user.Role);

            return Ok(token);
        }

    }
}
