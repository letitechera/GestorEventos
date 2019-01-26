using System.Drawing;

namespace GestorEventos.BLL.Interfaces
{
    public interface IAccreditationLogic
    {
        Bitmap GenerateQRCode(int registrationID);
    }
}
