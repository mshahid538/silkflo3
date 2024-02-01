// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IOtherRunningCostRepository
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
  public interface IOtherRunningCostRepository
  {
    bool IncludeDeleted { get; set; }

    Task<OtherRunningCost> GetAsync(string id);

    Task<OtherRunningCost> SingleOrDefaultAsync(Func<OtherRunningCost, bool> predicate);

    Task<bool> AddAsync(OtherRunningCost entity);

    Task<bool> AddRangeAsync(IEnumerable<OtherRunningCost> entities);

    Task<IEnumerable<OtherRunningCost>> GetAllAsync();

    Task<IEnumerable<OtherRunningCost>> FindAsync(Func<OtherRunningCost, bool> predicate);

    Task<OtherRunningCost> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForCostTypeAsync(CostType costType);

    Task GetForCostTypeAsync(IEnumerable<CostType> costTypes);

    Task GetForFrequencyAsync(Period frequency);

    Task GetForFrequencyAsync(IEnumerable<Period> frequencies);

    Task GetOtherRunningCostForAsync(IdeaOtherRunningCost ideaOtherRunningCost);

    Task GetOtherRunningCostForAsync(
      IEnumerable<IdeaOtherRunningCost> ideaOtherRunningCosts);

    Task<OtherRunningCost> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(OtherRunningCost entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<OtherRunningCost> entities);
  }
}
