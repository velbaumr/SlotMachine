using FluentValidation;
using Services.Model;

namespace Services.Validators;

public class UserInputValidator : AbstractValidator<UserInput>
{
    public UserInputValidator()
    {
        
    }
}