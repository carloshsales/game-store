using GameStore.API.Game.Domain.Exceptions;
using Xunit;
using DomainEntity = GameStore.API.Game.Domain.Entitites;
namespace GameStore.API.Game.UnitTests.Domain.Entities.Game;

[Collection( nameof( GameTestFixture ) )]
public class GameTest
{
    private readonly GameTestFixture _gameTestFixture;

    public GameTest ( GameTestFixture gameTestFixture )
    {
        _gameTestFixture = gameTestFixture;
    }

    [Fact( DisplayName = nameof( Instantiate ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    public void Instantiate ()
    {
        var validData = _gameTestFixture.GetGameTestFixture();

        var dateBefore = DateTime.Now;
        var game = new DomainEntity.Game( validData.Name, validData.Description, validData.Price, validData.Image );
        var dateAfter = DateTime.Now.AddSeconds( 1 );

        Assert.NotNull( game );
        Assert.Equal( validData.Name, game.Name );
        Assert.Equal( validData.Description, game.Description );
        Assert.Equal( validData.Price, game.Price );
        Assert.Equal( validData.Image, game.Image );
        Assert.NotEqual( default( Guid ), game.Id );
        Assert.NotEqual( default( DateTime ), game.CreatedAt );
        Assert.True( game.CreatedAt >= dateBefore );
        Assert.True( game.CreatedAt <= dateAfter );
        Assert.True( game.IsActive );


        //********* FLUENT ASSERTIONS
        //game.Should().NotBeNull();
        //game.Name.Should().Be( validData.Name );
        //game.Description.Should().Be( validData.Description );
        //game.Price.Should().Be( validData.Price );
        //game.Image.Should().Be( validData.Image );
        //game.Id.Should().NotBeEmpty();
        //game.CreatedAt.Should().NotBeSameDateAs( default( DateTime ) );
        //(game.CreatedAt > dateBefore).Should().BeTrue();
        //(game.CreatedAt < dateAfter).Should().BeTrue();
        //game.IsActive.Should().BeTrue();
    }

    [Theory( DisplayName = nameof( InstantiateErrorWhenNameIsNullOrEmpty ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    [InlineData( "" )]
    [InlineData( null )]
    [InlineData( "    " )]
    public void InstantiateErrorWhenNameIsNullOrEmpty ( string? name )
    {
        var validData = _gameTestFixture.GetGameTestFixture();
        Action action = () => new DomainEntity.Game( name!, validData.Description, validData.Price, validData.Image, true );

        var exception = Assert.Throws<EntityValidationException>( action );
        Assert.Equal( "Name shold not be empty or null", exception.Message );
    }

    [Theory( DisplayName = nameof( InstantiateErrorWhenNameIsLessThan3Characters ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    [InlineData( "ab" )]
    [InlineData( "a" )]
    public void InstantiateErrorWhenNameIsLessThan3Characters ( string invalidName )
    {
        var validData = _gameTestFixture.GetGameTestFixture();
        Action action = () => new DomainEntity.Game( invalidName, validData.Description, validData.Price, validData.Image, true );

        var exception = Assert.Throws<EntityValidationException>( action );
        Assert.Equal( "Name shold be at least 3 characters long", exception.Message );
    }

    [Fact( DisplayName = nameof( InstantiateErrorWhenNameIsGreaterThan255Characters ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters ()
    {
        var validData = _gameTestFixture.GetGameTestFixture();
        string invalidName = new string( 'A', 256 );
        Action action = () => new DomainEntity.Game( invalidName, validData.Description, validData.Price, validData.Image, true );

        var exception = Assert.Throws<EntityValidationException>( action );
        Assert.Equal( "Name shold be less or equal 255 characters long", exception.Message );
    }

    [Fact( DisplayName = nameof( InstantiateErrorWhenDescriptionIsNull ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    public void InstantiateErrorWhenDescriptionIsNull ()
    {
        var validData = _gameTestFixture.GetGameTestFixture();
        Action action = () => new DomainEntity.Game( validData.Name, null!, validData.Price, validData.Image, true );

        var exception = Assert.Throws<EntityValidationException>( action );
        Assert.Equal( "Description shold not be null", exception.Message );
    }

    [Fact( DisplayName = nameof( InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    public void InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters ()
    {
        string invalidDescription = new string( 'a', 10_001 );
        var validData = _gameTestFixture.GetGameTestFixture();
        Action action = () => new DomainEntity.Game( validData.Name, invalidDescription, validData.Price, validData.Image, true );
        var exception = Assert.Throws<EntityValidationException>( action );
        Assert.Equal( "Description shold be less or equal 10_000 characters long", exception.Message );
    }

    [Fact( DisplayName = nameof( InstantiateErrorWhenPriceIsNegative ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    public void InstantiateErrorWhenPriceIsNegative ()
    {
        var validData = _gameTestFixture.GetGameTestFixture();
        Action action = () => new DomainEntity.Game( validData.Name, validData.Description, -1.0m, validData.Image, true );
        var exception = Assert.Throws<EntityValidationException>( action );
        Assert.Equal( "The Price should be equal to or greater than 0", exception.Message );
    }

    [Theory( DisplayName = nameof( InstantiateWithIsActive ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    [InlineData( true )]
    [InlineData( false )]
    public void InstantiateWithIsActive ( bool isActive )
    {
        var validData = _gameTestFixture.GetGameTestFixture();

        var dateBefore = DateTime.Now;
        var game = new DomainEntity.Game( validData.Name, validData.Description, validData.Price, validData.Image, isActive );
        var dateAfter = DateTime.Now.AddSeconds( 1 );

        Assert.NotNull( game );
        Assert.Equal( validData.Name, game.Name );
        Assert.Equal( validData.Description, game.Description );
        Assert.Equal( validData.Price, game.Price );
        Assert.Equal( validData.Image, game.Image );
        Assert.NotEqual( default( Guid ), game.Id );
        Assert.NotEqual( default( DateTime ), game.CreatedAt );
        Assert.True( game.CreatedAt >= dateBefore );
        Assert.True( game.CreatedAt <= dateAfter );
        Assert.Equal( isActive, game.IsActive );
    }

    [Fact( DisplayName = nameof( Activate ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    public void Activate ()
    {
        var validData = _gameTestFixture.GetGameTestFixture();

        var game = new DomainEntity.Game( validData.Name, validData.Description, validData.Price, validData.Image, false );
        game.Activate();

        Assert.True( game.IsActive );
    }

    [Fact( DisplayName = nameof( Deactivate ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    public void Deactivate ()
    {
        var validData = _gameTestFixture.GetGameTestFixture();

        var game = new DomainEntity.Game( validData.Name, validData.Description, validData.Price, validData.Image, true );
        game.Deactivate();

        Assert.False( game.IsActive );
    }

    [Fact( DisplayName = nameof( Update ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    public void Update ()
    {
        var gameEntity = _gameTestFixture.GetGameTestFixture();
        var newValues = _gameTestFixture.GetGameTestFixture();

        var beforeDate = DateTime.Now;
        gameEntity.Update( newValues.Name, newValues.Description, newValues.Price, newValues.Image );
        var afterDate = DateTime.Now;

        Assert.Equal( newValues.Name, gameEntity.Name );
        Assert.Equal( newValues.Description, gameEntity.Description );
        Assert.Equal( newValues.Price, gameEntity.Price );
        Assert.Equal( newValues.Image, gameEntity.Image );
        Assert.True( gameEntity.UpdatedAt > beforeDate );
        Assert.True( gameEntity.UpdatedAt < afterDate );
    }

    [Theory( DisplayName = nameof( UpdateOnlyProperties ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    [InlineData( "New name", null, 0.0, null )]
    [InlineData( null, "New Description", 0.0, null )]
    [InlineData( null, null, 9.0, null )]
    [InlineData( null, null, 0.0, "newimage.com" )]
    public void UpdateOnlyProperties ( string name, string description, decimal price, string image )
    {
        var currentValues = _gameTestFixture.GetGameTestFixture();
        var gameEntity = new DomainEntity.Game( currentValues.Name, currentValues.Description, currentValues.Price, currentValues.Image, true );
        gameEntity.Update( name, description, price, image );

        Assert.Equal( name ?? currentValues.Name, gameEntity.Name );
        Assert.Equal( description ?? currentValues.Description, gameEntity.Description );
        Assert.Equal( price >= 0.0m ? price : currentValues.Price, gameEntity.Price );
        Assert.Equal( image ?? currentValues.Image, gameEntity.Image );
    }

    [Theory( DisplayName = nameof( UpdateErrorWhenNameIsLessThan3Characters ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    [InlineData( "ab" )]
    [InlineData( "a" )]
    public void UpdateErrorWhenNameIsLessThan3Characters ( string invalidName )
    {
        var currentGame = _gameTestFixture.GetGameTestFixture();
        Action action = () => currentGame.Update( invalidName, "Game description", 0.0m, currentGame.Image );

        var exception = Assert.Throws<EntityValidationException>( action );
        Assert.Equal( "Name shold be at least 3 characters long", exception.Message );
    }

    [Fact( DisplayName = nameof( UpdateErrorWhenNameIsGreaterThan255Characters ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    public void UpdateErrorWhenNameIsGreaterThan255Characters ()
    {
        string invalidName = _gameTestFixture.Faker.Lorem.Letter( 256 );
        var currentGame = _gameTestFixture.GetGameTestFixture();
        Action action = () => currentGame.Update( invalidName, "Game description", 0.0m, currentGame.Image );

        var exception = Assert.Throws<EntityValidationException>( action );
        Assert.Equal( "Name shold be less or equal 255 characters long", exception.Message );
    }

    [Fact( DisplayName = nameof( UpdateErrorWhenDescriptionIsGreaterThan10_000Characters ) )]
    [Trait( "Domain", "Game - Aggregates" )]
    public void UpdateErrorWhenDescriptionIsGreaterThan10_000Characters ()
    {
        string invalidDescription = _gameTestFixture.Faker.Lorem.Letter( 10_001 );

        var currentGame = _gameTestFixture.GetGameTestFixture();
        Action action = () => currentGame.Update( null, invalidDescription, 0.0m, currentGame.Image );
        var exception = Assert.Throws<EntityValidationException>( action );
        Assert.Equal( "Description shold be less or equal 10_000 characters long", exception.Message );
    }
}
