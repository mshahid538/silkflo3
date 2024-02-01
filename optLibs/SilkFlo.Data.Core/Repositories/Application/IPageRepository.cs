// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Application.IPageRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Application
{
  public interface IPageRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Page> GetAsync(string id);

    Task<Page> SingleOrDefaultAsync(Func<Page, bool> predicate);

    Task<bool> AddAsync(Page entity);

    Task<bool> AddRangeAsync(IEnumerable<Page> entities);

    Task<IEnumerable<Page>> GetAllAsync();

    Task<IEnumerable<Page>> FindAsync(Func<Page, bool> predicate);

    Task<Page> GetUsingNameAsync(string name);

    Task<Page> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Page entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Page> entities);
  }
}
