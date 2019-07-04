using GestorEventos.BLL.Interfaces;
using QRCoder;
using System.Drawing;
using System.IO;

namespace GestorEventos.BLL
{
    public class AccreditationLogic : IAccreditationLogic
    {
        public byte[] GenerateQRCode(int participantId)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(participantId.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap bitmap = qrCode.GetGraphic(20);

            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
    }
}
