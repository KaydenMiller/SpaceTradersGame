using SpaceTraders.Core;

namespace SpaceTraders.Api;

public class SpaceTradersApiException : Exception
{
    public ErrorCodes ErrorCode { get; private set; }
    public SpaceTradersApiException(string message, ErrorCodes code) : base(message)
    {
        ErrorCode = code;
    }
}

public class SpaceTradersApiException<TData> : SpaceTradersApiException where TData : class
{
    public TData? Data { get; private set; }

    public SpaceTradersApiException(string message, ErrorCodes code, TData? data) : base(message, code)
    {
        Data = data;
    }
}