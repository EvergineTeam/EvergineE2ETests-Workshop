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

namespace EvergineE2ETestsWorkshop.WebReact
{
    public static class EvergineDemoEvents
    {
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
            webEventsService.OnChangeColor += WebEventsServiceOnOnChangeColor;
        }

        public static void UnsubscribeToWebEvents(WebEventsService webEventsService)
        {
            if (webEventsService != null)
            {
                webEventsService.OnChangeColor -= WebEventsServiceOnOnChangeColor;
            }
        }

        private static void WebEventsServiceOnOnChangeColor(object sender, ChangeColorEventArgs e)
        {
            Material.BaseColor = Color.FromHex(e.Color);
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
            appEventsService?.SendCameraClick(MathHelper.ToDegrees(rotation));
        }
    }
}

