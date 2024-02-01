// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.IWebHookLogRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories
{
  public interface IWebHookLogRepository
  {
    bool IncludeDeleted { get; set; }

    Task<WebHookLog> GetAsync(string id);

    Task<WebHookLog> SingleOrDefaultAsync(Func<WebHookLog, bool> predicate);

    Task<bool> AddAsync(WebHookLog entity);

    Task<bool> AddRangeAsync(IEnumerable<WebHookLog> entities);

    Task<IEnumerable<WebHookLog>> GetAllAsync();

    Task<IEnumerable<WebHookLog>> FindAsync(Func<WebHookLog, bool> predicate);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(WebHookLog entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<WebHookLog> entities);
  }
}
