using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GestorEventos.DAL;
using Unity;
using Unity.Extension;

namespace GestorEventos.BLL
{
    public class BLLUnityExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.AddNewExtension<DALUnityExtension>();

            //Container.RegisterType<Inferface, Implementation>();
        }
    }
}