
namespace Managers
{
    public class ApplicationManager
    {
        private ApplicationManager()
        {
        }

        public static UIManager UiManager {get { return UIManager.instance; }}
        public static ProductManager ProductManager {get { return ProductManager.instance; }}
        public static ShopManager ShopManager {get { return ShopManager.instance; }}

        public static void Start()
        {
            UiManager.Start();
        }
    }
}