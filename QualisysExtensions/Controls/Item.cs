using System;

namespace QualisysExtensions.Controls
{
    /// <summary>
    ///     Modelo puente para los items del ComboBox
    /// </summary>
    [Serializable]
    public class Item
    {
        /// <summary>
        ///     Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Nombre
        /// </summary>
        public string Name { get; set; }
    }
}
