using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CorporateNAVReporting.Helpers
{
    public class SharedFunction
    {
        public static void SetComboIndex(DropDownList comboName, string TextToSet)
        {
            comboName.SelectedIndex = comboName.Items.IndexOf(comboName.Items.FindByText(TextToSet));
        }
        public static void SetComboIndexByValue(DropDownList comboName, string ValueToSet)
        {
            comboName.SelectedIndex = comboName.Items.IndexOf(comboName.Items.FindByValue(ValueToSet));
        }
        public static void SetComboIndex(DropDownList comboName, Guid ValueToSet)
        {
            comboName.SelectedIndex = comboName.Items.IndexOf(comboName.Items.FindByValue(ValueToSet.ToString()));
        }

        public static Guid SetGuid(string stringGuidToCheck)
        {
            Guid outGuid;
            if (Guid.TryParse(stringGuidToCheck, out outGuid))
                return outGuid;
            else
                return Guid.Empty;
        }


    }
}