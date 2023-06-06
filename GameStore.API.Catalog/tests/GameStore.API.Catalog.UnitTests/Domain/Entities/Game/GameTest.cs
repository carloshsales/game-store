using GameStore.API.Game.Domain.Exceptions;
using Xunit;
using DomainEntity = GameStore.API.Game.Domain.Entitites;
namespace GameStore.API.Game.UnitTests.Domain.Entities.Game;

public class GameTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Game - Aggregates")]
    public void Instantiate()
    {
        var validData = new
        {
            Name = "Game Name",
            Description = "Description here!",
            Price = 100.0m,
            Image = "http://urlimagegame.com",
        };

        var dateBefore = DateTime.Now;
        var game = new DomainEntity.Game(validData.Name, validData.Description, validData.Price, validData.Image);
        var dateAfter = DateTime.Now;

        Assert.NotNull(game);
        Assert.Equal(validData.Name, game.Name);
        Assert.Equal(validData.Description, game.Description);
        Assert.Equal(validData.Price, game.Price);
        Assert.Equal(validData.Image, game.Image);
        Assert.NotEqual(default(Guid), game.Id);
        Assert.NotEqual(default(DateTime), game.CreatedAt);
        Assert.True(game.CreatedAt >  dateBefore);
        Assert.True(game.CreatedAt < dateAfter);
        Assert.True(game.IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [Trait("Domain", "Game - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        var validData = new
        {
            Name = "Game Name",
            Description = "Description here!",
            Price = 100.0m,
            Image = "http://urlimagegame.com",
        };

        var dateBefore = DateTime.Now;
        var game = new DomainEntity.Game(validData.Name, validData.Description, validData.Price, validData.Image, isActive );
        var dateAfter = DateTime.Now;

        Assert.NotNull(game);
        Assert.Equal(validData.Name, game.Name);
        Assert.Equal(validData.Description, game.Description);
        Assert.Equal(validData.Price, game.Price);
        Assert.Equal(validData.Image, game.Image);
        Assert.NotEqual(default(Guid), game.Id);
        Assert.NotEqual(default(DateTime), game.CreatedAt);
        Assert.True(game.CreatedAt > dateBefore);
        Assert.True(game.CreatedAt < dateAfter);
        Assert.Equal(isActive, game.IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsNullOrEmpty))]
    [Trait("Domain", "Game - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void InstantiateErrorWhenNameIsNullOrEmpty(string? name)
    {
        Action action = () => new DomainEntity.Game(name!, "Game Description", 0.0m, "http://image.com", true);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name shold not be empty or null", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Game - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        Action action = () => new DomainEntity.Game("Valid name", null!, 0.0m, "http://image.com", true);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description shold not be null", exception.Message);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Game - Aggregates")]
    [InlineData("ab")]
    [InlineData("a")]
    public void InstantiateErrorWhenNameIsLessThan3Characters(string invalidName)
    {
        Action action = () => new DomainEntity.Game(invalidName, "Game description", 0.0m, "http://image.com", true);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name shold be at least 3 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Game - Aggregates")]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters()
    {
        string invalidName = new string('A', 255);
        Action action = () => new DomainEntity.Game(invalidName, "Game description", 0.0m, "http://image.com", true);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name shold be less or equal 255 characters long", exception.Message);
    }
}
