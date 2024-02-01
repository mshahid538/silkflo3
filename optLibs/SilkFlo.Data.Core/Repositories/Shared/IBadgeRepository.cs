// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.IBadgeRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IBadgeRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Badge> GetAsync(string id);

    Task<Badge> SingleOrDefaultAsync(Func<Badge, bool> predicate);

    Task<bool> AddAsync(Badge entity);

    Task<bool> AddRangeAsync(IEnumerable<Badge> entities);

    Task<IEnumerable<Badge>> GetAllAsync();

    Task<IEnumerable<Badge>> FindAsync(Func<Badge, bool> predicate);

    Task<Badge> GetUsingNameAsync(string name);

    Task GetBadgeForAsync(UserBadge userBadge);

    Task GetBadgeForAsync(IEnumerable<UserBadge> userBadges);

    Task<Badge> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Badge entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Badge> entities);
  }
}
