using Items.Interface;

namespace Panels
{
    public interface IPanelItems <T> : IPanel where T : IItemBase
    {
        /// <summary>
        /// Массив предметов контейнера
        /// </summary>
        T[] Items { set; get; }
    }
}