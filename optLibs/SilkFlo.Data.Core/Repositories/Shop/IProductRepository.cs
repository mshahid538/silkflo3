// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shop.IProductRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shop
{
  public interface IProductRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Product> GetAsync(string id);

    Task<Product> SingleOrDefaultAsync(Func<Product, bool> predicate);

    Task<bool> AddAsync(Product entity);

    Task<bool> AddRangeAsync(IEnumerable<Product> entities);

    Task<IEnumerable<Product>> GetAllAsync();

    Task<IEnumerable<Product>> FindAsync(Func<Product, bool> predicate);

    Task<Product> GetUsingNameAsync(string name);

    Task GetProductForAsync(Price price);

    Task GetProductForAsync(IEnumerable<Price> prices);

    Task<Product> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Product entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Product> entities);
  }
}
