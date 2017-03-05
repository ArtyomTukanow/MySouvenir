using Assets.Scripts.Layers.Enum;
using UnityEngine;

namespace Assets.Scripts.Layers
{
    public interface ILayerBase
    {
        LayerNamesEnum Name { get; }
        GameObject Canvas { get; }

        /// <summary>
        /// Метод запускается при активации окна
        /// </summary>
        void OnVisable();
        /// <summary>
        /// Метод запускается при деактивации окна
        /// </summary>
        void OnDisable();
        /// <summary>
        /// Метод перезагружает окно
        /// </summary>
        void OnReload();
    }
}