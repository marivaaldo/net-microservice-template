namespace NetMicroserviceTemplate.Application.Exceptions;

internal class BusinessException : ApplicationException
{
    public BusinessException(string message) : base(message) { }
    public BusinessException(string message, Exception innerException) : base(message, innerException) { }
}
