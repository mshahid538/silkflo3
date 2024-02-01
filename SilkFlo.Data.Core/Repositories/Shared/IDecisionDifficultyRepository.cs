// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.IDecisionDifficultyRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IDecisionDifficultyRepository
  {
    bool IncludeDeleted { get; set; }

    Task<DecisionDifficulty> GetAsync(string id);

    Task<DecisionDifficulty> SingleOrDefaultAsync(Func<DecisionDifficulty, bool> predicate);

    Task<bool> AddAsync(DecisionDifficulty entity);

    Task<bool> AddRangeAsync(IEnumerable<DecisionDifficulty> entities);

    Task<IEnumerable<DecisionDifficulty>> GetAllAsync();

    Task<IEnumerable<DecisionDifficulty>> FindAsync(Func<DecisionDifficulty, bool> predicate);

    Task<DecisionDifficulty> GetUsingNameAsync(string name);

    Task GetDecisionDifficultyForAsync(Idea idea);

    Task GetDecisionDifficultyForAsync(IEnumerable<Idea> ideas);

    Task<DecisionDifficulty> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(DecisionDifficulty entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<DecisionDifficulty> entities);
  }
}
