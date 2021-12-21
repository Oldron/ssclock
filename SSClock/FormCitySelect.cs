using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SSClock {
  public partial class FormCitySelect : Form {

    private FormConfig formConfig;
    public FormCitySelect(FormConfig formConfig)
    {
      this.formConfig = formConfig;
      InitializeComponent();
      fillCb();
    }

    private void fillCb()
    {
      cbCity.DisplayMember = "name";
      cbCity.ValueMember = "value";
      cbCity.DataSource = new BindingSource(formConfig.listCityItems, null);

      string cityCode = FormConfig.GetCityCode();
      if (FormConfig.CITY_CODE_VALUE_NOT_SELECTED != cityCode) {
        cbCity.SelectedValue = cityCode;
      } else {
        cbCity.SelectedIndex = -1;
      }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      string cityCode = (string)cbCity.SelectedValue;
      if (!string.IsNullOrEmpty(cityCode)) {
        RegistryKey key = Registry.CurrentUser.CreateSubKey(FormConfig.REG_KEY_APP);
        key.SetValue(FormConfig.REG_FLD_CITY_CODE, cityCode);
        formConfig.RefreshCity();
      }
      Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
