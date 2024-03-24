using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Name")]
        [RegularExpression(@"^[a-zA-Z0-9-\s]*$", ErrorMessage = "Special characters are not allowed in the name")]
        public string Name { get; set; }
    }
}
