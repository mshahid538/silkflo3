// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IVersionRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IVersionRepository
  {
    bool IncludeDeleted { get; set; }

    Task<SilkFlo.Data.Core.Domain.Business.Version> GetAsync(string id);

    Task<SilkFlo.Data.Core.Domain.Business.Version> SingleOrDefaultAsync(
      Func<SilkFlo.Data.Core.Domain.Business.Version, bool> predicate);

    Task<bool> AddAsync(SilkFlo.Data.Core.Domain.Business.Version entity);

    Task<bool> AddRangeAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> entities);

    Task<IEnumerable<SilkFlo.Data.Core.Domain.Business.Version>> GetAllAsync();

    Task<IEnumerable<SilkFlo.Data.Core.Domain.Business.Version>> FindAsync(
      Func<SilkFlo.Data.Core.Domain.Business.Version, bool> predicate);

    Task<SilkFlo.Data.Core.Domain.Business.Version> GetUsingNameAsync(string name);

    Task GetForApplicationAsync(Domain.Business.Application application);

    Task GetForApplicationAsync(IEnumerable<Domain.Business.Application> applications);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetVersionForAsync(IdeaApplicationVersion ideaApplicationVersion);

    Task GetVersionForAsync(
      IEnumerable<IdeaApplicationVersion> ideaApplicationVersions);

    Task<SilkFlo.Data.Core.Domain.Business.Version> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(SilkFlo.Data.Core.Domain.Business.Version entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> entities);
  }
}
