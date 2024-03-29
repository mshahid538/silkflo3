﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.IUserBadgeRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories
{
  public interface IUserBadgeRepository
  {
    bool IncludeDeleted { get; set; }

    Task<UserBadge> GetAsync(string id);

    Task<UserBadge> SingleOrDefaultAsync(Func<UserBadge, bool> predicate);

    Task<bool> AddAsync(UserBadge entity);

    Task<bool> AddRangeAsync(IEnumerable<UserBadge> entities);

    Task<IEnumerable<UserBadge>> GetAllAsync();

    Task<IEnumerable<UserBadge>> FindAsync(Func<UserBadge, bool> predicate);

    Task GetForBadgeAsync(Badge badge);

    Task GetForBadgeAsync(IEnumerable<Badge> badges);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(UserBadge entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<UserBadge> entities);
  }
}
