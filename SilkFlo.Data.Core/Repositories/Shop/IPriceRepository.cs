// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shop.IPriceRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shop
{
  public interface IPriceRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Price> GetAsync(string id);

    Task<Price> SingleOrDefaultAsync(Func<Price, bool> predicate);

    Task<bool> AddAsync(Price entity);

    Task<bool> AddRangeAsync(IEnumerable<Price> entities);

    Task<IEnumerable<Price>> GetAllAsync();

    Task<IEnumerable<Price>> FindAsync(Func<Price, bool> predicate);

    Task GetForCurrencyAsync(Currency currency);

    Task GetForCurrencyAsync(IEnumerable<Currency> currencies);

    Task GetForPeriodAsync(Period period);

    Task GetForPeriodAsync(IEnumerable<Period> periods);

    Task GetForProductAsync(Product product);

    Task GetForProductAsync(IEnumerable<Product> products);

    Task GetPriceForAsync(Subscription subscription);

    Task GetPriceForAsync(IEnumerable<Subscription> subscriptions);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Price entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Price> entities);
  }
}
