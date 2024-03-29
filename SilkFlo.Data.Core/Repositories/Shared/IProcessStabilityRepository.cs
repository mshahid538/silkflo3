﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.IProcessStabilityRepository
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
  public interface IProcessStabilityRepository
  {
    bool IncludeDeleted { get; set; }

    Task<ProcessStability> GetAsync(string id);

    Task<ProcessStability> SingleOrDefaultAsync(Func<ProcessStability, bool> predicate);

    Task<bool> AddAsync(ProcessStability entity);

    Task<bool> AddRangeAsync(IEnumerable<ProcessStability> entities);

    Task<IEnumerable<ProcessStability>> GetAllAsync();

    Task<IEnumerable<ProcessStability>> FindAsync(Func<ProcessStability, bool> predicate);

    Task<ProcessStability> GetUsingNameAsync(string name);

    Task GetProcessStabilityForAsync(Idea idea);

    Task GetProcessStabilityForAsync(IEnumerable<Idea> ideas);

    Task<ProcessStability> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(ProcessStability entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ProcessStability> entities);
  }
}
