using Application.Exceptions.CustomExceptions;
using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authors.Rules.BusinessRules;
public class AuthorBusinessRules
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorBusinessRules(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task CheckAuthorIsNull(int id)
    {
        Author? result = await _authorRepository.GetAsync(predicate: a => a.Id==id);

        if (result is null) { throw new BusinessException("AuthorMessages.AuthorIsNull"); }
    }
}
