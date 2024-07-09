using FluentValidation;
using NightClub.Models;
using System.Text.RegularExpressions;

public class MemberValidator : AbstractValidator<Member>
{
    public MemberValidator()
    {

        RuleFor(m => m.IdentityCard)
        .NotEmpty()
        .WithMessage("Identity Card is required."); 

        // La personne doit avoir > 18 ans
        RuleFor(m => m.IdentityCard.BirthDate)
          .LessThan(DateTime.UtcNow.AddYears(-18))
          .WithMessage("Member must be older than 18 years.");

        // La création d'un membre nécessite une carte d'identité valide       
        RuleFor(card => card.IdentityCard.ValidFrom)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Identity Card's ValidFrom date must be less than or equal to the current date.");

        RuleFor(card => card.IdentityCard.ValidTo)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Identity Card's ValidTo date must be greater than or equal to the current date.");

        // La création d'un membre nécessite une adresse email OU un numéro de téléphone
        RuleFor(m => m)
            .Custom((member, context) =>
            {
                if (string.IsNullOrEmpty(member.Email) && string.IsNullOrEmpty(member.PhoneNumber))
                {
                    context.AddFailure("Email or PhoneNumber is required.");
                }

                if (!string.IsNullOrEmpty(member.Email))
                {
                    var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                    if (!emailRegex.IsMatch(member.Email))
                    {
                        context.AddFailure("Email", "Invalid email format.");
                    }
                }

                if (!string.IsNullOrEmpty(member.PhoneNumber))
                {
                    var phoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$");
                    if (!phoneRegex.IsMatch(member.PhoneNumber))
                    {
                        context.AddFailure("PhoneNumber", "Invalid phone number format.");
                    }
                }
            });

    }
}
