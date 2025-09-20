using timesheets.Domain.Errors;

namespace timesheets.Domain.Shared;

public class Result
{
    protected Result(bool isSuccess, DomainError? error)
    {
        if (isSuccess && error is not null ||
            !isSuccess && error is null)
        {
            throw new ArgumentException("Invalid result state", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public DomainError? Error { get; }

    public static Result Success() => new(true, null);
    public static Result Failure(DomainError error) => new(false, error);

    public static Result<T> Success<T>(T value) => new(value, true, null);
    public static Result<T> Failure<T>(DomainError error) => new(default, false, error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    protected internal Result(T? value, bool isSuccess, DomainError? error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access value of failed result");

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(DomainError error) => Failure<T>(error);
}