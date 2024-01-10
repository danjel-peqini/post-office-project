
using IronBarCode;
using System.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.Mobile;
using ZXing.QrCode;
using QRCodeWriter = IronBarCode.QRCodeWriter;

namespace Helpers.Barcode
{
    public static class GenerateBarcode
    {

        public static Tuple<long, byte[]> GenerateNewBarcode()
        {
            // Your data to encode into the barcode (in this case, a 12-digit number)
            Random random = new Random();
            long randomNumber = (long)(random.NextDouble() * 900000000000L) + 100000000000L;
            byte[] imageBytes;

            // Create a BarcodeWriter instance
            GeneratedBarcode barcode = BarcodeWriter.CreateBarcode(randomNumber.ToString(), BarcodeEncoding.Code128, 300, 100);
            barcode.AddBarcodeValueTextBelowBarcode();
            barcode.SaveAsPng("barcode.png");

            //Add Annotation(text) below the generated barcode
            //var qrcode = QRCodeWriter.CreateQrCode(randomNumber.ToString());
            //qrcode.AddBarcodeValueTextBelowBarcode();


            //imageBytes = qrcode.ToJpegBinaryData();


          
            using (FileStream stream = File.OpenRead("barcode.png"))
            {
                imageBytes = new byte[stream.Length];
                stream.Read(imageBytes, 0, imageBytes.Length);
                
            }
            File.Delete("barcode.png");
            return Tuple.Create(randomNumber, imageBytes);
        }
    }
}
