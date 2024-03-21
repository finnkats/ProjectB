using System.Collections.Generic;
using System.Text.Json.Serialization;

public class AccountDataModel
{
    public class AdminAccount
    {
        [JsonPropertyName("AdminName")]
        public string? AdminName { get; set; }

        [JsonPropertyName("AdminPassword")]
        public string? AdminPassword { get; set; }
    }

    public class CustomerAccount
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }

    [JsonPropertyName("AdminAccount")]
    public AdminAccount? Admin { get; set; }

    [JsonPropertyName("CustomerAccount")]
    public List<CustomerAccount>? Customers { get; set; }
}
