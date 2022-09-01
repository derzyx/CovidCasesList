
namespace CovidCasesList.Models
{
    public class CountryCasesModel
    {
        public List<SingleCountry> Countries { get; set; }
        public DateTime Date { get; set; }
    }

    public class SingleCountry
    {
        public string Country { get; set; }
        public int TotalConfirmed { get; set; }
    }
}
