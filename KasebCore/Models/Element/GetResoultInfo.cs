namespace KasebCore.Models.Element
{
    public  class GetResoultInfo
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = "";

        public object? Data { get; set; } = new object();

        public int StatusCode { get; set; } = 0;

    }
}
