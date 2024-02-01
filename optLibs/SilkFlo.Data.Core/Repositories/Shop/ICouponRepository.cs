// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shop.ICouponRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shop
{
  public interface ICouponRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Coupon> GetAsync(string id);

    Task<Coupon> SingleOrDefaultAsync(Func<Coupon, bool> predicate);

    Task<bool> AddAsync(Coupon entity);

    Task<bool> AddRangeAsync(IEnumerable<Coupon> entities);

    Task<IEnumerable<Coupon>> GetAllAsync();

    Task<IEnumerable<Coupon>> FindAsync(Func<Coupon, bool> predicate);

    Task<Coupon> GetUsingNameAsync(string name);

    Task GetCouponForAsync(Subscription subscription);

    Task GetCouponForAsync(IEnumerable<Subscription> subscriptions);

    Task<Coupon> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Coupon entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Coupon> entities);
  }
}
