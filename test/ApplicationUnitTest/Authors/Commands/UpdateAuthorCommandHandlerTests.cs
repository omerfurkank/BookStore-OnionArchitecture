using Application.Features.Authors.Commands.UpdateAuthor;
using Application.Features.Authors.Rules.BusinessRules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUnitTest.Authors.Commands;
public class UpdateAuthorCommandHandlerTests
{
    private readonly Mock<IAuthorRepository> _mockAuthorRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly AuthorBusinessRules _businessRules;

    public UpdateAuthorCommandHandlerTests()
    {
        _mockAuthorRepository = new Mock<IAuthorRepository>();
        _mockMapper = new Mock<IMapper>();
        _businessRules = new AuthorBusinessRules(_mockAuthorRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsUpdateAuthorCommandResponse()
    {
        // Arrange
        var handler = new UpdateAuthorCommandHandler(_mockAuthorRepository.Object, _mockMapper.Object, _businessRules);

        var request = new UpdateAuthorCommandRequest
        {
            Id = 1,
            Name = "Updated Author",
            ImageUrl = GetTestFormFile()
        };

        var existingAuthor = new Author { Id = 1, Name = "Existing Author", ImageUrl = "existing_image" };
        var updatedAuthor = new Author { Id = 1, Name = "Updated Author", ImageUrl = "updated_image_base64" };
        var response = new UpdateAuthorCommandResponse { Id = 1, Name = "Updated Author" };

        // Define the predicate expression
        //Expression<Func<Author, bool>> predicate = author => author.Id == request.Id;

        // Setup GetAsync to return the existing author
        _mockAuthorRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Author, bool>>>(), null, true))
                        .ReturnsAsync(existingAuthor);

        // Mock Mapper
        _mockMapper.Setup(m => m.Map(request, existingAuthor)).Returns(updatedAuthor);
        _mockAuthorRepository.Setup(r => r.UpdateAsync(updatedAuthor)).ReturnsAsync(updatedAuthor);
        _mockMapper.Setup(m => m.Map<UpdateAuthorCommandResponse>(updatedAuthor)).Returns(response);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Id, result.Id);
        Assert.Equal(response.Name, result.Name);

        // Verify that GetAsync was called with the correct predicate
        _mockAuthorRepository.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Author, bool>>>(), null, true), Times.Once);

        // Verify other methods
        _mockMapper.Verify(m => m.Map(request, existingAuthor), Times.Once);
        _mockAuthorRepository.Verify(r => r.UpdateAsync(updatedAuthor), Times.Once);
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