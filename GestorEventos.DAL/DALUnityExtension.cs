using System;
using Unity;
using Unity.Extension;
using Unity.Lifetime;

namespace GestorEventos.DAL
{
    public class DALUnityExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            //Container.RegisterType<Inferface, Implementation>();
        }
    }
}