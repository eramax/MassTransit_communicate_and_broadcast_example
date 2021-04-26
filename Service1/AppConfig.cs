namespace Service1
{
    public class AppConfig
    {
        public MassTransitConfig MassTransit { get; set; }
    }
    public class MassTransitConfig
    {
        public string Host { get; set; }
        public string Queue { get; set; }
    }
}
