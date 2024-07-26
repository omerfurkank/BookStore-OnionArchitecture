using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Authors.Commands.CreateRangeAuthor;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Moq;
using Xunit;

namespace ApplicationUnitTest.Authors.Commands;
public class CreateRangeAuthorCommandHandlerTests
{
    private readonly Mock<IAuthorRepository> _mockAuthorRepository;
    private readonly Mock<IMapper> _mockMapper;

    public CreateRangeAuthorCommandHandlerTests()
    {
        _mockAuthorRepository = new Mock<IAuthorRepository>();
        _mockMapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreateRangeAuthorCommandResponse()
    {
        // Arrange
        var handler = new CreateRangeAuthorCommandHandler(_mockAuthorRepository.Object, _mockMapper.Object);

        var authorDtos = new List<CreateRangeAuthorCommandRequest.AuthorDto>
        {
            new CreateRangeAuthorCommandRequest.AuthorDto { Name = "Author 1" },
            new CreateRangeAuthorCommandRequest.AuthorDto { Name = "Author 2" }
        };

        var request = new CreateRangeAuthorCommandRequest
        {
            Authors = authorDtos
        };

        var authors = new List<Author>
        {
            new Author { Name = "Author 1" },
            new Author { Name = "Author 2" }
        };

        _mockMapper.Setup(m => m.Map<List<Author>>(It.IsAny<List<CreateRangeAuthorCommandRequest.AuthorDto>>())).Returns(authors);
        _mockAuthorRepository.Setup(r => r.AddRangeAsync(It.IsAny<List<Author>>())).ReturnsAsync(authors);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _mockMapper.Verify(m => m.Map<List<Author>>(It.IsAny<List<CreateRangeAuthorCommandRequest.AuthorDto>>()), Times.Once);
        _mockAuthorRepository.Verify(r => r.AddRangeAsync(It.IsAny<List<Author>>()), Times.Once);
    }
}

