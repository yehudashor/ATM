using FluentValidation;

namespace ATMCore.Validation;


/// <summary>
/// Provides a validation helper class to validate objects using FluentValidation.
/// (בסוף בינתיים אני משתמש בוואלידציה אוטומטית)
/// </summary>
public static class ValidationTool
{
    /// <summary>
    /// Validates the specified entity object using the given validator.
    /// </summary>
    /// <param name="validator">The FluentValidation validator.</param>
    /// <param name="entity">The object to be validated.</param>
    /// <exception cref="ValidationException">Thrown if the validation fails.</exception>
    public static void Validate(IValidator validator, object entity)
    {
        var context = new ValidationContext<object>(entity);
        var result = validator.Validate(context);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}
