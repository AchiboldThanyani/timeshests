namespace timesheets.Domain.Errors;

public abstract record DomainError(string Code, string Message)
{
    public static implicit operator string(DomainError error) => error.Code;
}