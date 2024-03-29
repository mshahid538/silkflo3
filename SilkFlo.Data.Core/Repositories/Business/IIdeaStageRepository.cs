﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IIdeaStageRepository
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
  public interface IIdeaStageRepository
  {
    bool IncludeDeleted { get; set; }

    Task<IdeaStage> GetAsync(string id);

    Task<IdeaStage> SingleOrDefaultAsync(Func<IdeaStage, bool> predicate);

    Task<bool> AddAsync(IdeaStage entity);

    Task<bool> AddRangeAsync(IEnumerable<IdeaStage> entities);

    Task<IEnumerable<IdeaStage>> GetAllAsync();

    Task<IEnumerable<IdeaStage>> FindAsync(Func<IdeaStage, bool> predicate);

    Task GetForIdeaAsync(Idea idea);

    Task GetForIdeaAsync(IEnumerable<Idea> ideas);

    Task GetForStageAsync(Stage stage);

    Task GetForStageAsync(IEnumerable<Stage> stages);

    Task GetIdeaStageForAsync(IdeaStageStatus ideaStageStatus);

    Task GetIdeaStageForAsync(IEnumerable<IdeaStageStatus> ideaStageStatuses);

    Task GetIdeaStageForAsync(ImplementationCost implementationCost);

    Task GetIdeaStageForAsync(
      IEnumerable<ImplementationCost> implementationCosts);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(IdeaStage entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaStage> entities);
  }
}
