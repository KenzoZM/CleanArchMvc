using CleanArchMvc.Domain.Entitites;
using FluentAssertions;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = "Create Product With Valid State")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "Product Image");
            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "Product Image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().
                WithMessage("Invalid Id value");
        }

        [Fact]
        public void CreateProduct_ShortNameValue__DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "Product Image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().
                WithMessage("Name too short, minimun 3 characters");
        }

        [Fact]
        public void CreateProduct_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Product(1, "", "Product Description", 9.99m, 99, "Product Image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().
                WithMessage("Invalid name, Name is required");
        }

        [Fact]
        public void CreateProduct_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Product(1, null, "Product Description", 9.99m, 99, "Product Image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_ShortDescriptionValue_DomainExceptionRequiredDescription()
        {
            Action action = () => new Product(1, "Product Name", "Des", 9.99m, 99, "Product Image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().
                WithMessage("invalid discription, too short, minimun 5 characteres");
        }

        [Fact]
        public void CreateProduct_MissingDescriptionValue_DomainExceptionRequiredDescription()
        {
            Action action = () => new Product(1, "Product Name", "", 9.99m, 99, "Product Image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().
                WithMessage("Invalid discription, Discription is required");
        }

        [Fact]
        public void CreateProduct_WithNullDescriptionValue_DomainExceptionRequiredDescription()
        {
            Action action = () => new Product(1, "Product Name", null, 9.99m, 99, "Product Image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99,
                "Product Image  toooooooooooooooooooooooooooooooooooooooooooooooo looooooooooooooooooooooooooooooooooooooooooonnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnngggggggggggggggg toooooooooooooooooooooooooooooooooooooooooooooooo looooooooooooooooooooooooooooooooooooooooooonnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnngggggggggggggggg");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().
                WithMessage("invalid image name, too long, maximun 250 characteres");
        }

        [Fact]
        public void CreateProduct_WithNullImageValue_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_WithEmptyImageValue_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, value, "Product Image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().
                WithMessage("invalid stock value");
        }

        [Theory]
        [InlineData(-1)]
        public void CreateProduct_InvalidPriceValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", value, 99, "Product Image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().
                WithMessage("invalid price value");
        }


    }
}
