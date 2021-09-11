using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SSClock {
    public partial class FormConfig : Form {

        public const string REG_KEY_APP = "SOFTWARE\\SSClock";
        public const string REG_FLD_API_KEY = "ApiKey";

        public FormConfig() {
            InitializeComponent();
        }

        private void FormConfig_Load(Object sender, EventArgs e) {
            LoadSettings();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e) {
            SaveSettings();
            Close();
        }

        private void LoadSettings() {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(REG_KEY_APP);
            if ( key == null ) {
                tbApiKey.Text = "";
            } else {
                tbApiKey.Text = (string)key.GetValue(REG_FLD_API_KEY);
            }
        }

        private void SaveSettings() {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(REG_KEY_APP);
            key.SetValue(REG_FLD_API_KEY, tbApiKey.Text);
        }
    }
}
