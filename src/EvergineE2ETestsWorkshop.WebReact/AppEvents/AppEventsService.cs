using System;
using EvergineE2ETestsWorkshop.WebReact.Base;

namespace EvergineE2ETestsWorkshop.WebReact.AppEvents
{
    public class AppEventsService : AppEventsServiceBase
    {
        public event EventHandler<CameraClickEventArgs> OnCameraClick;

        public void SendCameraClick(float rotation)
        {
            this.OnCameraClick?.Invoke(this, new CameraClickEventArgs(rotation));
        }
    }
}

