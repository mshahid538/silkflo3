// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Agency.IManageTenantRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Agency
{
  public interface IManageTenantRepository
  {
    bool IncludeDeleted { get; set; }

    Task<ManageTenant> GetAsync(string id);

    Task<ManageTenant> SingleOrDefaultAsync(Func<ManageTenant, bool> predicate);

    Task<bool> AddAsync(ManageTenant entity);

    Task<bool> AddRangeAsync(IEnumerable<ManageTenant> entities);

    Task<IEnumerable<ManageTenant>> GetAllAsync();

    Task<IEnumerable<ManageTenant>> FindAsync(Func<ManageTenant, bool> predicate);

    Task GetForTenantAsync(Client tenant);

    Task GetForTenantAsync(IEnumerable<Client> tenants);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(ManageTenant entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ManageTenant> entities);
  }
}
