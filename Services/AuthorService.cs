using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core;
using Data.Entities;

namespace Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;

        #region Ctor

        public AuthorService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        #endregion

        #region Methods

        public async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            if (authorId == 0)
                return null;

            var result = await _authorRepository.GetByIdAsync(authorId);
            return result;
        }

        #endregion


    }
}
