﻿using LibraryWebApp.DataAccess.Data;
using LibraryWebApp.DataAccess.Repository.IRepository;
using LibraryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebApp.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var productFromDb = _db.Products.FirstOrDefault(u => u.Id == product.Id);//cauta product id
            if(productFromDb != null)
            {
                //câmpurile produsului cu valorile noi ce vor fi actualizate
                productFromDb.Title = product.Title;
                productFromDb.ISBN = product.ISBN;
                productFromDb.Price = product.Price;
                productFromDb.Price50 = product.Price50;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.Price100 = product.Price100;
                productFromDb.Price100 = product.Price100;
                productFromDb.CategoryId = product.CategoryId;
                productFromDb.Author = product.Author;
                productFromDb.ProductImages = product.ProductImages;
                //if(product.ImageUrl != null)
                //{
                //    productFromDb.ImageUrl = product.ImageUrl;
                //}
            }
        }
    }
}
