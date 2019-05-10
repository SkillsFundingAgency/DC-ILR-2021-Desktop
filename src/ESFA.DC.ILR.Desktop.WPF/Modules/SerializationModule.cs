using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Json;
using ESFA.DC.Serialization.Xml;

namespace ESFA.DC.ILR.Desktop.WPF.Modules
{
    public class SerializationModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<XmlSerializationService>().As<IXmlSerializationService>();
            containerBuilder.RegisterType<JsonSerializationService>().As<IJsonSerializationService>().As<ISerializationService>();
        }
    }
}
