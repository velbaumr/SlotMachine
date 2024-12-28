using FluentValidation;
using Services.Model;

namespace Services.Validators;

public class UserInputValidator : AbstractValidator<UserInput>
{
    public UserInputValidator()
    {
        RuleFor(x => x.SpinCount)
            .GreaterThan(0);
        
        RuleFor(x => x.StartBalance)
            .GreaterThan(0)
            .GreaterThanOrEqualTo(x => x.Bet);
        
        RuleFor(x => x.Bet)
            .GreaterThan(0);
    }
}