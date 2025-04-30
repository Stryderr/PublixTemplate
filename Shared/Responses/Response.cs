namespace Shared.Responses
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public T? Data { get; set; }

        public static Response<T> Ok(T data) => new Response<T>() { Success = true, Data = data };
        public static Response<T> Fail(string error) => new Response<T>() { Success = false, Error = error };
    }
}
