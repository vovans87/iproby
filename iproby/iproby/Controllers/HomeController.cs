using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace iproby.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
               

       
        public ActionResult Index()
        {
            //string strFilePath = @"c:\Users\Administrator\Documents\Visual Studio 2013\Projects\iproby\iproby\Content\bootstrap\test.bat";
            //System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("cmd.exe");
            //psi.UseShellExecute = false;
            //psi.RedirectStandardOutput = true;
            //psi.RedirectStandardInput = true;
            //psi.RedirectStandardError = true;
         
            //System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi);
            //// Open the batch file for reading
            //System.IO.StreamReader strm = System.IO.File.OpenText(strFilePath);
            //// Attach the output for reading
            //System.IO.StreamReader sOut = proc.StandardOutput;
            //// Attach the in for writing
            //System.IO.StreamWriter sIn = proc.StandardInput;
            //// Write each line of the batch file to standard input
            //while (strm.Peek() != -1)
            //{
            //    sIn.WriteLine(strm.ReadLine());
            //}

            //strm.Close();
            //string stEchoFmt = "# {0} run successfully. Exiting";
            //sIn.WriteLine(String.Format(stEchoFmt, strFilePath));
            //sIn.WriteLine("Exit");
            //proc.Close();
            //string results = sOut.ReadToEnd().Trim();
            //sIn.Close();
            //sOut.Close();

            //Debug.Write(sIn);


            return View();
        }

        
    }
}
