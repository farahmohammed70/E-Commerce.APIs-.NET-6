﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public ProductBrand productBrand { get; set; } //Navigational Property
        public int ProductBrandId { get; set; } //Foreign Key
        public ProductType productType { get; set; } //Navigational Property
        public int ProductTypeId { get; set; } //Foreign Key
    }
}
