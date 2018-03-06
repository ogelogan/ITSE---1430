﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            //array version
            //var prods = new product[]
            //var prods = new[]
            //{
            //    new Product(),
            //    new Product()
            //};

            //_products = new Product[25];
            _products = new List<Product>()
            {

                new Product() { Id = _nextId++, Name = "Iphone x",
                                IsDiscontinued = true, Price = 1500, },
                new Product() { Id = _nextId++, Name = "Windows phone",
                                IsDiscontinued = true, Price = 15, },
                new Product() { Id = _nextId++, Name = "Samsung s8",
                                IsDiscontinued = false, Price = 1800, }
            };
        

           //var product = new Product() {
           //    Id = _nextId++,
           //    Name = "Iphone x",
           //    IsDiscontinued = true,
           //    Price = 1500,
            //};
            //_products.Add(product);

            //product = new Product() {
            //    Id = _nextId++,
            //    Name = "Windows phone",
            //    IsDiscontinued = true,
            //    Price = 15,
            //};
            //_products.Add(product);

            //product = new Product() {
            //    Id = _nextId++,
            //    Name = "Samsung s8",
            //    IsDiscontinued = false,
            //    Price = 1800,
            //};
            //_products.Add(product);
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
            //var error = product.Validate();
            var errors = ObjectValidator.Validate(product);
            if (errors.Count() > 0)
            {
                message = errors.ElementAt(0).ErrorMessage;
                return null;
            }

            //TODO; Verify unique product

            //ADD
           // var index = FindEmptyProductIndex();
           // if (index < 0)
           // {
           //     message = "out of memory";
           //     return null;
           // }

            //Clone a object
            product.Id = _nextId++;
            _products.Add(Clone(product));
            message = null;

            //return a copy
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
            var existing = GetById(product.Id);
            if (existing == null)
            {
                message = "Product not found";
                return null;
            }
            //_products[existingIndex] = Clone(product);
            Copy(existing, product);
            message = null;
            return product;
        }
        public void Remove ( int id)
        {
            if(id > 0)
            {
                var existing = GetById(id);
                if (existing != null)
                    _products.Remove(existing);
            };
        }

        public Product[] GetAll ()
        {
            var items = new List<Product>();
            //for (var index = 0; index < _products.Length; ++index)
            foreach (var product in _products)
            {
                if (product != null)
                    items.Add(Clone(product));
            }
            return items.ToArray();
        }

        //private int FindEmptyProductIndex()
        //{
        //    //Find empty array element
        //    for (var index = 0; index < _products.Length; ++index)
        //    {
        //        if (_products[index] == null)
        //        {
        //            return index;
        //        }
        //    }
        //
        //    return -1;
        //}

        private Product Clone ( Product item)
        {
            var newProduct = new Product();
            Copy(newProduct, item);
            
            return newProduct;
        }

        private void Copy (Product target, Product source)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.IsDiscontinued = source.IsDiscontinued;
        }

        private Product GetById (int id)
        {
           foreach (var product in _products)
            {
                if (product.Id == id)
                {
                    return product;
                }
            };
            return null;
        }

        private readonly List<Product> _products = new List<Product>();
        private int _nextId = 1;
    }
}
