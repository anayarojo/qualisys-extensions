using System;

namespace QualisysExtensions.Controls
{
    /// <summary>
    ///     Modelo para los items del ComboBox
    /// </summary>
    [Serializable]
    public class ComboItem
    {
        /// <summary>
        ///     Valor
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        ///     Texto
        /// </summary>
        public string Text { get; set; }
    }
}
