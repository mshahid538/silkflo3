// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IIdeaOtherRunningCostRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
    public interface IIdeaOtherRunningCostRepository
    {
        bool IncludeDeleted { get; set; }

        Task<IdeaOtherRunningCost> GetAsync(string id);

        Task<IdeaOtherRunningCost> SingleOrDefaultAsync(Func<IdeaOtherRunningCost, bool> predicate);

        Task<bool> AddAsync(IdeaOtherRunningCost entity);

        Task<bool> AddRangeAsync(IEnumerable<IdeaOtherRunningCost> entities);

        Task<IEnumerable<IdeaOtherRunningCost>> GetAllAsync();

        Task<IEnumerable<IdeaOtherRunningCost>> FindAsync(Func<IdeaOtherRunningCost, bool> predicate);

        Task GetForClientAsync(Client client);

        Task GetForClientAsync(IEnumerable<Client> clients);

        Task GetForIdeaAsync(Idea idea);

        Task GetForIdeaAsync(IEnumerable<Idea> ideas);

        Task GetForOtherRunningCostAsync(OtherRunningCost otherRunningCost);

        Task GetForOtherRunningCostAsync(IEnumerable<OtherRunningCost> otherRunningCosts);

        Task<DataStoreResult> RemoveAsync(string id);

        Task<DataStoreResult> RemoveAsync(IdeaOtherRunningCost entity);

        Task<DataStoreResult> RemoveRangeAsync(IEnumerable<IdeaOtherRunningCost> entities);
    }
}