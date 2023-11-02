using log4net;
using System.Runtime.CompilerServices;

namespace HundirLaFlota
{
    internal class Logs
    {
        public static ILog GetLogger([CallerFilePath] string filename = "")
        {
            return LogManager.GetLogger(filename);
        }
    }
}
