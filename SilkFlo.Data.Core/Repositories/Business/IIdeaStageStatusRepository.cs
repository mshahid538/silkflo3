﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IIdeaStageStatusRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IIdeaStageStatusRepository
  {
    bool IncludeDeleted { get; set; }

    Task<IdeaStageStatus> GetAsync(string id);

    Task<IdeaStageStatus> SingleOrDefaultAsync(Func<IdeaStageStatus, bool> predicate);

    Task<bool> AddAsync(IdeaStageStatus entity);

    Task<bool> AddRangeAsync(IEnumerable<IdeaStageStatus> entities);

    Task<IEnumerable<IdeaStageStatus>> GetAllAsync();

    Task<IEnumerable<IdeaStageStatus>> FindAsync(Func<IdeaStageStatus, bool> predicate);

    Task GetForIdeaStageAsync(IdeaStage ideaStage);

    Task GetForIdeaStageAsync(IEnumerable<IdeaStage> ideaStages);

    Task GetForStatusAsync(IdeaStatus status);

    Task GetForStatusAsync(IEnumerable<IdeaStatus> statuses);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(IdeaStageStatus entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaStageStatus> entities);
  }
}
