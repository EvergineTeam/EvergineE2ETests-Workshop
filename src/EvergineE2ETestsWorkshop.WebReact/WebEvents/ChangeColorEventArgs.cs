using System;

namespace EvergineE2ETestsWorkshop.WebReact.WebEvents
{
    public class ChangeColorEventArgs : EventArgs
    {
        public string Color { get; }

        public ChangeColorEventArgs(string color)
        {
            this.Color = color;
        }
    }
}

