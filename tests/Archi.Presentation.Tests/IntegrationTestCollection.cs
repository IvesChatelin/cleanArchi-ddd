using Archi.Common.Tests;

namespace Archi.Presentation.Tests;

[CollectionDefinition(CollectionName.IntegrationTest)]
public sealed class IntegrationTestCollection : ICollectionFixture<IntegrationTestWebAppFactory>
{
    // Rien à l'interieur 
    // il sert juste à dire qu'on utilise un conteneur pour toutes les classes de tests
}
