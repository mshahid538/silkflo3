// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.IInputRepository
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
  public interface IInputRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Input> GetAsync(string id);

    Task<Input> SingleOrDefaultAsync(Func<Input, bool> predicate);

    Task<bool> AddAsync(Input entity);

    Task<bool> AddRangeAsync(IEnumerable<Input> entities);

    Task<IEnumerable<Input>> GetAllAsync();

    Task<IEnumerable<Input>> FindAsync(Func<Input, bool> predicate);

    Task<Input> GetUsingNameAsync(string name);

    Task GetInputForAsync(Idea idea);

    Task GetInputForAsync(IEnumerable<Idea> ideas);

    Task<Input> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Input entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Input> entities);
  }
}
