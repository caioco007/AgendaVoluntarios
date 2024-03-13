using AgendaVoluntarios.Data.Entities;
using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;
using AgendaVoluntarios.Repositories.Interfaces;
using AgendaVoluntarios.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.UnitTestes.Services
{
    public class MusicServiceTests
    {

        [Fact]
        public async Task IsMusicLinkedToEvent_ReturnsTrue_WhenMusicIsLinkedToEvent()
        {
            // Arrange
            var mockEventMusicRepository = new Mock<IEventMusicRepository>();
            var musicId = Guid.NewGuid();
            mockEventMusicRepository.Setup(repo => repo.GetEventsByMusicIdAsync(musicId))
                                    .ReturnsAsync(new List<EventMusic> {
                                        new EventMusic(Guid.NewGuid(), musicId, Guid.NewGuid()),
                                        new EventMusic(Guid.NewGuid(), musicId, Guid.NewGuid())
                                    }); // Supondo que há pelo menos um evento vinculado à música
            var musicService = new MusicService(null, mockEventMusicRepository.Object);

            // Act
            var result = await musicService.IsMusicLinkedToEvent(musicId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsMusicLinkedToEvent_ReturnsFalse_WhenMusicIsNotLinkedToEvent()
        {
            // Arrange
            var mockEventMusicRepository = new Mock<IEventMusicRepository>();
            var musicId = Guid.NewGuid();
            mockEventMusicRepository.Setup(repo => repo.GetEventsByMusicIdAsync(musicId))
                                    .ReturnsAsync(new List<EventMusic>()); // Nenhum evento vinculado à música
            var musicService = new MusicService(null, mockEventMusicRepository.Object);

            // Act
            var result = await musicService.IsMusicLinkedToEvent(musicId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllMusics()
        {
            // Arrange
            var mockRepository = new Mock<IMusicRepository>();
            var musics = new List<Music>
            {
                new Music(Guid.NewGuid(), "Music 1", "Key 1"),
                new Music(Guid.NewGuid(), "Music 2", "Key 2"),
                new Music(Guid.NewGuid(), "Music 3", "Key 3")
            };
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(musics);
            var service = new MusicService(mockRepository.Object, null);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(musics.Count, result.Count);
            Assert.All(result, m => Assert.IsType<MusicViewModel>(m));
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMusicViewModel()
        {
            // Arrange
            var mockRepository = new Mock<IMusicRepository>();
            var musicId = Guid.NewGuid();
            var music = new Music(musicId, "Music 1", "Key 1");
            mockRepository.Setup(repo => repo.GetByIdAsync(musicId)).ReturnsAsync(music);
            var service = new MusicService(mockRepository.Object, null);

            // Act
            var result = await service.GetByIdAsync(musicId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MusicViewModel>(result);
            Assert.Equal(musicId, result.Id);
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryAddAsync()
        {
            // Arrange
            var mockRepository = new Mock<IMusicRepository>();
            var service = new MusicService(mockRepository.Object, null);
            var inputModel = new NewMusicInputModel { Name = "New Music", Key = "New Key" };

            // Act
            await service.AddAsync(inputModel);

            // Assert
            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Music>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryUpdateAsync()
        {
            // Arrange
            var mockRepository = new Mock<IMusicRepository>();
            var service = new MusicService(mockRepository.Object, null);
            var inputModel = new EditMusicInputModel { Id = Guid.NewGuid(), Name = "Updated Music", Key = "Updated Key" };

            // Act
            await service.UpdateAsync(inputModel);

            // Assert
            mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Music>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepositoryDeleteAsync()
        {
            // Arrange
            var mockRepository = new Mock<IMusicRepository>();
            var service = new MusicService(mockRepository.Object, null);
            var musicId = Guid.NewGuid();

            // Act
            await service.DeleteAsync(musicId);

            // Assert
            mockRepository.Verify(repo => repo.DeleteAsync(musicId), Times.Once);
        }
    }
}
