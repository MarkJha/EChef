using eCafe.Infrastructure.Models;
using FluentValidation;

namespace eCafe.WebUI.Validator
{
    /// <summary>
    /// 
    /// </summary>
    public class MainMenuViewModelValidator : AbstractValidator<MainMenuDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public MainMenuViewModelValidator()
        {
            RuleFor(reg => reg.Name)
                    .NotEmpty()
                    .NotNull()
                    .MaximumLength(50);

            RuleFor(reg => reg.Description)
                           .NotEmpty();
        }
    }
}
