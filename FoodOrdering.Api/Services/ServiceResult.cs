namespace FoodOrdering.Api.Services;

public record ServiceResult(ServiceOutcome Outcome, object? Data = null)
{
    public static ServiceResult Ok(object? data)
    {
        return new(ServiceOutcome.Success, data);
    }

    public static ServiceResult Created(object data)
    {
        return new(ServiceOutcome.Created, data);
    }

    public static ServiceResult Invalid(object data)
    {
        return new(ServiceOutcome.Invalid, data);
    }

    public static ServiceResult Missing(object? data = null)
    {
        return new(ServiceOutcome.NotFound, data);
    }

    public static ServiceResult Conflict(object data)
    {
        return new(ServiceOutcome.Conflict, data);
    }

}
