// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.ILogRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories
{
  public interface ILogRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Log> GetAsync(string id);

    Task<Log> SingleOrDefaultAsync(Func<Log, bool> predicate);

    Task<bool> AddAsync(Log entity);

    Task<bool> AddRangeAsync(IEnumerable<Log> entities);

    Task<IEnumerable<Log>> GetAllAsync();

    Task<IEnumerable<Log>> FindAsync(Func<Log, bool> predicate);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Log entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Log> entities);
  }
}
