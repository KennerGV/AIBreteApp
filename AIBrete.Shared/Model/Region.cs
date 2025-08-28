using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace AIBrete.Shared.Model
{

    public class Region
    {
        public string Code { get; set; }
        public string NameEs { get; set; }
        public string NameEn { get; set; }
    }

    public static class CountryProvider
    {
        public static List<Region> GetCountries()
        {
            try
            {
                var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

                var regions = cultures
                    .Select(c =>
                    {
                        try
                        {
                            return new RegionInfo(c.Name);
                        }
                        catch (ArgumentException)
                        {
                            return null;
                        }
                    })
                    .Where(r => r != null)
                    .GroupBy(r => r.TwoLetterISORegionName)
                    .Select(g => g.First())
                    .ToList();

                var countries = regions.Select(r =>
                {
                    var nameEs = new RegionInfo("es-ES").DisplayName;
                    return new Region
                    {
                        Code = r.TwoLetterISORegionName,
                        NameEs = r.EnglishName
                    };
                })
                .OrderBy(c => c.NameEs)
                .ToList();

                return countries;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

        }
    }

}
