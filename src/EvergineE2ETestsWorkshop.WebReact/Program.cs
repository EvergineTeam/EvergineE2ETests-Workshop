using Evergine.Common.Graphics;
using Evergine.Framework.Graphics;
using Evergine.Framework.Services;
using Evergine.OpenGL;
using Evergine.Web;
using System.Collections.Generic;
using System.Diagnostics;
using Evergine.Framework;
using EvergineE2ETestsWorkshop;
using EvergineE2ETestsWorkshop.WebReact;
using EvergineE2ETestsWorkshop.WebReact.AppEvents;
using EvergineE2ETestsWorkshop.WebReact.WebEvents;

public class Program
{
    // Wasm instance need to be initialized here for debugger
    public static readonly Evergine.Web.WebAssembly Wasm = Evergine.Web.WebAssembly.GetInstance();
    public static readonly Dictionary<string, WebSurface> AppCanvas = new();

    public static WebWindowsSystem WindowsSystem;
    public static MyApplication Application;

    // Auxiliary variable to optimize update
    private static Scene scene;
    private static AppEventsService appEventsService;

    public static void Main()
    {
        // Hack for AOT dll dependencies
        _ = new Evergine.Components.Graphics3D.Spinner();
    }

    public static void Run(string canvasId)
    {
        // Create app
        Application = new MyApplication();

        // Events
        Application.Container.Register<AppEventsService>();
        Application.Container.Register<AppEventsProxy>();
        Application.Container.Register<WebEventsService>();

        // Create Services
        WindowsSystem = new WebWindowsSystem();
        Application.Container.RegisterInstance(WindowsSystem);

        var canvas = Wasm.GetElementById(canvasId);
        var surface = (WebSurface)WindowsSystem.CreateSurface(canvas);
        AppCanvas[canvasId] = surface;
        ConfigureGraphicsContext(Application, surface, canvasId);

        // Audio is currently unsupported
        //var xaudio = new Evergine.XAudio2.XAudioDevice();
        //Application.Container.RegisterInstance(xaudio);

        Stopwatch clockTimer = Stopwatch.StartNew();
        WindowsSystem.Run(
            () =>
            {
                Application.Container.Resolve<AppEventsProxy>().SubscribeToAppEvents();
                Application.Initialize();

                var assetsService = Application.Container.Resolve<AssetsService>();
                scene = assetsService.Load<MyScene>(EvergineContent.Scenes.MyScene_wescene);
                scene.Started += Scene_Started;
                scene.Closed += Scene_Closed;

                EvergineDemoEvents.CreateTeapot(scene);
                EvergineDemoEvents.CreateFlightHelmets(scene);
                EvergineDemoEvents.SubscribeToWebEvents(Application.Container.Resolve<WebEventsService>());

                appEventsService = Application.Container.Resolve<AppEventsService>();
            },
            () =>
            {
                var gameTime = clockTimer.Elapsed;
                clockTimer.Restart();
                Application.UpdateFrame(gameTime);
                EvergineDemoEvents.CheckCameraClick(scene.Managers.RenderManager.ActiveCamera3D.Display, appEventsService);
                Application.DrawFrame(gameTime);
            });
    }

    private static void Scene_Started(object sender, System.EventArgs e)
    {
        Application.Container.Resolve<AppEventsService>().SetEvergineReady(true);
    }

    private static void Scene_Closed(object sender, System.EventArgs e)
    {
        Application.Container.Resolve<AppEventsService>()?.SetEvergineReady(false);
        EvergineDemoEvents.UnsubscribeToWebEvents(Application.Container.Resolve<WebEventsService>());
    }

    private static void ConfigureGraphicsContext(Application application, Surface surface, string canvasId)
    {
        // Enabled web canvas antialias (MSAA)
        Wasm.Invoke("window._evergine_EGL", false, "webgl2", canvasId);

        GraphicsContext graphicsContext = new GLGraphicsContext(GraphicsBackend.WebGL2);
        graphicsContext.CreateDevice();
        SwapChainDescription swapChainDescription = new SwapChainDescription()
        {
            SurfaceInfo = surface.SurfaceInfo,
            Width = surface.Width,
            Height = surface.Height,
            ColorTargetFormat = PixelFormat.R8G8B8A8_UNorm,
            ColorTargetFlags = TextureFlags.RenderTarget | TextureFlags.ShaderResource,
            DepthStencilTargetFormat = PixelFormat.D24_UNorm_S8_UInt,
            DepthStencilTargetFlags = TextureFlags.DepthStencil,
            SampleCount = TextureSampleCount.None,
            IsWindowed = true,
            RefreshRate = 60
        };
        var swapChain = graphicsContext.CreateSwapChain(swapChainDescription);
        swapChain.VerticalSync = true;
        swapChain.FrameBuffer.IntermediateBufferAssociated = false;

        var graphicsPresenter = application.Container.Resolve<GraphicsPresenter>();
        var firstDisplay = new Display(surface, swapChain);
        graphicsPresenter.AddDisplay("DefaultDisplay", firstDisplay);

        application.Container.RegisterInstance(graphicsContext);
    }
}

