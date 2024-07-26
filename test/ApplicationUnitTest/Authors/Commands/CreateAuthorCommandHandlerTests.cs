using Application.Features.Authors.Commands.CreateAuthor;
using Moq;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Microsoft.AspNetCore.Http;
using Application.Features.Books.Rules.BusinessRules;

namespace ApplicationUnitTest.Authors.Commands;
public class CreateAuthorCommandHandlerTests
{
    private readonly Mock<IAuthorRepository> _mockAuthorRepository;
    private readonly Mock<IMapper> _mockMapper;

    public CreateAuthorCommandHandlerTests()
    {
        _mockAuthorRepository = new Mock<IAuthorRepository>();
        _mockMapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreateAuthorCommandResponse()
    {
        // Arrange
        var handler = new CreateAuthorCommandHandler(_mockAuthorRepository.Object, _mockMapper.Object);
        var request = new CreateAuthorCommandRequest
        {
            Name = "Test Author",
            ImageUrl = GetTestFormFile()
        };

        var author = new Author { Name = "Test Author", ImageUrl = "test_image" };
        var response = new CreateAuthorCommandResponse { Id = 1, Name = "Test Author" };

        _mockMapper.Setup(m => m.Map<Author>(It.IsAny<CreateAuthorCommandRequest>())).Returns(author);
        _mockAuthorRepository.Setup(r => r.AddAsync(It.IsAny<Author>())).ReturnsAsync(author);
        _mockMapper.Setup(m => m.Map<CreateAuthorCommandResponse>(It.IsAny<Author>())).Returns(response);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Id, result.Id);
        Assert.Equal(response.Name, result.Name);
        _mockAuthorRepository.Verify(r => r.AddAsync(It.IsAny<Author>()), Times.Once);
    }

    private IFormFile GetTestFormFile()
    {
        var content = "Test image content";
        var fileName = "test_image.png";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        return new FormFile(stream, 0, stream.Length, "id_from_form", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png"
        };
    }
}
