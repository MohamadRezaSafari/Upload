using MohamadReza;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Web.Helpers;
using System.Threading.Tasks;

namespace Providers
{
    public class Upload
    {
        private Encryption enc = new Encryption();
        private readonly string[] ImageExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif" };
        private string ImageName;
        private string ImageName_thumb;


        public string ImageUpload(string dir, int Width, int Height, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0 && ImageExtensions.Contains(Path.GetExtension(file.FileName).ToLower()) && file.ContentType.Contains("image"))
            {
                try
                {
                    ImageName = enc.GUID() + Path.GetExtension(file.FileName);
                    ImageName_thumb = enc.GUID() + "_thumb" + Path.GetExtension(file.FileName);

                    string path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(dir), Path.GetFileName(ImageName));
                    string path_tumb = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(dir), Path.GetFileName(ImageName_thumb));

                    file.SaveAs(path);
                    Crop(Width, Height, file.InputStream, path_tumb);

                    return ImageName;
                }
                catch (Exception)
                {
                    throw new Exception("Run again!");
                }
            }
            else
            {
                return null;
            }
        }



        public string ImageUpload(string dir, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0 && ImageExtensions.Contains(Path.GetExtension(file.FileName).ToLower()) && file.ContentType.Contains("image"))
            {
                try
                {
                    ImageName = enc.GUID() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(dir), Path.GetFileName(ImageName));
                    file.SaveAs(path);
                    return ImageName;
                }
                catch (Exception)
                {
                    throw new Exception("Run again!");
                }
            }
            else
            {
                return null;
            }
        }



        private static void Crop(int Width, int Height, Stream streamImg, string saveFilePath)
        {
            WebImage img = new WebImage(streamImg);
            img.Resize(Width, Height);
            img.Save(saveFilePath);
        }

    }
}
