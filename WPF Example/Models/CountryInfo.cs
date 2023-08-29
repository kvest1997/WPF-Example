using System.Collections.Generic;

namespace WPF_Example.Models
{
    /// <summary>
    /// Информаия о стране
    /// </summary>
    internal class CountryInfo : PlaceInfo 
    {   
        public IEnumerable<ProvinceInfo> ProvinceCounts { get; set; }
    }
}
