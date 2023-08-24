using Domain.Entities;
using FluentValidation;

namespace Core.Users; 

public class UsersValidations: AbstractValidator<User> {
    public UsersValidations() {
        RuleFor(user => user.Username)
            .NotNull()
            .MaximumLength(100);

        RuleFor(user => user.Password)
            .NotNull()
            .MinimumLength(6); 
    }
}