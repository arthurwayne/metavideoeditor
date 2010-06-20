using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Drawing;

namespace mveEngine
{
    public class ImageUtil
    {
        
        public static string GetLocalImagePath(string ImgPath)
        {
            if (string.IsNullOrEmpty(ImgPath))
                return string.Empty;
            if (File.Exists(ImgPath))
                return ImgPath;
            if (File.Exists(GetLocalImgPath(ImgPath)))
                return GetLocalImgPath(ImgPath);
            if (ImgPath.StartsWith("http://") && DownloadUsingRetry(ImgPath))
                return GetLocalImgPath(ImgPath);
            else
                return string.Empty;
        }

        public static bool LocalFileExists(string ImgPath, out string localPath)
        {
            localPath = "";
            if (string.IsNullOrEmpty(ImgPath)) return false;
            if (File.Exists(ImgPath))
                localPath = ImgPath;
            else if (File.Exists(GetLocalImgPath(ImgPath)))
                localPath = GetLocalImgPath(ImgPath);
            return !string.IsNullOrEmpty(localPath);
        }

        static string GetLocalImgPath(string ImgPath)
        {

            return Path.Combine(ApplicationPaths.AppImagePath, Helper.GetMD5(ImgPath) + Path.GetExtension(ImgPath));
        }

        static bool DownloadUsingRetry(string ImgPath)
        {
            int attempt = 0;
            bool success = false;
            while (attempt < 2)
            {
                try
                {
                    attempt++;
                    DownloadImage(ImgPath);
                    success = true;
                    break;
                }
                catch 
                {
                }
            }
            return success;
        }

        static void DownloadImage(string ImgPath)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(ImgPath);
            req.Timeout = 30000;
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            using (MemoryStream ms = new MemoryStream())
            {
                Stream r = resp.GetResponseStream();
                int read = 1;
                byte[] buffer = new byte[10000];
                while (read > 0)
                {
                    read = r.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, read);
                }
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);


                using (var stream = ProtectedFileStream.OpenExclusiveWriter(GetLocalImgPath(ImgPath)))
                {
                    stream.Write(ms.ToArray(), 0, (int)ms.Length);
                }
                ms.Close(); 
            }
        }

        public static Image SetImgOpacity(Image imgPic, float imgOpac)
        {

            Bitmap bmpPic = new Bitmap(imgPic.Width, imgPic.Height);
            Graphics gfxPic = Graphics.FromImage(bmpPic);
            System.Drawing.Imaging.ColorMatrix cmxPic = new System.Drawing.Imaging.ColorMatrix();
            cmxPic.Matrix33 = imgOpac;
            System.Drawing.Imaging.ImageAttributes iaPic = new System.Drawing.Imaging.ImageAttributes();
            iaPic.SetColorMatrix(cmxPic, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
            gfxPic.DrawImage(imgPic, new Rectangle(0, 0, bmpPic.Width, bmpPic.Height), 0, 0, imgPic.Width, imgPic.Height, GraphicsUnit.Pixel, iaPic);
            gfxPic.Dispose();
            return bmpPic;
        }
        
    }
}