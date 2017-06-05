using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Services
{
    public interface IAuthorService
    {
        Task<Author> GetAuthorByIdAsync(int authorId);
    }
}
