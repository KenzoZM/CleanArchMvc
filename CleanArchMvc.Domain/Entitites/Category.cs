using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Modelo de Dominio

namespace CleanArchMvc.Domain.Entitites
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }
        public ICollection<Product> Products { get; set; }

        public Category(string name)
        {
            ValidateDomain(name);
        }

        public void Update(string name)
        {
            ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            ValidateDomain(name);
            DomainExceptionValidation.When(Id < 0, "Invalid Id value");
            Id = id;
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name or Name is required");
            DomainExceptionValidation.When(name.Length  < 3, "Name is too short, minimun 3 characters");
            Name = name;

        }
    }
}
