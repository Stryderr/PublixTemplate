namespace Domain.ReturnModels
{
    public class BaseReturnDM
    {
        public string Process { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int SuccessCount { get; set; } = 0;
        public List<Exception> Exceptions { get; set; } = new List<Exception>();
    }
}
