using System;

namespace Organizer
{
    public class ErrorMessage
    {
        public String Text { get; set; }

        public ErrorMessage(string text)
        {
            Text = text;
        }
    }
}