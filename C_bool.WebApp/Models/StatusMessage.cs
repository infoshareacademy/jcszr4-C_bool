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
        public int Duration { get; set; } = 10000;
        public bool Dismissable { get; set; } = true;
        public string MessageParsed { get; set; }

        public StatusMessage(string message, Status status)
        {
            Message = message;
            MessageStatus = status;
            if (MessageStatus == Status.OK)
            {
                Color = "green";
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
                Type = "info";
            }
            if (MessageStatus is Status.INFO)
            {
                Color = "#61a5c2";
                Type = "info";
            }

            MessageParsed = ParseNotyf();

        }

        public string ParseNotyf()
        {
            return @$"function showNotyf2() {{
            const notyf2 = new Notyf({{
                position: {{
                    x: '{PositionX}',
                    y: '{PositionY}',
                }},
                types: [
                    {{
                        type: '{Type}',
                        background: '{Color}',
                        icon: {{
                            className: 'fas fa-info-circle',
                            tagName: 'span',
                            color: '#fff'
                        }},
                        dismissible: true
                    }}
                ],
                duration: {Duration},
            }});
            notyf2.open({{
                type: '{Type}',
                message: '{Message}'
            }});
            }};
            window.addEventListener(""load"", showNotyf2());";


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
