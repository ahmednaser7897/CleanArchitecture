namespace Domain.Exceptions;


public class NotFoundException : Exception
{
    public NotFoundException(string resourceName, object id) : base($"The resource {resourceName ?? "Resource"} with id {id ?? ""} was not found.")
    { }
    public NotFoundException(string message) : base(message) { }
}
