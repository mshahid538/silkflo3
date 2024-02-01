// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Application.IHotSpotRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Application
{
  public interface IHotSpotRepository
  {
    bool IncludeDeleted { get; set; }

    Task<HotSpot> GetAsync(string id);

    Task<HotSpot> SingleOrDefaultAsync(Func<HotSpot, bool> predicate);

    Task<bool> AddAsync(HotSpot entity);

    Task<bool> AddRangeAsync(IEnumerable<HotSpot> entities);

    Task<IEnumerable<HotSpot>> GetAllAsync();

    Task<IEnumerable<HotSpot>> FindAsync(Func<HotSpot, bool> predicate);

    Task<HotSpot> GetUsingNameAsync(string name);

    Task<HotSpot> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(HotSpot entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<HotSpot> entities);
  }
}
