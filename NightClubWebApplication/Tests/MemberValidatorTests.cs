using FluentValidation.TestHelper;
using NightClub.Models;
using System;
using Xunit;
namespace NightClubTests
{
    public class MemberValidatorTests
    {
        private readonly MemberValidator _validator;

        public MemberValidatorTests()
        {
            _validator = new MemberValidator();
        }

        [Fact]
        public void Should_Have_Errors_When_Invalid_Member()
        {
            var member = new Member
            {
                Email = "invalid-email",
                PhoneNumber = "invalid-phone-number",
                IdentityCard = new IdentityCard
                {
                    BirthDate = DateTime.UtcNow.AddYears(-15), // Less than 18 years
                    ValidFrom = DateTime.UtcNow.AddYears(1), // Future date
                    ValidTo = DateTime.UtcNow.AddYears(-1) // Past date
                }
            };

            var result = _validator.TestValidate(member);

            result.ShouldHaveValidationErrorFor(m => m.IdentityCard.BirthDate)
                .WithErrorMessage("Member must be older than 18 years.");

            result.ShouldHaveValidationErrorFor(m => m.PhoneNumber)
                .WithErrorMessage("Invalid phone number format.");

            result.ShouldHaveValidationErrorFor(m => m.Email)
                .WithErrorMessage("Invalid email format.");

            result.ShouldHaveValidationErrorFor(m => m.IdentityCard.ValidFrom)
                .WithErrorMessage("Identity Card's ValidFrom date must be less than or equal to the current date.");

            result.ShouldHaveValidationErrorFor(m => m.IdentityCard.ValidTo)
                .WithErrorMessage("Identity Card's ValidTo date must be greater than or equal to the current date.");
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Valid_Member()
        {
            var member = new Member
            {
                Email = "Aymen@yopmail.com",
                PhoneNumber = "+3234567890",
                IdentityCard = new IdentityCard
                {
                    BirthDate = DateTime.UtcNow.AddYears(-20), // Older than 18 years
                    ValidFrom = DateTime.UtcNow.AddYears(-1), // Past date
                    ValidTo = DateTime.UtcNow.AddYears(41) // Future date
                }
            };

            var result = _validator.TestValidate(member);

            result.ShouldNotHaveValidationErrorFor(m => m.IdentityCard.BirthDate);
            result.ShouldNotHaveValidationErrorFor(m => m.PhoneNumber);
            result.ShouldNotHaveValidationErrorFor(m => m.Email);
            result.ShouldNotHaveValidationErrorFor(m => m.IdentityCard.ValidFrom);
            result.ShouldNotHaveValidationErrorFor(m => m.IdentityCard.ValidTo);
        }

        [Fact]
        public void Should_Have_Error_When_BirthDate_Is_Exactly_18_Years_Ago()
        {
            var member = new Member
            {
                Email = "Aymen@yopmail.com",
                PhoneNumber = "+3234567890",
                IdentityCard = new IdentityCard
                {
                    BirthDate = DateTime.UtcNow.AddYears(-18), // Exactly 18 years ago
                    ValidFrom = DateTime.UtcNow.AddYears(-1),
                    ValidTo = DateTime.UtcNow.AddYears(41)
                }
            };

            var result = _validator.TestValidate(member);
            result.ShouldHaveValidationErrorFor(m => m.IdentityCard.BirthDate);
        }
    }
}