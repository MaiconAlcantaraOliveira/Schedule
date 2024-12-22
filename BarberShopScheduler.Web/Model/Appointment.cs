namespace BarberShopScheduler.Web.Model
{
    public class Appointment
    {
        public string Model { get; set; }
        public string BarberShop { get; set; }
        public string BarberShopId { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string ServiceDescription { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

}
