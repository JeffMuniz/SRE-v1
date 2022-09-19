namespace Product.Supplier.Magalu.Worker.Jobs
{
    public class JobScheduleConfiguration
    {
        public int SupplierId { get; set; }

        public string CronExpression { get; set; }
    }
}
