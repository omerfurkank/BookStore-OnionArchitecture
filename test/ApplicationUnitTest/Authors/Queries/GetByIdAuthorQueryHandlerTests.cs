using Application.Features.Authors.Queries.GetByIdAuthor;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Application.Features.Authors.Rules.BusinessRules;

namespace ApplicationUnitTest.Authors.Queries;
public class GetByIdAuthorQueryHandlerTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AuthorBusinessRules _businessRules;
    private readonly GetByIdAuthorQueryHandler _handler;

    public GetByIdAuthorQueryHandlerTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _mapperMock = new Mock<IMapper>();
        _businessRules = new AuthorBusinessRules(_authorRepositoryMock.Object);
        _handler = new GetByIdAuthorQueryHandler(_authorRepositoryMock.Object, _mapperMock.Object,_businessRules);
    }

    [Fact]
    public async Task Handle_AuthorExists_ReturnsAuthor()
    {
        // Arrange
        var request = new GetByIdAuthorQueryRequest { Id = 1 };
        var author = new Author { Id = 1, Name = "Test Author", ImageUrl = "test.jpg" };
        var response = new GetByIdAuthorQueryResponse { Id = 1, Name = "Test Author", ImageUrl = "test.jpg" };

        _authorRepositoryMock.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Author, bool>>>(), null, true)).ReturnsAsync(author);
        _mapperMock.Setup(mapper => mapper.Map<GetByIdAuthorQueryResponse>(author)).Returns(response);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(response);
        _authorRepositoryMock.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Author, bool>>>(), null, true), Times.Exactly(2));
        _mapperMock.Verify(mapper => mapper.Map<GetByIdAuthorQueryResponse>(author), Times.Once);
    }

    [Fact]
    public async Task Handle_AuthorDoesNotExist_ThrowsException()
    {
        // Arrange
        var request = new GetByIdAuthorQueryRequest { Id = 1 };

        _authorRepositoryMock.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Author, bool>>>(), null, true)).ReturnsAsync((Author)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("AuthorMessages.AuthorIsNull");
        _authorRepositoryMock.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Author, bool>>>(), null, true), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<GetByIdAuthorQueryResponse>(It.IsAny<Author>()), Times.Never);
    }
}
