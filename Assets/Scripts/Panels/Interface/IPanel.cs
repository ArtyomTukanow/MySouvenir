using UnityEngine;

namespace Panels
{
    public interface IPanel
    {
        /// <summary>
        /// Базовый контейнер.
        /// </summary>
        GameObject ItemsContainer { set; get; }
        /// <summary>
        /// Обязтельный метод после инициализации параметров.
        /// </summary>
        void ReloadPanel();
    }
}