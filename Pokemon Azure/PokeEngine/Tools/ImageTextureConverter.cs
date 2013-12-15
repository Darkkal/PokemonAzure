using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace PokeEngine.Tools
{
    static class ImageTextureConverter
    {

        public static Image Texture2DToImage(Texture2D texture)
        {
            if (texture.IsDisposed || texture == null)
                return null;

            //Memory stream to store the bitmap data.
            MemoryStream ms = new MemoryStream();

            //Save the texture to the stream.
            texture.SaveAsPng(ms, texture.Width, texture.Height);

            //Seek the beginning of the stream.
            ms.Seek(0, SeekOrigin.Begin);

            //Create an image from a stream.
            Image bmp2 = Bitmap.FromStream(ms);

            //Close the stream, we nolonger need it.
            ms.Close();
            ms = null;
            return bmp2;
        }

        public static void ImageToTexture2D(Image image, GraphicsDevice graphics, ref Texture2D texture)
        {
            if (image == null)
                return;

            if (texture == null || texture.IsDisposed ||
                texture.Width != image.Width ||
                texture.Height != image.Height ||
                texture.Format != SurfaceFormat.Color)
            {
                if (texture != null && !texture.IsDisposed)
                    texture.Dispose();

                texture = new Texture2D(graphics, image.Width, image.Height, false, SurfaceFormat.Color);
            }
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    if (graphics.Textures[i] == texture)
                    {
                        graphics.Textures[i] = null;
                        break;
                    }
                }
            }

            //Memory stream to store the bitmap data.
            MemoryStream ms = new MemoryStream();

            //Save to that memory stream.
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            //Go to the beginning of the memory stream.
            ms.Seek(0, SeekOrigin.Begin);

            //Fill the texture.
            texture = Texture2D.FromStream(graphics, ms, image.Width, image.Height, false);

            //Close the stream.
            ms.Close();
            ms = null; 
        }

    }
}
