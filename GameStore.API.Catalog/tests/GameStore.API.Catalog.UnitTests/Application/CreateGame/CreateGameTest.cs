using GameStore.API.Game.Domain.Entitites;
using Moq;
using Xunit;
using DimainEntity = GameStore.API.Game.Domain.Entitites;
using UseCases = GameStore.API.Game.Application.UseCases.CreateGame;

namespace GameStore.API.Game.UnitTests.Application.CreateGame
{
    public class CreateGameTest
    {

        [Fact( DisplayName = nameof( CreateGame ) )]
        [Trait( "Application", "CreateGame - Use Cases" )]
        public async void CreateGame ()
        {
            var repositoryMock = new Mock<IGameRepository>();
            var unityOfWorkMock = new Mock<IUnitOfWork>();

            var useCase = new UseCases.CreateGame( repositoryMock.Object, unityOfWorkMock.Object );

            var input = new CreateGameInput( "Game name", "Game Description", 0.0m, "http://image.com", true );

            repositoryMock.Verify( repository => repository.Create( It.IsAny<DimainEntity.Game>(), It.IsAny<CancellationToken>() ), Times.Once );
            unityOfWorkMock.Verify( unityOfWork => unityOfWork.Commit( It.IsAny<CancellationToken>() ), Times.Once );

            var result = await useCase.Execute( input, CancellationToken.None );

            Assert.NotNull( result );
            Assert.Empty( result.Name, input.Name );
            Assert.Empty( result.Description, input.Description );
            Assert.Empty( result.Price, input.Price );
            Assert.Empty( result.Image, input.Image );
            Assert.Empty( result.IsActice, input.IsActice );
            Assert.True( result.Id != null && result.Id != Guid.Empty );
            Assert.True( result.CreatedAt != null && result.CreatedAt != default( DateTime ) );
        }

    }
}
