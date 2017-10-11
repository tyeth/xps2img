using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace xps2img
{
    public static class Xps2JpegConverter
    {
        public static IEnumerable<Bitmap> Convert(byte[] fileData, int? width, int? height, int? dpi, int quality = 85)
        {
            using (var xpsConverter = new Xps2Image(fileData))
            {
                var pageSizeRatio = xpsConverter.PageSize.Width / xpsConverter.PageSize.Height;
                Size? size;


                if (width.HasValue || height.HasValue)
                {
                    int newWidth = 0;
                    int newHeight = 0;
                    if (!(width.HasValue && height.HasValue))
                    {
                        newWidth = width.HasValue ? width.Value : (int) (height.Value * pageSizeRatio);
                        newHeight = height.HasValue ? height.Value : (int) (width.Value / pageSizeRatio);

                        size = new Size(newWidth, newHeight);
                    }
                    else
                    {
                        size = new Size(width.Value, height.Value);
                    }
                }
                Parameters
                    p = new xps2img.Parameters
                    {
                        ImageOptions = new ImageOptions(quality, TiffCompressOption.Zip),
                        Dpi = dpi.HasValue ? dpi.Value : 300,
                        ImageType = ImageType.Jpeg,
                        RequiredSize = size
                    };


                var images = xpsConverter.ToBitmap(new Parameters
                {
                    ImageType = ImageType.Png,
                    Dpi = 300
                });
                return images;
            }
//        
//        Convert a xps document to png from disk
//
//        using (var xpsConverter = new Xps2Image("multipage.xps"))
//        {
//            var images = xpsConverter.ToBitmap(new Parameters
//            {
//                ImageType = ImageType.Png,
//                Dpi = 300
//            });
//        }
//        Convert a xps document to png from a byte array
//
//        using (var xpsConverter = new Xps2Image(File.ReadAllBytes("multipage.xps")))
//        {
//            var images = xpsConverter.ToBitmap(new Parameters
//            {
//                ImageType = ImageType.Png,
//                Dpi = 300
//            });
//        }
//        Convert a xps document to png from stream
//
//        using (var xpsConverter = new Xps2Image(new MemoryStream(File.ReadAllBytes("multipage.xps"))))
//        {
//            var images = xpsConverter.ToBitmap(new Parameters
//            {
//                ImageType = ImageType.Png,
//                Dpi = 300
//            });
//        }
        }
    }
}