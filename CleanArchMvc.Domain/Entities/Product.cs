﻿using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
// Modelo de Dominio

namespace CleanArchMvc.Domain.Entitites
{
    public sealed class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Invalid id value");
            Id = id;
            ValidateDomain(name, description, price, stock, image);
        }

        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            CategoryId = categoryId;
        }

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name, Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Name too short, minimun 3 characters");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid description, Description is required");
            DomainExceptionValidation.When(description.Length < 5, "invalid description, too short, minimun 5 characteres");

            DomainExceptionValidation.When(price < 0, "invalid price value");
            DomainExceptionValidation.When(stock < 0, "invalid stock value");

            DomainExceptionValidation.When(image?.Length > 250, "invalid image name, too long, maximun 250 characteres");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;

        }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
