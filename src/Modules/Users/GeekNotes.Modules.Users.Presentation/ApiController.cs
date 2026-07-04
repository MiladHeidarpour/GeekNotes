using GeekNotes.BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace GeekNotes.Modules.Users.Presentation;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected IActionResult HandleResult(OperationResult result)
    {
        return result.Status switch
        {
            OperationResultStatus.Success => Ok(result),
            OperationResultStatus.NotFound => NotFound(result),
            OperationResultStatus.Conflict => Conflict(result),
            OperationResultStatus.Error => BadRequest(result),
            _ => BadRequest(result)
        };
    }
    protected IActionResult HandleResult<TData>(OperationResult<TData> result)
    {
        return result.Status switch
        {
            OperationResultStatus.Success => Ok(result),
            OperationResultStatus.NotFound => NotFound(result),
            OperationResultStatus.Conflict => Conflict(result),
            OperationResultStatus.Error => BadRequest(result),
            _ => BadRequest(result)
        };
    }


    protected IActionResult HandleMappedResult<TSource, TDestination>(
    OperationResult<TSource> sourceResult,
    Func<TSource, TDestination> mapper)
    {
        if (sourceResult.Status == OperationResultStatus.Success)
        {
            var mappedData = mapper(sourceResult.Data);
            var successResult = OperationResult<TDestination>.Success(mappedData, sourceResult.Message);
            return HandleResult(successResult);
        }

        var errorResult = sourceResult.Status switch
        {
            OperationResultStatus.NotFound => OperationResult<TDestination>.NotFound(sourceResult.Message),
            OperationResultStatus.Conflict => OperationResult<TDestination>.Conflict(sourceResult.Message),
            OperationResultStatus.Warning => OperationResult<TDestination>.Warning(sourceResult.Message),
            _ => OperationResult<TDestination>.Error(sourceResult.Message)
        };
        return HandleResult(errorResult);
    }
    //protected IActionResult HandleResult<TData>(OperationResult<TData> result)
    //{
    //    return result.Status switch
    //    {
    //        OperationResultStatus.Success => Ok(ApiResult<TData>.Success(result.Data, result.Message)),
    //        OperationResultStatus.Error => BadRequest(ApiResult<TData>.Error(result.Message)),
    //        OperationResultStatus.NotFound => NotFound(ApiResult<TData>.Error(result.Message)),
    //        OperationResultStatus.Conflict => Conflict(ApiResult<TData>.Error(result.Message)),
    //        _ => BadRequest(ApiResult<TData>.Error(result.Message))
    //    };
    //}

    //protected IActionResult QueryResult<TData>(TData? data)
    //{
    //    if (data is null)
    //        return NotFound(ApiResult<TData>.Error("اطلاعات یافت نشد!"));

    //    return Ok(ApiResult<TData>.Success(data, "عملیات با موفقیت انجام شد."));
    //}
}