namespace GestorEventos.BLL.Interfaces
{
    public interface IAccreditationLogic
    {
        byte[] GenerateQRCode(int participantId);
    }
}
