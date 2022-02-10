using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C_bool.WebApp.Models
{
    public class StatusMessage
    {
        public string Message { get; set; }
        public string Color { get; set; }
        private string Type { get; set; }
        public Status MessageStatus { get; set; }
        public string PositionX { get; set; } = "right";
        public string PositionY { get; set; } = "bottom";
        public int Duration { get; set; } = 3000;
        public bool Dismissable { get; set; } = true;
        public string MessageParsed { get; set; }

        public StatusMessage(string message, Status status)
        {
            Message = message;
            MessageStatus = status;
            if (MessageStatus == Status.OK)
            {
                Color = "#499F68";
                Type = "success";

            }
            if (MessageStatus is Status.FAIL or Status.BAD_REQUEST)
            {
                Color = "#db222a";
                Type = "error";
            }
            if (MessageStatus is Status.WARNING)
            {
                Color = "orange";
                Type = "success";
            }
            if (MessageStatus is Status.INFO)
            {
                Color = "#61a5c2";
                Type = "success";
            }

            MessageParsed = ParseNotyf();

        }

        public string ParseNotyf()
        {
            return @$"function showStatusMessage() {{
            notyf.{Type}({{
            message: '{Message}',
            duration: {Duration},
            color: '{Color}',
            dismissable: {Dismissable.ToString().ToLower()}
        }});
            }};
            window.addEventListener(""load"", showStatusMessage());";
        }

        public StatusMessage(string message, Status status, string color)
        {
            Message = message;
            MessageStatus = status;
            Color = color;
            Type = "info";

            MessageParsed = ParseNotyf();
        }

        public enum Status
        {
            INFO,
            OK,
            FAIL,
            WARNING,
            BAD_REQUEST
        }
    }
}
