using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data.Memory
{
    /// <summary>Provides an in-memory product database.</summary>
    public class MemoryProductDatabase
    {
        public MemoryProductDatabase()
        {
            _products = new Product[25];

            var product = new Product();
            product.Name = "Iphone x";
            product.IsDiscontinued = true;
            product.Price = 1500;
            _products[0] = product;

            product = new Product();
            product.Name = "Windows phone";
            product.IsDiscontinued = true;
            product.Price = 15;
            _products[1] = product;

            product = new Product();
            product.Name = "Samsung s8";
            product.IsDiscontinued = false;
            product.Price = 1800;
            _products[2] = product;
        }
        public Product Add ( Product product, out string message)
        {
            //Check for null
            if (product == null)
            {
                message = "Product cannot be null.";
                return null;
            }

            //Validate Product
            var error = product.Validate();
            if (!String.IsNullOrEmpty(error))
            {
                message = error;
                return null;
            }

            //TODO; Verify unique product

            //ADD
            var index = FindEmptyProductIndex();
            if (index < 0)
            {
                message = "out of memory";
                return null;
            }
            _products[index] = product;
            message = null;
            return product;
        }

        public Product Edit ( Product product, out string message )
        {
            //Check for null
            if (product == null)
            {
                message = "Product cannot be null.";
                return null;
            }

            //Validate Product
            var error = product.Validate();
            if (!String.IsNullOrEmpty(error))
            {
                message = error;
                return null;
            }

            //TODO; Verify unique product except current product

            //find existing
            var existingIndex = GetById(product.Id);
            if (existingIndex < 0)
            {
                message = "Product not found";
                return null;
            }
            _products[existingIndex] = product;
            message = null;
            return product;
        }
        public void Remove ( int id)
        {
            if(id > 0)
            {
                var index = GetById(id);
                if (index >= 0)
                    _products[index] = null;
            };
        }

        public Product[] GetAll ()
        {
            return _products;
        }

        private int FindEmptyProductIndex()
        {
            //Find empty array element
            for (var index = 0; index < _products.Length; ++index)
            {
                if (_products[index] == null)
                {
                    return index;
                }
            }

            return -1;
        }
        private int GetById (int id)
        {
            for (var index = 0; index < _products.Length; ++index)
            {
                if (_products[index]?.Id == id)
                {
                    return index;
                }
            };
            return -1;
        }

        private Product[] _products;
    }
}
