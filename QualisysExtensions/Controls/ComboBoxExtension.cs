using QualisysExtensions.Enums;
using QualisysExtensions.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QualisysExtensions.Controls
{
    /// <summary>
    ///     Extensión para agilizar el trabajo con ComboBoxes
    /// </summary>
    /// <remarks>
    ///     Raul Anaya, 19/12/2017
    /// </remarks>
    public static class ComboBoxExtension
    {
        /// <summary>
        ///    Método para cargar una la lista de un ComboBox
        /// </summary>
        /// <param name="pObjComboBox">
        ///     ComboBox
        /// </param>
        /// <param name="pLstObjDataSource">
        ///     Lista
        /// </param>
        public static void LoadDataSource(this ComboBox pObjComboBox, IList<ComboItem> pLstObjDataSource)
        {
            pObjComboBox.DataSource = pLstObjDataSource;
            pObjComboBox.DisplayMember = "Text";
            pObjComboBox.ValueMember = "Value";
            pObjComboBox.Text = "Favor de seleccionar";
        }

        /// <summary>
        ///     Método para cargar un enum en el ComboBox
        /// </summary>
        /// <typeparam name="T">
        ///     Enum
        /// </typeparam>
        /// <param name="pObjComboBox">
        ///     ComboBox
        /// </param>
        public static void LoadDataSource<T>(this ComboBox pObjComboBox) where T : struct, IConvertible
        {
            pObjComboBox.LoadDataSource(GetComboItemListFromEnum<T>());
        }

        /// <summary>
        ///     Método para convertir un enum en una lista para ComboBox
        /// </summary>
        /// <typeparam name="T">
        ///     Enum
        /// </typeparam>
        /// <returns>
        ///     Lista ComboBox
        /// </returns>
        public static IList<ComboItem> GetComboItemListFromEnum<T>() where T : struct, IConvertible
        {
            IList<ComboItem> lLstObjComboResult = new List<ComboItem>();

            int lIntValueItem = 0;
            string lStrLabelItem = null;

            if (typeof(T).IsEnum)
            {
                foreach (Enum lObjItem in typeof(T).GetEnumValues())
                {
                    lIntValueItem = Convert.ToInt32(lObjItem);
                    lStrLabelItem = lObjItem.GetDescription();
                    lLstObjComboResult.Add(new ComboItem() { Value = lIntValueItem, Text = lStrLabelItem });
                }
                return lLstObjComboResult;
            }
            else
            {
                throw new Exception("La clase seleccionada no es del tipo enumerador.");
            }
        }

        /// <summary>
        ///     Método para convertir un enum en una lista para ComboBox
        /// </summary>
        /// <typeparam name="T">
        ///     Enum
        /// </typeparam>
        /// <param name="pStrDefaultItem">
        ///     Item default
        /// </param>
        /// <returns>
        ///     Lista para ComboBox
        /// </returns>
        public static IList<ComboItem> GetComboItemListFromEnum<T>(string pStrDefaultItem) where T : struct, IConvertible
        {
            IList<ComboItem> lLstObjComboResult = new List<ComboItem>();
            lLstObjComboResult.Add(new ComboItem() { Value = 0, Text = pStrDefaultItem });

            int lIntValueItem = 0;
            string lStrLabelItem = null;

            if (typeof(T).IsEnum)
            {
                foreach (Enum lObjItem in typeof(T).GetEnumValues())
                {
                    lIntValueItem = Convert.ToInt32(lObjItem);
                    lStrLabelItem = lObjItem.GetDescription();
                    lLstObjComboResult.Add(new ComboItem() { Value = lIntValueItem, Text = lStrLabelItem });
                }
                return lLstObjComboResult;
            }
            else
            {
                throw new Exception("La clase seleccionada no es del tipo enumerador.");
            }
        }

        /// <summary>
        ///     Método para obtener una lista para ComboBox
        /// </summary>
        /// <param name="pUnkLstObjList">
        ///     Lista de entidades
        /// </param>
        /// <returns>
        ///     Lista para ComboBox
        /// </returns>
        public static IList<ComboItem> GetComboItemListFromList<T>(this IList<T> pUnkLstObjList) where T : class
        {
            IList<ComboItem> lLstObjComboResult = new List<ComboItem>();

            foreach (Item lObjItem in pUnkLstObjList.ParseList<T,Item>())
            {
                lLstObjComboResult.Add(new ComboItem() { Value = lObjItem.Id, Text = lObjItem.Name });
            }

            return lLstObjComboResult;
        }

        /// <summary>
        ///     Método para obtener una lista para ComboBox
        /// </summary>
        /// <param name="pUnkLstObjList">
        ///     Lista de entidades
        /// </param>
        /// <param name="pStrDefaultItem">
        ///     Item default
        /// </param>
        /// <returns>
        ///     Lista para ComboBox
        /// </returns>
        public static IList<ComboItem> GetComboItemListFromList<T>(this IList<T> pUnkLstObjList, string pStrDefaultItem) where T : class
        {
            IList<ComboItem> lLstObjComboResult = new List<ComboItem>();
            lLstObjComboResult.Add(new ComboItem() { Value = 0, Text = pStrDefaultItem });

            foreach (Item lObjItem in pUnkLstObjList.ParseList<T, Item>())
            {
                lLstObjComboResult.Add(new ComboItem() { Value = lObjItem.Id, Text = lObjItem.Name });
            }

            return lLstObjComboResult;
        }

        /// <summary>
        ///     Método para validar la sección de un ComboBox
        /// </summary>
        /// <param name="pObjCombo">
        ///     ComboBox
        /// </param>
        /// <returns>
        ///     Si es valido
        /// </returns>
        public static bool IsValid(this ComboBox pObjCombo)
        {
            if (((ComboItem)pObjCombo.SelectedItem).Value == -1)
            {
                return false;
            }
            return (int)pObjCombo.SelectedIndex == 0 ? false : true;
        }

        /// <summary>
        ///     Método para reiniciar el ComboBox
        /// </summary>
        /// <param name="pObjCombo">
        ///     ComboBox
        /// </param>
        /// <param name="pIntId">
        ///     Id
        /// </param>
        /// <param name="pStrName">
        ///     Nombre
        /// </param>
        /// <returns></returns>
        public static ComboBox ReInitCombo(this ComboBox pObjCombo, int pIntId, string pStrName)
        {
            pObjCombo.DataSource = LookForDisabledItem(pObjCombo, pIntId, pStrName);
            pObjCombo.DisplayMember = "Text";
            pObjCombo.ValueMember = "Value";
            pObjCombo.SelectedIndex = HaveDisabledItem(pObjCombo) ? GetIndexForCombo(pObjCombo, -1) : GetIndexForCombo(pObjCombo, pIntId);
            return pObjCombo;
        }

        /// <summary>
        ///     Método para obtener el índice de un id en el combo
        /// </summary>
        /// <param name="pObjCombo">
        ///     ComboBox
        /// </param>
        /// <param name="pIntId">
        ///     Id
        /// </param>
        /// <returns>
        ///     Indíce
        /// </returns>
        public static int GetIndexForCombo(this ComboBox pObjCombo, int pIntId)
        {
            for (int x = 0; x <= pObjCombo.Items.Count - 1; x++)
            {
                if (((ComboItem)pObjCombo.Items[x]).Value == pIntId)
                    return x;
            }
            return 0;
        }

        /// <summary>
        ///     Método para buscar los items inválidos
        /// </summary>
        /// <param name="pObjCombo">
        ///     ComboBox
        /// </param>
        /// <param name="pIntId">
        ///     Id
        /// </param>
        /// <param name="pStrName">
        ///     Nombre
        /// </param>
        /// <returns>
        ///     Lista de items del ComboBox
        /// </returns>
        private static IList<ComboItem> LookForDisabledItem(ComboBox pObjCombo, int pIntId, string pStrName)
        {
            IList<ComboItem> lLstObj = new List<ComboItem>();
            bool lBolResult = false;

            foreach (ComboItem lObjComboDTO in pObjCombo.Items)
            {
                if (lObjComboDTO.Value == pIntId)
                {
                    lBolResult = true;
                }
                lLstObj.Add(new ComboItem() { Value = lObjComboDTO.Value, Text = lObjComboDTO.Text });
            }
            if (!lBolResult)
            {
                lLstObj.Add(new ComboItem()
                {
                    Value = -1,
                    Text = pStrName != string.Empty ? pStrName : "Registro desactivado, favor de seleccionar otro."
                });
            }
            return lLstObj.OrderBy(a => a.Text).ToList();
        }

        /// <summary>
        ///     Método que indica si un ComboBox tiene items inválidos.
        /// </summary>
        /// <param name="pObjCombo">
        ///     ComboBox
        /// </param>
        /// <returns>
        ///     Si es inválido
        /// </returns>
        private static bool HaveDisabledItem(ComboBox pObjCombo)
        {
            bool lBolResult = false;
            foreach (ComboItem lObjComboDTO in pObjCombo.Items)
            {
                if (lObjComboDTO.Value == -1)
                    lBolResult = true;
            }
            return lBolResult;
        }
    }
}
