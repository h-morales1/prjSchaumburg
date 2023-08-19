using prjSchaumburg.Models.Domain;

namespace prjSchaumburg.Models
{
    public class AddComponentViewModel
    {
        public Guid machineID { get; set; }
        public string Name { get; set; }
        public string serialNum { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }
    }
}
