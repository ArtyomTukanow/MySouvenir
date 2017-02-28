using Items.Interface;
using UnityEngine;

namespace Panels
{
    public interface IPanel <T> where T : IItemBase
    {
        /// <summary>
        /// Базовый контейнер.
        /// </summary>
        GameObject ItemsContainer { set; get; }
        /// <summary>
        /// Массив предметов контейнера
        /// </summary>
        T[] Items { set; get; }
        /// <summary>
        /// Обязтельный метод перед инициализацией параметров.
        /// </summary>
        void InitPanel(GameObject panel, GameObject parent);
        /// <summary>
        /// Обязтельный метод после инициализации параметров.
        /// </summary>
        void ReloadPanel();
    }
}