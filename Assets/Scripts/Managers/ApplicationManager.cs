
namespace Managers
{
    public class ApplicationManager
    {
        private ApplicationManager()
        {
        }

        public static UIManager uiManager {get { return UIManager.instance; }}
        public static ProductManager productManager {get { return ProductManager.instance; }}

        public static void Start()
        {
            uiManager.Start();
        }
    }
}