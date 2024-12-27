using FluentValidation;
using AeroLinker.Core.Common.DTO.Project;
using System.Text.RegularExpressions;

namespace AeroLinker.Core.WebAPI.Validators.Project;

public class ProjectDtoValidator : AbstractValidator<ProjectDto>
{
    public ProjectDtoValidator()
    {
        RuleFor(x => x.Description)!.MaximumLength(1000);
        RuleFor(x => x.Name)!
            .MinimumLength(3)!
            .MaximumLength(50)
            .Matches(ValidationRegExes.NoCyrillic);
    }
}