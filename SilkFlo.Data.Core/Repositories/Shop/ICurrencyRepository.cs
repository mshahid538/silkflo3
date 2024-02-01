// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shop.ICurrencyRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shop
{
  public interface ICurrencyRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Currency> GetAsync(string id);

    Task<Currency> SingleOrDefaultAsync(Func<Currency, bool> predicate);

    Task<bool> AddAsync(Currency entity);

    Task<bool> AddRangeAsync(IEnumerable<Currency> entities);

    Task<IEnumerable<Currency>> GetAllAsync();

    Task<IEnumerable<Currency>> FindAsync(Func<Currency, bool> predicate);

    Task GetCurrencyForAsync(Client client);

    Task GetCurrencyForAsync(IEnumerable<Client> clients);

    Task GetCurrencyForAsync(Price price);

    Task GetCurrencyForAsync(IEnumerable<Price> prices);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Currency entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Currency> entities);
  }
}
