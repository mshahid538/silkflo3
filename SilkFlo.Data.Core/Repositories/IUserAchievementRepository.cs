// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.IUserAchievementRepository
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
  public interface IUserAchievementRepository
  {
    bool IncludeDeleted { get; set; }

    Task<UserAchievement> GetAsync(string id);

    Task<UserAchievement> SingleOrDefaultAsync(Func<UserAchievement, bool> predicate);

    Task<bool> AddAsync(UserAchievement entity);

    Task<bool> AddRangeAsync(IEnumerable<UserAchievement> entities);

    Task<IEnumerable<UserAchievement>> GetAllAsync();

    Task<IEnumerable<UserAchievement>> FindAsync(Func<UserAchievement, bool> predicate);

    Task GetForAchievementAsync(Achievement achievement);

    Task GetForAchievementAsync(IEnumerable<Achievement> achievements);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(UserAchievement entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<UserAchievement> entities);
  }
}
