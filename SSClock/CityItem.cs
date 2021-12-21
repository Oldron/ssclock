using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSClock {
  public class CityItem {
    public string value { get; set; }
    public string name { get; set; }
    public CityItem(string value, string name)
    {
      this.value = value;
      this.name = name;
    }
    public override string ToString()
    {
      return "{value: '" + value + "', name: '" + name + "'}";
    }
  }
}
