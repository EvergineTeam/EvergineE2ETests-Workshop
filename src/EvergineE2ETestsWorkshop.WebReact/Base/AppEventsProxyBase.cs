namespace EvergineE2ETestsWorkshop.WebReact.Base;

public class AppEventsProxyBase
{
    protected AppEventsServiceBase appEventsServiceBase;

    public AppEventsProxyBase(AppEventsServiceBase appEventsService)
    {
        this.appEventsServiceBase = appEventsService;
    }

    public virtual void SubscribeToAppEvents()
    {
        this.appEventsServiceBase.OnEvergineReady += ApplicationOnEvergineReady;
    }

    public virtual void UnsubscribeToAppEvents()
    {
        this.appEventsServiceBase.OnEvergineReady -= ApplicationOnEvergineReady;
    }

    private static void ApplicationOnEvergineReady(object sender, bool ready)
    {
        Program.Wasm.Invoke("App.appEventsListener.onEvergineReady", false, ready);
    }
}

