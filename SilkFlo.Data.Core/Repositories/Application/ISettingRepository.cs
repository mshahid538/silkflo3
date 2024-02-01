// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Application.ISettingRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Application
{
  public interface ISettingRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Setting> GetAsync(string id);

    Task<Setting> SingleOrDefaultAsync(Func<Setting, bool> predicate);

    Task<bool> AddAsync(Setting entity);

    Task<bool> AddRangeAsync(IEnumerable<Setting> entities);

    Task<IEnumerable<Setting>> GetAllAsync();

    Task<IEnumerable<Setting>> FindAsync(Func<Setting, bool> predicate);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Setting entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Setting> entities);
  }
}
