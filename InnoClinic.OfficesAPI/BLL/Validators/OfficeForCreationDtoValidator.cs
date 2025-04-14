using BLL.DTO;

namespace BLL.Validators;

public class OfficeForCreationDtoValidator : OfficeBaseValidator<OfficeForCreatingDto>
{
    public OfficeForCreationDtoValidator()
    {
        ApplyCommonRules();
    }
}