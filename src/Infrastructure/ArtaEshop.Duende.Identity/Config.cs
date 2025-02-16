using Duende.IdentityServer.Models;

namespace ArtaEshop.Duende.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("catalogapi"),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        [
            //List of Microservices 
            new ApiResource("Catalog", "Catalog.API")
            {
                Scopes =["catalogapi"],
            }
        ];

    public static IEnumerable<Client> Clients =>
        [
            // m2m client credentials flow client
                new Client
                {
                    ClientId = "CatalogApiClient",
                    ClientName = "Catalog API Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "catalogapi" }
                },
        ];
}
