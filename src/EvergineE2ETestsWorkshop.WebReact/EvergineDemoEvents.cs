using Evergine.Common.Graphics;
using Evergine.Components.Graphics3D;
using Evergine.Framework;
using Evergine.Framework.Graphics;
using Evergine.Framework.Graphics.Effects;
using Evergine.Framework.Graphics.Materials;
using Evergine.Framework.Services;
using Evergine.Mathematics;
using EvergineE2ETestsWorkshop.WebReact.AppEvents;
using EvergineE2ETestsWorkshop.WebReact.WebEvents;
using System.Text.Json;
using System;

namespace EvergineE2ETestsWorkshop.WebReact
{
    public static class EvergineDemoEvents
    {
        public static bool TestsResultsEnabled;

        private static StandardMaterial Material;
        private static Transform3D TeapotTransform3D;

        public static void CreateTeapot(Scene scene)
        {
            var assetsService = Application.Current.Container.Resolve<AssetsService>();
            var effect = assetsService.Load<Effect>(EvergineContent.Effects.StandardEffect);
            var layer = new RenderLayerDescription()
            {
                RenderState = new RenderStateDescription()
                {
                    RasterizerState = RasterizerStates.None,
                    BlendState = BlendStates.Opaque,
                    DepthStencilState = DepthStencilStates.ReadWrite,
                }
            };
            Material = new StandardMaterial(effect)
                { LayerDescription = layer, IBLEnabled = true, LightingEnabled = true };

            TeapotTransform3D = new Transform3D();

            var teapot = new Entity()
                .AddComponent(new TeapotMesh())
                .AddComponent(new MaterialComponent() { Material = Material.Material })
                .AddComponent(new MeshRenderer())
                .AddComponent(TeapotTransform3D)
                .AddComponent(new Spinner() { AxisIncrease = Vector3.UnitY / 2 });
            scene.Managers.EntityManager.Add(teapot);
        }

        public static void SubscribeToWebEvents(WebEventsService webEventsService)
        {
            webEventsService.OnChangeColor += WebEventsServiceOnChangeColor;
            webEventsService.OnSetTestMode += WebEventsServiceOnSetTestMode;
        }

        public static void UnsubscribeToWebEvents(WebEventsService webEventsService)
        {
            if (webEventsService != null)
            {
                webEventsService.OnChangeColor -= WebEventsServiceOnChangeColor;
                webEventsService.OnSetTestMode -= WebEventsServiceOnSetTestMode;
            }
        }

        private static void WebEventsServiceOnChangeColor(object sender, ChangeColorEventArgs e)
        {
            Material.BaseColor = Color.FromHex(e.Color);
            var testResult = new TestResultDto()
            {
                TeapotColor = Material.BaseColor.ToHexColorCode()
            };
            PrintTestResult(testResult);
        }

        private static void WebEventsServiceOnSetTestMode(object sender, SetTestModeEventArgs e)
        {
            TestsResultsEnabled = e.Activated;
            Console.WriteLine($"Test mode {(TestsResultsEnabled ? "ON" : "OFF")}");
        }
        
        private static void PrintTestResult(TestResultDto testResult)
        {
            if (!TestsResultsEnabled)
            {
                return;
            }

            var json = JsonSerializer.Serialize(testResult, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            Console.WriteLine($"TestResult: {json}");
        }

        public static void CheckCameraClick(Display display, AppEventsService appEventsService)
        {
            if (display?.MouseDispatcher == null)
            {
                return;
            }

            if (display.MouseDispatcher.Points.Count == 0)
            {
                return;
            }

            var point = display.MouseDispatcher.Points[0];
            if (point == default)
            {
                return;
            }

            var rotation = TeapotTransform3D.LocalRotation.Y >= 0
                ? TeapotTransform3D.LocalRotation.Y
                : MathHelper.TwoPi + TeapotTransform3D.LocalRotation.Y;
            var degrees = MathHelper.ToDegrees(rotation);
            appEventsService?.SendCameraClick(degrees);
            
            var testResult = new TestResultDto()
            {
                TeapotRotation = degrees
            };
            PrintTestResult(testResult);
        }
    }
}

