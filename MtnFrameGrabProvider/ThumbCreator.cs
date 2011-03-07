using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using mveEngine;
using ICSharpCode.SharpZipLib.Zip;

namespace MtnFrameGrabProvider
{
    public static class ThumbCreator
    {
        const int MAX_PATH = 255;

        public static readonly string MtnPath = Path.Combine(ApplicationPaths.AppConfigPath, "mtn");
        public static readonly string MtnExe = Path.Combine(MtnPath, "mtn.exe");
        public static readonly string FrameGrabsPath = Path.Combine(MtnPath, "FrameGrabs");

        // We need our temp path as a short path due to a bug in mtn 
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)]
             string path,
            [MarshalAs(UnmanagedType.LPTStr)]
             StringBuilder shortPath,
            int shortPathLength
            );


        public static bool CreateThumb(string video, string destination, string secondsFromStart)
        {
            EnsureMtnIsExtracted();
            if (File.Exists(destination))
            {
                try
                {
                    File.Delete(destination);
                }
                catch { return false; }
            }
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = MtnExe;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            // see: http://moviethumbnail.sourceforge.net/usage.en.html
            psi.Arguments = string.Format("\"{0}\" -P -c 1 -r 1 -b 0,80 -B {1} -i -t -D 8 -o .jpg -O {2}",
                video, secondsFromStart, GetShortPath(Path.GetTempPath()));
            var process = Process.Start(psi);
            process.WaitForExit();

            string tempFile = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(video) + ".jpg");
            try
            {
                File.Move(tempFile, destination);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private static string GetShortPath(string path)
        {
            var shortPath = new StringBuilder(MAX_PATH);
            GetShortPathName(path, shortPath, MAX_PATH);
            return shortPath.ToString();
        }

        public static void EnsureMtnIsExtracted()
        {

            if (!Directory.Exists(MtnPath))
            {
                Directory.CreateDirectory(MtnPath);
                Directory.CreateDirectory(FrameGrabsPath);

                string name = "MtnFrameGrabProvider.mtn.zip";
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);

                using (var zip = new ZipInputStream(stream))
                {

                    ZipEntry entry;
                    while ((entry = zip.GetNextEntry()) != null)
                    {
                        string destination = Path.Combine(MtnPath, entry.Name);
                        File.WriteAllBytes(destination, ReadAllBytes(zip));
                    }
                }
            }
        }


        public static byte[] ReadAllBytes(this Stream source)
        {


            byte[] readBuffer = new byte[4096];

            int totalBytesRead = 0;
            int bytesRead = 0;

            while ((bytesRead = source.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;

                if (totalBytesRead == readBuffer.Length)
                {
                    int nextByte = source.ReadByte();
                    if (nextByte != -1)
                    {
                        byte[] temp = new byte[readBuffer.Length * 2];
                        Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                        Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                        readBuffer = temp;
                        totalBytesRead++;
                    }
                }
            }
            byte[] buffer = readBuffer;
            if (readBuffer.Length != totalBytesRead)
            {
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            }
            return buffer;
        }
    

    }
}
