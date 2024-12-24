﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi501.Dtos
{
    public class ResultProductDto
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public string productCategory { get; set; }
        public int productStock { get; set; }
        public decimal productPrice { get; set; }
    }
}
