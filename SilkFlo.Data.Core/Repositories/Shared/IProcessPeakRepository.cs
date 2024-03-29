﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.IProcessPeakRepository
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
  public interface IProcessPeakRepository
  {
    bool IncludeDeleted { get; set; }

    Task<ProcessPeak> GetAsync(string id);

    Task<ProcessPeak> SingleOrDefaultAsync(Func<ProcessPeak, bool> predicate);

    Task<bool> AddAsync(ProcessPeak entity);

    Task<bool> AddRangeAsync(IEnumerable<ProcessPeak> entities);

    Task<IEnumerable<ProcessPeak>> GetAllAsync();

    Task<IEnumerable<ProcessPeak>> FindAsync(Func<ProcessPeak, bool> predicate);

    Task<ProcessPeak> GetUsingNameAsync(string name);

    Task GetProcessPeakForAsync(Idea idea);

    Task GetProcessPeakForAsync(IEnumerable<Idea> ideas);

    Task<ProcessPeak> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(ProcessPeak entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ProcessPeak> entities);
  }
}
