using System;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SSClock {
  public partial class FormConfig : Form {

    public const string REG_KEY_APP = "SOFTWARE\\SSClock";
    public const string REG_FLD_API_KEY = "ApiKey";
    public const string REG_FLD_CITY_CODE = "CityCode";
    public const string CITY_CODE_VALUE_NOT_SELECTED = "not selected";
    private const string CITY_CODE_VALUE_UNKNOWN = "unknown city";

    private List<CityRec> listCities;
    private readonly Dictionary<string, CityRec> dicCities = new Dictionary<string, CityRec>();
    public readonly List<CityItem> listCityItems = new List<CityItem>();

    public FormConfig()
    {
      InitializeComponent();
    }

    public static string GetCityCode()
    {
      RegistryKey key = Registry.CurrentUser.OpenSubKey(REG_KEY_APP);
      if (key == null)
      {
        return CITY_CODE_VALUE_NOT_SELECTED;
      }
      string cityCode = (string)key.GetValue(REG_FLD_CITY_CODE);
      return !string.IsNullOrEmpty(cityCode) ? cityCode : CITY_CODE_VALUE_NOT_SELECTED;
    }

    public void RefreshCity() {
      string cityCode = GetCityCode();
      if (!string.IsNullOrEmpty(cityCode) && CITY_CODE_VALUE_NOT_SELECTED != cityCode)
      {
        CityRec cityRec = dicCities[cityCode];
        lblCityValue.Text = null != cityRec
          ? cityRec.name + " " + cityRec.country + " " + cityRec.id + " (" + cityRec.coord.lon + " " + cityRec.coord.lat + ")"
          : CITY_CODE_VALUE_UNKNOWN + " " + cityCode;
      }
      else
      {
        lblCityValue.Text = CITY_CODE_VALUE_NOT_SELECTED;
      }
    }

    private void FormConfig_Load(Object sender, EventArgs e)
    {
      InitSitiesData();
      LoadSettings();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      SaveSettings();
      Close();
    }

    private void LoadSettings()
    {
      RegistryKey key = Registry.CurrentUser.OpenSubKey(REG_KEY_APP);
      if (key == null)
      {
        tbApiKey.Text = "";
      }
      else
      {
        tbApiKey.Text = (string)key.GetValue(REG_FLD_API_KEY);
      }
      RefreshCity();
    }

    private void SaveSettings()
    {
      RegistryKey key = Registry.CurrentUser.CreateSubKey(REG_KEY_APP);
      key.SetValue(REG_FLD_API_KEY, tbApiKey.Text);
    }

    private void InitSitiesData()
    {
      if (0 < dicCities.Count)
      {
        return;
      }
      try
      {
        string sityListJson = SSClock.Properties.Resources.city_list;
        System.IO.TextReader tr = new System.IO.StringReader(sityListJson);
        JsonReader reader = new JsonTextReader(tr);
        JsonSerializer serializer = new JsonSerializer();
        listCities = serializer.Deserialize<List<CityRec>>(reader);
        foreach (var city in listCities)
        {
          dicCities.Add(city.id.ToString(), city);
          CityItem cityItem = new CityItem(city.id.ToString(), city.name + " " + city.country + " (" + city.coord.lon + " " + city.coord.lat + ")");
          listCityItems.Add(cityItem);
        }
        listCityItems.Sort((item1, item2) => item1 == null ? (item2 == null ? 0 : -1) : (item2 == null ? 1 : item1.name.CompareTo(item2.name)));
      }
      catch (Exception e)
      {
        Console.WriteLine("Error load cities data: " + e);
      }
    }

    private void btnCitySelect_Click(object sender, EventArgs e)
    {
      FormCitySelect formCitySelect = new FormCitySelect(this);
      formCitySelect.ShowDialog();
    }
  }
}
