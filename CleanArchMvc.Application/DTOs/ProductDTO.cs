using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Entitites;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CleanArchMvc.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Name")]
        [RegularExpression(@"^[a-zA-Z0-9\s.]*$", ErrorMessage = "Special characters are not allowed in the name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description is Required")]
        [MinLength(5)]
        [MaxLength(200)]
        [DisplayName("Description")]
        [RegularExpression(@"^[a-zA-Z0-9\s.]*$", ErrorMessage = "Special characters are not allowed in the Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Price is Required")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Stock is Required")]
        [Range(1, 9999)]
        [DisplayName("Stock")]
        public int Stock { get; set; }

        [MaxLength(250)]
        [DisplayName("Product Image")]
        [RegularExpression(@"^[a-zA-Z0-9\s.]*$", ErrorMessage = "Special characters are not allowed in the name")]
        public string Image { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Category? Category { get; set; }

        [DisplayName("Categories")]
        public int? CategoryId { get; set; }
    }
}

