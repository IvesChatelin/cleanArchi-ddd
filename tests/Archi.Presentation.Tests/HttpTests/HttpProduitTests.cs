using System.Net.Http.Json;
using Archi.Common.Tests;
using Archi.Domain.ProduitAggregate;
using Archi.Domain.ProduitAggregate.Repositories;
using Archi.Domain.ProduitAggregate.ValueObjects;
using AutoFixture;
using Moq;
using static Archi.Presentation.EndPoints.ProduitsEndpoints.v1.CreateProduitEndpoint;

namespace Archi.Presentation.Tests.HttpTests;

[Collection(CollectionName.IntegrationTest)]
public class HttpProduitTests
{
    private readonly Mock<IProduitWriteRepository> _produitWriteRepositoryMock;
    private readonly Fixture _fixture;
    private readonly HttpClient _client;

    public HttpProduitTests(IntegrationTestWebAppFactory factory)
    {
        _produitWriteRepositoryMock = new Mock<IProduitWriteRepository>();
        _fixture = new Fixture();
        _client = factory.CreateClient();
    }

    // Should_<ExpectedBehavior>_When_<Context>()

    [Fact]
    public async Task Should_do_something()
    {
        var response = await _client.GetAsync("/healthz/live");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Should_Presist_Product_When_CommandIsValid()
    {
        // Arrange
        var request = _fixture.Create<CreateProduitRequest>();
        var response = await _client.PostAsJsonAsync("/api/v1/produits", request); 
        response.EnsureSuccessStatusCode();
    }
}
