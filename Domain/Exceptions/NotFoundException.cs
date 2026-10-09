namespace Domain.Exceptions;


public class NotFoundException(string resourceName, object id, string? message = null)
: Exception(message ?? $"The resource {resourceName} with id {id} was not found.")
{
    public String ResourceName { get; set; } = resourceName;
    public object Id { get; set; } = id;

}
