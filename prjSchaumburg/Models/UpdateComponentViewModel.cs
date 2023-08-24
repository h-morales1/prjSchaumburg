namespace prjSchaumburg.Models
{
    public class UpdateComponentViewModel
    {
        public Guid Id { get; set; }
        public Guid machineID { get; set; }
        public string Name { get; set; }
        public string serialNum { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }

    }
}
