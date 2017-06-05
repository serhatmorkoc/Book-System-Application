using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Data.Entities;

namespace Services
{
   public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            if (categoryId == 0)
                return null;

            var result = await _categoryRepository.GetByIdAsync(categoryId);
            return result;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var query = await _categoryRepository.TableAsync();
            query = query.OrderBy(x => x.Name);
            return query;
        }
    }
}
