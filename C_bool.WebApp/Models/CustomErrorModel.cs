namespace C_bool.WebApp.Models
{
    public class CustomErrorModel : ErrorViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public MessageType Type { get; set; }

        public enum MessageType
        {
            Error,
            Info,
            Success,
            Warning
        }

        public CustomErrorModel()
        {
            Type = MessageType.Error;
        }
    }
}