using System;
using Evergine.Framework.Services;

namespace EvergineE2ETestsWorkshop.WebReact.Base
{
    public class AppEventsServiceBase : Service
    {
        public event EventHandler<bool> OnEvergineReady;

        public void SetEvergineReady(bool ready)
        {
            this.OnEvergineReady?.Invoke(this, ready);
        }
    }
}

