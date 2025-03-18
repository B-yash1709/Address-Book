using FluentValidation;
using ModelLayer.Model;

namespace BusinessLayer.Validators
{
    public class AddressBookValidator : AbstractValidator<RequestModel>
    {
        public AddressBookValidator()
        {
            // Name Validation
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name must be less than 50 characters");

            // Email Validation
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            // Contact Validation
            RuleFor(x => x.Contact)
                .NotEmpty().WithMessage("Contact is required")
                .InclusiveBetween(1000000000, 9999999999).WithMessage("Contact must be a valid 10-digit number");

            // Address Validation
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(100).WithMessage("Address must be less than 100 characters");
        }
    }
}
