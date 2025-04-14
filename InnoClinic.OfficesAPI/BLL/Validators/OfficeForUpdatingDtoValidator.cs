using BLL.DTO;

namespace BLL.Validators;

public class OfficeForUpdatingDtoValidator : OfficeBaseValidator<OfficeForUpdatingDto>
{
    public OfficeForUpdatingDtoValidator()
    {
        ApplyCommonRules();
    }
}