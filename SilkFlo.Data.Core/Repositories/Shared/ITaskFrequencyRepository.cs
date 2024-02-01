// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.ITaskFrequencyRepository
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
  public interface ITaskFrequencyRepository
  {
    bool IncludeDeleted { get; set; }

    Task<TaskFrequency> GetAsync(string id);

    Task<TaskFrequency> SingleOrDefaultAsync(Func<TaskFrequency, bool> predicate);

    Task<bool> AddAsync(TaskFrequency entity);

    Task<bool> AddRangeAsync(IEnumerable<TaskFrequency> entities);

    Task<IEnumerable<TaskFrequency>> GetAllAsync();

    Task<IEnumerable<TaskFrequency>> FindAsync(Func<TaskFrequency, bool> predicate);

    Task<TaskFrequency> GetUsingNameAsync(string name);

    Task GetTaskFrequencyForAsync(Idea idea);

    Task GetTaskFrequencyForAsync(IEnumerable<Idea> ideas);

    Task<TaskFrequency> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(TaskFrequency entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<TaskFrequency> entities);
  }
}
