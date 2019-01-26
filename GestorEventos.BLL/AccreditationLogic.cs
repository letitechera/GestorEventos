using GestorEventos.BLL.Interfaces;
using QRCoder;
using System.Drawing;

namespace GestorEventos.BLL
{
    public class AccreditationLogic : IAccreditationLogic
    {
        public Bitmap GenerateQRCode(int registrationID)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(registrationID.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }
    }
}
