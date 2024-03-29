﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.IStageGroupRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IStageGroupRepository
  {
    bool IncludeDeleted { get; set; }

    Task<StageGroup> GetAsync(string id);

    Task<StageGroup> SingleOrDefaultAsync(Func<StageGroup, bool> predicate);

    Task<bool> AddAsync(StageGroup entity);

    Task<bool> AddRangeAsync(IEnumerable<StageGroup> entities);

    Task<IEnumerable<StageGroup>> GetAllAsync();

    Task<IEnumerable<StageGroup>> FindAsync(Func<StageGroup, bool> predicate);

    Task<StageGroup> GetUsingNameAsync(string name);

    Task GetStageGroupForAsync(Stage stage);

    Task GetStageGroupForAsync(IEnumerable<Stage> stages);

    Task<StageGroup> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(StageGroup entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<StageGroup> entities);
  }
}
