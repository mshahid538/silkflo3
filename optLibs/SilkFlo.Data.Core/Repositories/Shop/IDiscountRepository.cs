// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shop.IDiscountRepository
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
  public interface IDiscountRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Discount> GetAsync(string id);

    Task<Discount> SingleOrDefaultAsync(Func<Discount, bool> predicate);

    Task<bool> AddAsync(Discount entity);

    Task<bool> AddRangeAsync(IEnumerable<Discount> entities);

    Task<IEnumerable<Discount>> GetAllAsync();

    Task<IEnumerable<Discount>> FindAsync(Func<Discount, bool> predicate);

    Task<Discount> GetUsingNameAsync(string name);

    Task GetAgencyDiscountForAsync(Client client);

    Task GetAgencyDiscountForAsync(IEnumerable<Client> clients);

    Task GetAgencyDiscountForAsync(Subscription subscription);

    Task GetAgencyDiscountForAsync(IEnumerable<Subscription> subscriptions);

    Task<Discount> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Discount entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Discount> entities);
  }
}
