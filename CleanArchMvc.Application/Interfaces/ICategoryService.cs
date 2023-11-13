using CleanArchMvc.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int? id);
        Task AddCategoryAsync(CategoryDTO categoryDTO);
        Task UpdateCategoryAsync(CategoryDTO categoryDTO);
        Task RemoveCategoryAsync(int? id);

    }
}
