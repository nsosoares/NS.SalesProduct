namespace NS.SalesProduct.Services.API.Extensions
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpirationInHours { get; set; }
        public string Issuer { get; set; }
    }
}
