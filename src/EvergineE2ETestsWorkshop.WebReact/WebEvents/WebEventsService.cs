using System;
using EvergineE2ETestsWorkshop.WebReact.Base;

namespace EvergineE2ETestsWorkshop.WebReact.WebEvents
{
    public class WebEventsService : WebEventsServiceBase
    {
        public event EventHandler<ChangeColorEventArgs> OnChangeColor;

        public void SendChangeColor(string changeColorData)
        {
            this.OnChangeColor?.Invoke(this, new ChangeColorEventArgs(changeColorData));
        }
    }
}

