using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GestorEventos.BLL.Interfaces
{
    public interface IImagesLogic
    {
        string LoadImage(FileInfo image, string blobName);
    }
}
