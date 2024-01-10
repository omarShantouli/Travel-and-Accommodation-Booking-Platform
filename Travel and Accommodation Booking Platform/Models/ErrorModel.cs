namespace Travel_and_Accommodation_Booking_Platform.Models
{
    /// <summary>
    /// Represents an error model with information about a specific field and associated error message.
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// Gets or sets the name of the field associated with the error.
        /// </summary>
        public string? FieldName { get; set; }

        /// <summary>
        /// Gets or sets the error message for the field.
        /// </summary>
        public string? Message { get; set; }
    }
}
