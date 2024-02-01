// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shop.ISubscriptionRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shop
{
  public interface ISubscriptionRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Subscription> GetAsync(string id);

    Task<Subscription> SingleOrDefaultAsync(Func<Subscription, bool> predicate);

    Task<bool> AddAsync(Subscription entity);

    Task<bool> AddRangeAsync(IEnumerable<Subscription> entities);

    Task<IEnumerable<Subscription>> GetAllAsync();

    Task<IEnumerable<Subscription>> FindAsync(Func<Subscription, bool> predicate);

    Task GetForAgencyDiscountAsync(Discount agencyDiscount);

    Task GetForAgencyDiscountAsync(IEnumerable<Discount> agencyDiscounts);

    Task GetForAgencyAsync(Client agency);

    Task GetForAgencyAsync(IEnumerable<Client> agencies);

    Task GetForAgencyTypeAsync(ClientType agencyType);

    Task GetForAgencyTypeAsync(IEnumerable<ClientType> agencyTypes);

    Task GetForCouponAsync(Coupon coupon);

    Task GetForCouponAsync(IEnumerable<Coupon> coupons);

    Task GetForPriceAsync(Price price);

    Task GetForPriceAsync(IEnumerable<Price> prices);

    Task GetForTenantAsync(Client tenant);

    Task GetForTenantAsync(IEnumerable<Client> tenants);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Subscription entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Subscription> entities);
  }
}
