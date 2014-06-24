using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace blackSheep.Helper
{
    public class HtmlToPdf
    {
        public static bool HtmltoPdf(string Url, string outputFilename)
        {
            string filename = outputFilename;

            Process p = new System.Diagnostics.Process();
            p.StartInfo.Arguments = Url + " " + filename;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;

            p.StartInfo.FileName = HttpContext.Current.Server.MapPath("~/bin/") + "wkhtmltopdf.exe";

            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();

            p.WaitForExit(60000);

            // read the exit code, close process
            int returnCode = p.ExitCode;
            p.Close();

            // if 0 or 2, it works
            return (returnCode == 0 || returnCode == 2);
        }
    }
}