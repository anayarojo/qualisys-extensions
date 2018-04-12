# Qualisys Extensions

#### Nuget installation:

```
Install-Package QualisysLog -Version 1.0.1
```

#### Basic use:

```csharp
using QualisysExtensions.Controls;
using QualisysExtensions.Date;

public partial class frmLogViewer : Form
{
    public enum DateFilterEnum : int
    {
        [DescriptionAttribute("Hoy")]
        TODAY = 1,
        [DescriptionAttribute("Esta semana")]
        CURRENT_WEEK = 2,
        [DescriptionAttribute("Este mes")]
        CURRENT_MONTH = 3,
        [DescriptionAttribute("Este año")]
        CURRENT_YEAR = 4,
        [DescriptionAttribute("Última semana")]
        LAST_WEEK = 5,
        [DescriptionAttribute("Último mes")]
        LAST_MONTH = 6,
        [DescriptionAttribute("Último año")]
        LAST_YEAR = 7,
        [DescriptionAttribute("Rango")]
        RANGE = 8,
    }

    public frmLogViewer()
    {
        //Load combo from an enum
        cboDateFilter.LoadDataSource(ComboBoxExtension.GetComboItemListFromEnum<DateFilterEnum>("Seleccione"));
        cboDateFilter.SelectedIndex = 1;
    }

    private void ChangeDateFilter(ComboBox pObjComboBox)
    {
        dtmDateFrom.Enabled = false;
        dtmDateTo.Enabled = false;
        dtmDateFrom.Value = DateTime.Today;
        dtmDateTo.Value = DateTime.Today;

        if (pObjComboBox.SelectedIndex > 0)
        {
            switch ((DateFilterEnum)pObjComboBox.SelectedValue)
            {
                case DateFilterEnum.CURRENT_WEEK:
                    dtmDateFrom.Value = DateTime.Today.StartOfWeek(DayOfWeek.Monday);
                    dtmDateTo.Value = dtmDateFrom.Value.AddDays(6);
                    break;
                case DateFilterEnum.CURRENT_MONTH:
                    dtmDateFrom.Value = new DateTime(dtmDateFrom.Value.Year, dtmDateFrom.Value.Month, 1);
                    dtmDateTo.Value = dtmDateFrom.Value.AddMonths(1).AddDays(-1);
                    break;
                case DateFilterEnum.CURRENT_YEAR:
                    dtmDateFrom.Value = new DateTime(dtmDateFrom.Value.Year, 1, 1);
                    dtmDateTo.Value = dtmDateFrom.Value.AddYears(1).AddDays(-1);
                    break;
                case DateFilterEnum.LAST_WEEK:
                    dtmDateFrom.Value = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6);
                    dtmDateTo.Value = dtmDateFrom.Value.AddDays(6);
                    break;
                case DateFilterEnum.LAST_MONTH:
                    dtmDateFrom.Value = DateTime.Today.AddMonths(-1);
                    dtmDateFrom.Value = new DateTime(dtmDateFrom.Value.Year, dtmDateFrom.Value.Month, 1);
                    dtmDateTo.Value = dtmDateFrom.Value.AddMonths(1).AddDays(-1);
                    break;
                case DateFilterEnum.LAST_YEAR:
                    dtmDateFrom.Value = DateTime.Today.AddYears(-1);
                    dtmDateFrom.Value = new DateTime(dtmDateFrom.Value.Year, 1, 1);
                    dtmDateTo.Value = dtmDateFrom.Value.AddYears(1).AddDays(-1);
                    break;
                case DateFilterEnum.RANGE:
                    dtmDateFrom.Enabled = true;
                    dtmDateTo.Enabled = true;
                    break;
            }
        }
    }
}
```

See more

[Nuget package page](https://www.nuget.org/packages/QualisysExtensions/)