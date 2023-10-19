namespace HundirLaFlota
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                Console.SetWindowSize(67, 40);

            Game game = new Game();
            game.Launch();
        }
    }
}