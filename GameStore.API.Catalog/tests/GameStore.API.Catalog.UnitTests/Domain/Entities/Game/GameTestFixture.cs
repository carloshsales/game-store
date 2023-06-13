using GameStore.API.Game.UnitTests.Common;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;
using DomainEntity = GameStore.API.Game.Domain.Entitites;

namespace GameStore.API.Game.UnitTests.Domain.Entities.Game
{
    public class GameTestFixture : BaseFixture
    {
        public GameTestFixture () : base() { }

        public string GetValidGameName ()
        {
            var gameName = new [] { "God of War", "Zelda - Ocarina Of Time", "Need for Speed - Underground", "League of Legends", "Ever quest" };
            return Faker.PickRandom( gameName );
        }

        public string GetValidGameDescription ()
        {
            return Faker.Lorem.Paragraph( 20 );
        }

        public decimal GetValidGamePrice ()
        {
            return Math.Round( Faker.Random.Decimal( 0, 1000 ), 2 );
        }

        public string GetValidGameImage ()
        {
            return Faker.Image.Abstract();
        }

        public DomainEntity.Game GetGameTestFixture () => new( GetValidGameName(), GetValidGameDescription(), GetValidGamePrice(), GetValidGameImage(), true );
    }

    [CollectionDefinition( nameof( GameTestFixture ) )]
    public class GameTestFixtureCollection : ICollectionFixture<GameTestFixture> { }
}
