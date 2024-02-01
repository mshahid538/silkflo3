// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IImplementationCostRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IImplementationCostRepository
  {
    bool IncludeDeleted { get; set; }

    Task<ImplementationCost> GetAsync(string id);

    Task<ImplementationCost> SingleOrDefaultAsync(Func<ImplementationCost, bool> predicate);

    Task<bool> AddAsync(ImplementationCost entity);

    Task<bool> AddRangeAsync(IEnumerable<ImplementationCost> entities);

    Task<IEnumerable<ImplementationCost>> GetAllAsync();

    Task<IEnumerable<ImplementationCost>> FindAsync(Func<ImplementationCost, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForIdeaStageAsync(IdeaStage ideaStage);

    Task GetForIdeaStageAsync(IEnumerable<IdeaStage> ideaStages);

    Task GetForRoleAsync(Role role);

    Task GetForRoleAsync(IEnumerable<Role> roles);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(ImplementationCost entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ImplementationCost> entities);
  }
}
