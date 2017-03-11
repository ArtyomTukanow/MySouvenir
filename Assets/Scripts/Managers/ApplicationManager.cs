
namespace Managers
{
    public class ApplicationManager
    {
        private ApplicationManager()
        {
        }

        public static UIManager UiManager {get { return UIManager.instance; }}
        public static ProductManager ProductManager {get { return ProductManager.instance; }}
        public static TestManager TestManager {get {return  TestManager.instance; }}

        public static void Start()
        {
            UiManager.Start();
        }
    }
}