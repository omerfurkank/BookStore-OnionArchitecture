using Application.Features.Authors.Queries.GetListAuthor;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUnitTest.Authors.Queries;
public class GetListAuthorQueryHandlerTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetListAuthorQueryHandler _handler;

    public GetListAuthorQueryHandlerTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetListAuthorQueryHandler(_mapperMock.Object, _authorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_AuthorsExist_ReturnsMappedAuthors()
    {
        // Arrange
        var request = new GetListAuthorQueryRequest { Index = 0, Size = 10 };
        var authors = new List<Author>
        {
            new Author { Id = 1, Name = "Author 1" },
            new Author { Id = 2, Name = "Author 2" }
        };
        var response = new List<GetListAuthorQueryResponse>
        {
            new GetListAuthorQueryResponse { Id = 1, Name = "Author 1" },
            new GetListAuthorQueryResponse { Id = 2, Name = "Author 2" }
        };

        _authorRepositoryMock.Setup(repo => repo.GetListAsync(
            It.IsAny<Expression<Func<Author, bool>>>(),
            It.IsAny<Func<IQueryable<Author>, IIncludableQueryable<Author, object>>>(),
            It.IsAny<bool>(),
            request.Index,
            request.Size
        )).ReturnsAsync(authors);

        _mapperMock.Setup(mapper => mapper.Map<IList<GetListAuthorQueryResponse>>(authors))
            .Returns(response);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(response);
        _authorRepositoryMock.Verify(repo => repo.GetListAsync(
            It.IsAny<Expression<Func<Author, bool>>>(),
            It.IsAny<Func<IQueryable<Author>, IIncludableQueryable<Author, object>>>(),
            It.IsAny<bool>(),
            request.Index,
            request.Size
        ), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<IList<GetListAuthorQueryResponse>>(authors), Times.Once);
    }

    [Fact]
    public async Task Handle_NoAuthors_ReturnsEmptyList()
    {
        // Arrange
        var request = new GetListAuthorQueryRequest { Index = 0, Size = 10 };
        var authors = new List<Author>();
        var response = new List<GetListAuthorQueryResponse>();

        _authorRepositoryMock.Setup(repo => repo.GetListAsync(
            It.IsAny<Expression<Func<Author, bool>>>(),
            It.IsAny<Func<IQueryable<Author>, IIncludableQueryable<Author, object>>>(),
            It.IsAny<bool>(),
            request.Index,
            request.Size
        )).ReturnsAsync(authors);

        _mapperMock.Setup(mapper => mapper.Map<IList<GetListAuthorQueryResponse>>(authors))
            .Returns(response);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(response);
        _authorRepositoryMock.Verify(repo => repo.GetListAsync(
            It.IsAny<Expression<Func<Author, bool>>>(),
            It.IsAny<Func<IQueryable<Author>, IIncludableQueryable<Author, object>>>(),
            It.IsAny<bool>(),
            request.Index,
            request.Size
        ), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<IList<GetListAuthorQueryResponse>>(authors), Times.Once);
    }

    [Fact]
    public async Task Handle_GetListAsyncThrowsException_ThrowsException()
    {
        // Arrange
        var request = new GetListAuthorQueryRequest { Index = 0, Size = 10 };

        _authorRepositoryMock.Setup(repo => repo.GetListAsync(
            It.IsAny<Expression<Func<Author, bool>>>(),
            It.IsAny<Func<IQueryable<Author>, IIncludableQueryable<Author, object>>>(),
            It.IsAny<bool>(),
            request.Index,
            request.Size
        )).ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
    }
}

