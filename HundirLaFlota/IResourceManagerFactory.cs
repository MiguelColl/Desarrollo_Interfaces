using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundirLaFlota
{
    internal interface IResourceManagerFactory
    {
        System.Resources.ResourceManager CreateResourceManager();
    }
}
