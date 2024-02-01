// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.IAnalyticRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories
{
  public interface IAnalyticRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Analytic> GetAsync(string id);

    Task<Analytic> SingleOrDefaultAsync(Func<Analytic, bool> predicate);

    Task<bool> AddAsync(Analytic entity);

    Task<bool> AddRangeAsync(IEnumerable<Analytic> entities);

    Task<IEnumerable<Analytic>> GetAllAsync();

    Task<IEnumerable<Analytic>> FindAsync(Func<Analytic, bool> predicate);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Analytic entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Analytic> entities);
  }
}
