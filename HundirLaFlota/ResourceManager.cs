using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundirLaFlota
{
    internal class ResourceManager
    {
        private IResourceManagerFactory factory;
        private System.Resources.ResourceManager resourceManager;

        public ResourceManager(string lang)
        {
            if(lang == "es")
            {
                factory = new EspResourceManagerFactory();
            }
            else if(lang == "en")
            {
                factory = new EngResourceManagerFactory();
            }
            else
            {
                throw new ArgumentException("Idioma desconocido");
            }

            resourceManager = factory.CreateResourceManager();
        }

        public string GetString(string keyName)
        {
            return resourceManager.GetString(keyName);
        }
    }
}
