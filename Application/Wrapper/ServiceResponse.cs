
namespace Application.Wrapper
{
    public class ServiceResponse<T>:ServiceResponse
    {
        public T Data { get; set; }
        public ServiceResponse()
        {

        }
        public ServiceResponse(T data)
        {
            Data = data;
        }

    }
    public class ServiceResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public ServiceResponse()
        {

        }
        public ServiceResponse(string message)
        {
            Message = message;        
        }
        public ServiceResponse(string message,bool success)
        {
            Success = success;
            Message = message;
        }
    }
}
