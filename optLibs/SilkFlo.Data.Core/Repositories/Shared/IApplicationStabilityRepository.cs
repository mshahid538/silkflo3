// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.IApplicationStabilityRepository
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
  public interface IApplicationStabilityRepository
  {
    bool IncludeDeleted { get; set; }

    Task<ApplicationStability> GetAsync(string id);

    Task<ApplicationStability> SingleOrDefaultAsync(Func<ApplicationStability, bool> predicate);

    Task<bool> AddAsync(ApplicationStability entity);

    Task<bool> AddRangeAsync(IEnumerable<ApplicationStability> entities);

    Task<IEnumerable<ApplicationStability>> GetAllAsync();

    Task<IEnumerable<ApplicationStability>> FindAsync(Func<ApplicationStability, bool> predicate);

    Task<ApplicationStability> GetUsingNameAsync(string name);

    Task GetApplicationStabilityForAsync(Idea idea);

    Task GetApplicationStabilityForAsync(IEnumerable<Idea> ideas);

    Task<ApplicationStability> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(ApplicationStability entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ApplicationStability> entities);
  }
}
