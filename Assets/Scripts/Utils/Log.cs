namespace Utils
{
    public class Log
    {
        public static void d(string text)
        {
            Loader.print("DEBUG: " + text);
        }
    }
}