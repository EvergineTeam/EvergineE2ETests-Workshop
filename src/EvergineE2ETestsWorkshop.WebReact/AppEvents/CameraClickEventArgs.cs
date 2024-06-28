using System;

namespace EvergineE2ETestsWorkshop.WebReact.AppEvents
{
    public class CameraClickEventArgs : EventArgs
    {
        public float Rotation { get; }

        public CameraClickEventArgs(float rotation)
        {
            this.Rotation = rotation;
        }
    }
}

