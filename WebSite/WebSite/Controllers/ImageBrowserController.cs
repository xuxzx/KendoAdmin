using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Controllers.Abstract;
using WebSite.Helper;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class ImageBrowserController : EditorImageBrowserController
    {
        private const string contentFolderRoot = "~/Images/";
        //private const string prettyName = "Images/";
        private static readonly string[] foldersToCopy = new[] { "~/Content/shared/" };


        /// <summary>
        /// Gets the base paths from which content will be served.
        /// </summary>
        public override string ContentPath
        {
            get
            {
                return CreateUserFolder();
            }
        }

        private string CreateUserFolder()
        {
            var virtualPath = Path.Combine(contentFolderRoot, "UserFiles");

            var path = Server.MapPath(virtualPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                foreach (var sourceFolder in foldersToCopy)
                {
                    CopyFolder(Server.MapPath(sourceFolder), path);
                }
            }
            return virtualPath;
        }

        private void CopyFolder(string source, string destination)
        {
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            foreach (var file in Directory.EnumerateFiles(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(file));
                System.IO.File.Copy(file, dest);
            }

            foreach (var folder in Directory.EnumerateDirectories(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(folder));
                CopyFolder(folder, dest);
            }
        }

        private string UploadPic(HttpPostedFileBase hpfb, string strFileName)
        {
            string server = Server.MapPath("~/Images/Upload/");

            hpfb.SaveAs(server + strFileName);

            //ImageHelper.MakeThumbnail(server + filename, server + "small_" + filename, 40, 40, "cut", "jpg");
            return "";
        }

        public ActionResult Save(IEnumerable<HttpPostedFileBase> pic)
        {
            if (pic != null)
            {
                foreach (var file in pic)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    string strFileName = System.IO.Path.GetFileName(file.FileName);
                    //string strFileExt = System.IO.Path.GetExtension(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images/Upload/"), strFileName);
                    if (System.IO.File.Exists(physicalPath))
                    {
                        return Content("图片名" + strFileName+"已存在");
                    }
                    else
                    {
                        UploadPic(file, strFileName);
                    }
                }
            }
            return Content("");
        }

        public ActionResult Remove(string[] fileNames)
        {
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images/Upload/"), fileName);

                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            return Content("");
        }
    }
}
