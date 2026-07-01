namespace GeekNotes.BuildingBlocks.Application;

public class OperationResult
{
    protected OperationResult(string message, OperationResultStatus status)
    {
        Message = message;
        Status = status;
    }

    public string Message { get; init; }
    public OperationResultStatus Status { get; init; }


    public static OperationResult Success(string message = "عملیات با موفقیت انجام شد")
    {
        return new OperationResult(message, OperationResultStatus.Success);
    }

    public static OperationResult Error(string message = "عملیات با شکست مواجه شد")
    {
        return new OperationResult(message, OperationResultStatus.Error);
    }

    public static OperationResult NotFound(string message = "اطلاعات یافت نشد")
    {
        return new OperationResult(message, OperationResultStatus.NotFound);
    }

    public static OperationResult Warning(string message = "هشدار!")
    {
        return new OperationResult(message, OperationResultStatus.Warning);
    }

    public static OperationResult Conflict(string message = "تداخل در داده‌ها")
    {
        return new OperationResult(message, OperationResultStatus.Conflict);
    }
}

public class OperationResult<TData> : OperationResult
{
    private OperationResult(string message, OperationResultStatus status, TData? data)
        : base(message, status)
    {
        Data = data;
    }

    public TData? Data { get; init; }

    public static OperationResult<TData> Success(TData data, string message = "عملیات با موفقیت انجام شد")
    {
        return new OperationResult<TData>(message, OperationResultStatus.Success, data);
    }

    public new static OperationResult<TData> Error(string message = "عملیات با شکست مواجه شد")
    {
        return new OperationResult<TData>(message, OperationResultStatus.Error, default);
    }

    public new static OperationResult<TData> NotFound(string message = "اطلاعات یافت نشد")
    {
        return new OperationResult<TData>(message, OperationResultStatus.NotFound, default);
    }

    public new static OperationResult<TData> Warning(string message = "هشدار!")
    {
        return new OperationResult<TData>(message, OperationResultStatus.Warning, default);
    }

    public new static OperationResult<TData> Conflict(string message = "تداخل در داده‌ها")
    {
        return new OperationResult<TData>(message, OperationResultStatus.Conflict, default);
    }
}

public enum OperationResultStatus
{
    Success,
    Error,
    NotFound,
    Warning,
    Conflict
}
