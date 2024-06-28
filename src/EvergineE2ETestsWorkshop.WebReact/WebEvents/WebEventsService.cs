using System;
using EvergineE2ETestsWorkshop.WebReact.Base;

namespace EvergineE2ETestsWorkshop.WebReact.WebEvents
{
    public class WebEventsService : WebEventsServiceBase
    {
        public event EventHandler<ChangeColorEventArgs> OnChangeColor;
        public event EventHandler<SetTestModeEventArgs> OnSetTestMode;


        public void SendChangeColor(string changeColorData)
        {
            this.OnChangeColor?.Invoke(this, new ChangeColorEventArgs(changeColorData));
        }

        public void SendSetTestMode(bool activated)
        {
            this.OnSetTestMode?.Invoke(this, new SetTestModeEventArgs(activated));
        }
    }
}

