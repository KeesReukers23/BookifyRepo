using Interfaces;
using Interfaces.IRepos;
using Logic.Entities;
using Logic.ExtensionMethods;
using Logic.Services;
using Moq;

namespace Bookify.Tests.UnitTests.Logic.Services
{
    public class PostServiceTests
    {
        private readonly Mock<IPostRepository> _mockPostRepository;
        private readonly PostService _postService;

        public PostServiceTests()
        {
            _mockPostRepository = new Mock<IPostRepository>();
            _postService = new PostService(_mockPostRepository.Object);
        }

        [Fact]
        public async Task AddPost_Succes_ReturnsPostId()
        {
            // Arrange
            Guid postId = new Guid();
            Post post = new Post(5, "Review", postId, "Title");


            PostDto expectedPostDto = post.toDto();

            _mockPostRepository.Setup(repo => repo.CreatePostAsync(It.IsAny<PostDto>()))
                                .ReturnsAsync(expectedPostDto.PostId);

            // Act
            var result = await _postService.AddPost(post);

            // Assert
            Assert.Equal(expectedPostDto.PostId, result);
        }

        [Fact]
        public async Task AddPost_Failure_ReturnsNull()
        {
            // Arrange
            Guid postId = new Guid();
            Post post = new Post(5, "Review", postId, "Title");


            PostDto expectedPostDto = post.toDto();

            _mockPostRepository.Setup(repo => repo.CreatePostAsync(It.IsAny<PostDto>()))
                                      .ReturnsAsync((Guid?)null);

            // Act
            var result = await _postService.AddPost(post);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeletePost_Success_ReturnsTrue()
        {
            // Arrange
            var postId = Guid.NewGuid();
            _mockPostRepository.Setup(repo => repo.DeletePostAsync(postId)).ReturnsAsync(true);

            // Act
            var result = await _postService.DeletePost(postId);

            // Assert
            Assert.True(result);
            _mockPostRepository.Verify(repo => repo.DeletePostAsync(postId), Times.Once);
        }

        [Fact]
        public async Task UpdatePost_Success_ReturnsTrue()
        {
            // Arrange
            Guid postId = new Guid();
            Post post = new Post(5, "Review", postId, "Title");

            var postDto = post.toDto();
            _mockPostRepository.Setup(repo => repo.UpdatePostAsync(It.IsAny<PostDto>())).ReturnsAsync(true);

            // Act
            var result = await _postService.UpdatePost(post);

            // Assert
            Assert.True(result);
            _mockPostRepository.Verify(repo => repo.UpdatePostAsync(It.Is<PostDto>(dto => dto.PostId == postDto.PostId)), Times.Once);
        }

        [Fact]
        public async Task GetPostById_PostExists_ReturnsPost()
        {
            // Arrange
            Guid postId = new Guid();
            Post post = new Post(5, "Review", postId, "Title");

            PostDto postDto = post.toDto();

            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(postId)).ReturnsAsync(postDto);

            // Act
            var result = await _postService.GetPostById(postId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(postDto.PostId, result?.PostId);
            _mockPostRepository.Verify(repo => repo.GetPostByIdAsync(postId), Times.Once);
        }

        [Fact]
        public async Task GetPostById_PostDoesNotExist_ReturnsNull()
        {
            // Arrange
            var postId = Guid.NewGuid();
            _mockPostRepository.Setup(repo => repo.GetPostByIdAsync(postId)).ReturnsAsync((PostDto?)null);

            // Act
            var result = await _postService.GetPostById(postId);

            // Assert
            Assert.Null(result);
            _mockPostRepository.Verify(repo => repo.GetPostByIdAsync(postId), Times.Once);
        }

        [Fact]
        public async Task GetAllPostsFromUser_ReturnsPosts()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var postDtos = new List<PostDto>
        {
            new PostDto { PostId = Guid.NewGuid(), Review = "Review 1", UserId = userId, CreatedAt = DateTime.UtcNow, Title = "Title 1" },
            new PostDto { PostId = Guid.NewGuid(), Review = "Review 2", UserId = userId, CreatedAt = DateTime.UtcNow, Title = "Title 2" }
        };

            _mockPostRepository.Setup(repo => repo.GetAllPostsFromUserAsync(userId)).ReturnsAsync(postDtos);

            // Act
            var result = await _postService.GetAllPostsFromUser(userId);

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(postDtos.Count, result.Count());
            _mockPostRepository.Verify(repo => repo.GetAllPostsFromUserAsync(userId), Times.Once);
        }

    }
}
