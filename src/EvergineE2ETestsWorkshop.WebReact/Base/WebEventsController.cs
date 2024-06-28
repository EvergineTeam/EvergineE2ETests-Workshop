using Evergine.Framework;
using EvergineE2ETestsWorkshop.WebReact.AppEvents;

namespace EvergineE2ETestsWorkshop.WebReact.Base;

public static class WebEventsController
{
    private static AppEventsProxy appEventsProxy;

    public static void StartEvergineOnCanvas(string canvasId)
    {
        Program.Run(canvasId);
        appEventsProxy = Application.Current.Container.Resolve<AppEventsProxy>();
    }

    public static void StopEvergineOnCanvas(string canvasId)
    {
        if (Program.AppCanvas.ContainsKey(canvasId))
        {
            Program.AppCanvas[canvasId].Dispose();
            Program.AppCanvas.Remove(canvasId);
        }

        Program.WindowsSystem?.Dispose();
        Program.Application?.Dispose();

        appEventsProxy.UnsubscribeToAppEvents();
        Program.Application = null;
        Program.WindowsSystem = null;
        appEventsProxy = null;
    }

    public static void UpdateSizeOnCanvas(string canvasId)
    {
        if (Program.AppCanvas.TryGetValue(canvasId, out var surface))
        {
            surface.RefreshSize();
        }
    }   
}

