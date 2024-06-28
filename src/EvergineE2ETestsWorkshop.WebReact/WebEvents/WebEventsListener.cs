using Evergine.Framework;
using Microsoft.JSInterop;
using EvergineE2ETestsWorkshop.WebReact.Base;

namespace EvergineE2ETestsWorkshop.WebReact.WebEvents;

/// <summary>
/// Web source code (TypeScript) controls Evergine and all c# wasm code through this class.
/// </summary>
public static class WebEventsListener 
{
    private static WebEventsService webEventsService;

    /// <summary>
    /// Entry-point of web application.
    /// </summary>
    /// <param name="canvasId">The id of the canvas object in which Evergine will render.</param>
    [JSInvokable("EvergineE2ETestsWorkshop.WebReact.WebEvents.WebEventsListener:StartEvergineOnCanvas")]
    public static void StartEvergineOnCanvas(string canvasId)
    {
        WebEventsController.StartEvergineOnCanvas(canvasId);
        webEventsService = Application.Current.Container.Resolve<WebEventsService>();
    }

    [JSInvokable("EvergineE2ETestsWorkshop.WebReact.WebEvents.WebEventsListener:StopEvergineOnCanvas")]
    public static void StopEvergineOnCanvas(string canvasId)
    {
        WebEventsController.StopEvergineOnCanvas(canvasId);
        webEventsService = null;
    }

    [JSInvokable("EvergineE2ETestsWorkshop.WebReact.WebEvents.WebEventsListener:UpdateSizeOnCanvas")]
    public static void UpdateSizeOnCanvas(string canvasId)
    {
        WebEventsController.UpdateSizeOnCanvas(canvasId);
    }

    [JSInvokable("EvergineE2ETestsWorkshop.WebReact.WebEvents.WebEventsListener:ChangeColor")]
    public static void ChangeColor(string color)
    {
        webEventsService.SendChangeColor(color);
    }

    [JSInvokable("EvergineE2ETestsWorkshop.WebReact.WebEvents.WebEventsListener:SetTestMode")]
    public static void SetTestMode(bool activated)
    {
        webEventsService.SendSetTestMode(activated);
    }
}

