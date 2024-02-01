// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.IAchievementRepository
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
  public interface IAchievementRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Achievement> GetAsync(string id);

    Task<Achievement> SingleOrDefaultAsync(Func<Achievement, bool> predicate);

    Task<bool> AddAsync(Achievement entity);

    Task<bool> AddRangeAsync(IEnumerable<Achievement> entities);

    Task<IEnumerable<Achievement>> GetAllAsync();

    Task<IEnumerable<Achievement>> FindAsync(Func<Achievement, bool> predicate);

    Task<Achievement> GetUsingNameAsync(string name);

    Task GetAchievementForAsync(UserAchievement userAchievement);

    Task GetAchievementForAsync(IEnumerable<UserAchievement> userAchievements);

    Task<Achievement> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Achievement entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Achievement> entities);
  }
}
