﻿using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Models;
using ShoppingCart.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ShoppingCartDbContext context): base(context) { }
      
    }
}
