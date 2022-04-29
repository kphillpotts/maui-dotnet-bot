using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maui_dotnet_bot.Controls
{
    public class SvgCanvas : SKCanvasView
    {
        public ObservableCollection<Svg.Skia.SKSvg> SvgLayers { get; set; } = new ObservableCollection<Svg.Skia.SKSvg>();

        public SvgCanvas()
        {
            SvgLayers.CollectionChanged += SvgLayers_CollectionChanged; 
        }

        private void SvgLayers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.InvalidateSurface();
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var info = e.Info;
            var canvas = e.Surface.Canvas;

            canvas.Clear();

            foreach (var svg in SvgLayers)
            {
                var viewInfo = e.Info;
                var drawBounds = viewInfo.Rect;

                // Get bounding rectangle for SVG image
                var boundingBox = svg.Picture.CullRect;

                // Translate and scale drawing canvas to fit SVG image
                canvas.Translate(drawBounds.MidX, drawBounds.MidY);
                canvas.Scale(0.9f *
                    Math.Min(drawBounds.Width / boundingBox.Width,
                        drawBounds.Height / boundingBox.Height));
                canvas.Translate(-boundingBox.MidX, -boundingBox.MidY);

                // Now finally draw the SVG image
                canvas.DrawPicture(svg.Picture);

                // Optional -> Reset the matrix before performing more draw operations
                canvas.ResetMatrix();
            }
        }

    }
}
