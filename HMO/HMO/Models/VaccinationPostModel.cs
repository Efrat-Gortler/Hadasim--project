namespace HMO.API.Models
{
    public class VaccinationPostModel
    {
        public int ManufacturerId { get; set; }
        public DateTime Date { get; set; }
        public int MemberId { get; set; }
    }
}
