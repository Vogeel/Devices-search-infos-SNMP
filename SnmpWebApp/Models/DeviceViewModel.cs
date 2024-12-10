namespace SnmpWebApp.Models
{
    public class DeviceViewModel
    {
        public string IP { get; set; }
        public string OID { get; set; }
        public string DeviceName { get; set; }
        public string Username { get; set; }
        public string UpTime { get; set; }
        public string SysDescription { get; set; }
        public string Result { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
