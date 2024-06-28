using EvergineE2ETestsWorkshop.WebReact.Base;

namespace EvergineE2ETestsWorkshop.WebReact.AppEvents;

public class AppEventsProxy : AppEventsProxyBase
{
    private AppEventsService appEventsService;

    public AppEventsProxy(AppEventsService appEventsService) : base(appEventsService)
    {
        this.appEventsService = appEventsService;
    }

    public override void SubscribeToAppEvents()
    {
        base.SubscribeToAppEvents();
        this.appEventsService.OnCameraClick += AppEventsServiceOnCameraClick;
    }

    public override void UnsubscribeToAppEvents()
    {
        this.appEventsService.OnCameraClick -= AppEventsServiceOnCameraClick;
        base.UnsubscribeToAppEvents();
    }

    private static void AppEventsServiceOnCameraClick(object sender, CameraClickEventArgs e)
    {
        Program.Wasm.Invoke("App.appEventsListener.onCameraClick", false, e.Rotation);
    }
}

