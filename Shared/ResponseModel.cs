namespace Ecommerce_2023.Shared
{
    public class ResponseModel
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public dynamic? Result { get; set; }
    }
}
