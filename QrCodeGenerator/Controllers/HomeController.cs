using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing;
using ZXing.Common;

namespace QrCodeGenerator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string text = "123456789 - John Doe";
            ViewBag.QrCode = GenerateQRCode(text);
            ViewBag.Text = text;
            return View();
        }

        private string GenerateQRCode(string qrcodeText)
        {
            var qrWriter = new BarcodeWriter();
            qrWriter.Format = BarcodeFormat.QR_CODE;
            qrWriter.Options = new EncodingOptions() { Height = 100, Width = 100, Margin = 2 };

            using (var q = qrWriter.Write(qrcodeText))
            {
                using (var ms = new MemoryStream())
                {
                    q.Save(ms, ImageFormat.Png);
                    return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}