namespace WebAPI.Options {
    public class RateLimitingOptions {
        public int PermitLimit { get; set; }
        public TimeSpan Window { get; set; }
    }
}
