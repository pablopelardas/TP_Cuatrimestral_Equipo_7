using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace Negocio.Servicios
{
    public class PdfServicio
    {
        public enum TransmitMethod
        {
            None,
            Attachment,
            Inline
        }

        public static void GeneratePdf(string url, string filePath)
        {
            var chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

            if (!File.Exists(chromePath))
            {
                string userfolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                chromePath = $@"{userfolder}\AppData\Local\Google\Chrome\Application\chrome.exe";
            }

            if (!File.Exists(chromePath))
            {
                throw new Exception("Unable to locate Chrome.exe");
            }

            using (var p = new Process())
            {
                p.StartInfo.FileName = chromePath;
                //--headless --disable-gpu --print-to-pdf=file1.pdf
                p.StartInfo.Arguments = $"--headless --disable-gpu --run-all-compositor-stages-before-draw --print-to-pdf=\"{filePath}\" {url}";
                p.Start();
                p.WaitForExit();
            }

            GC.Collect();
        }

        public static void GeneratePdfInline(string html)
        {
            ChromePublish(html, TransmitMethod.Inline, null);
        }

        public static void GeneratePdfAttachment(string html, string filenameWithPdf)
        {
            ChromePublish(html, TransmitMethod.Attachment, filenameWithPdf);
        }

        static void ChromePublish(string html, TransmitMethod transmitMethod, string filename)
        {
            string folderTemp = HttpContext.Current.Server.MapPath("~/temp/pdf");

            if (!Directory.Exists(folderTemp))
            {
                Directory.CreateDirectory(folderTemp);
            }

            Random rd = new Random();

            string randomstr = rd.Next(100000000, int.MaxValue).ToString();

            string fileHtml = HttpContext.Current.Server.MapPath($"~/temp/pdf/{randomstr}.html");
            string filePdf = HttpContext.Current.Server.MapPath($"~/temp/pdf/{randomstr}.pdf");

            File.WriteAllText(fileHtml, html);

            var r = HttpContext.Current.Request.Url;

            string url = $"{r.Scheme}://{r.Host}:{r.Port}/temp/pdf/{randomstr}.html";

            GeneratePdf(url, filePdf);

            FileInfo fi = new FileInfo(filePdf);
            byte[] ba = File.ReadAllBytes(filePdf);

            string filelength = fi.Length.ToString();

            try
            {
                File.Delete(filePdf);
            }
            catch { }

            try
            {
                File.Delete(fileHtml);
            }
            catch { }

            // open new http response in new tab

            
            // Page page = (Page)context.Handler;
            // if (page == null) {
            //     throw new InvalidOperationException(
            //         "Cannot redirect to new window outside Page context.");
            // }
            // url = page.ResolveClientUrl(url);
            //
            // string script;
            // if (!String.IsNullOrEmpty(windowFeatures)) {
            //     script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
            // }
            // else {
            //     script = @"window.open(""{0}"", ""{1}"");";
            // }
            //
            // script = String.Format(script, url, target, windowFeatures);
            // ScriptManager.RegisterStartupScript(page,
            //     typeof(Page),
            //     "Redirect",
            //     script,
            //     true);
            
            
            HttpContext.Current.Response.Clear();

            if (transmitMethod == TransmitMethod.Inline)
                HttpContext.Current.Response.AddHeader("Content-Disposition", "inline");
            else if (transmitMethod == TransmitMethod.Attachment)
                HttpContext.Current.Response.AddHeader("Content-Disposition", $"attachment; filename=\"{filename}\"");

            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("Content-Length", filelength);
            HttpContext.Current.Response.BinaryWrite(ba);
            // write response to new tab
            HttpContext.Current.Response.End();
            // Add this line to avoid the error "Thread was being aborted"
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}