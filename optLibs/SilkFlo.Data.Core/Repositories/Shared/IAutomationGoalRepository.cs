// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.IAutomationGoalRepository
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
  public interface IAutomationGoalRepository
  {
    bool IncludeDeleted { get; set; }

    Task<AutomationGoal> GetAsync(string id);

    Task<AutomationGoal> SingleOrDefaultAsync(Func<AutomationGoal, bool> predicate);

    Task<bool> AddAsync(AutomationGoal entity);

    Task<bool> AddRangeAsync(IEnumerable<AutomationGoal> entities);

    Task<IEnumerable<AutomationGoal>> GetAllAsync();

    Task<IEnumerable<AutomationGoal>> FindAsync(Func<AutomationGoal, bool> predicate);

    Task<AutomationGoal> GetUsingNameAsync(string name);

    Task GetAutomationGoalForAsync(Idea idea);

    Task GetAutomationGoalForAsync(IEnumerable<Idea> ideas);

    Task<AutomationGoal> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(AutomationGoal entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<AutomationGoal> entities);
  }
}
