using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MohammadReza
{
    public class Upload : Controller 
    {
        // 
        private MohammadReza.Encryption enc = new Encryption();
        // Image Valid Extension
        private readonly string[] ImageExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif" };
        //
        private string ImageName;

        /**
        *   Image Upload
        *   
        *   @param String DIR
        *   @param HttpPostedFileBase File
        *   @return String File Name
        */
        public string ImageUpload(string dir, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0 && ImageExtensions.Contains(System.IO.Path.GetExtension(file.FileName).ToLower()) && file.ContentType.Contains("image"))
            {
                try
                {
                    ImageName = enc.UUID() + System.IO.Path.GetExtension(file.FileName);
                    string path = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(dir), System.IO.Path.GetFileName(ImageName));
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
    }
}