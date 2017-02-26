using Net;
using UnityEngine;

namespace Items.Interface
{
    public interface IItemBase
    {
        /// <summary>
        /// Соединение для загрузки изображения.
        /// </summary>
        NetConnection ImageLoadConnection { set; get; }

        /// <summary>
        /// Привязанный к айтему GameObject.
        /// </summary>
        GameObject ContainerGameObject { set; get; }

        /// <summary>
        /// Создание объекта на поле
        /// </summary>
        /// <param name="container">Базовый GameObject</param>
        /// <param name="parent">Родитель создаваемого GameObject'а</param>
        void Create(GameObject container, Transform parent);
    }
}