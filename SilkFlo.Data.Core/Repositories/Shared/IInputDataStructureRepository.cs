// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.IInputDataStructureRepository
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
  public interface IInputDataStructureRepository
  {
    bool IncludeDeleted { get; set; }

    Task<InputDataStructure> GetAsync(string id);

    Task<InputDataStructure> SingleOrDefaultAsync(Func<InputDataStructure, bool> predicate);

    Task<bool> AddAsync(InputDataStructure entity);

    Task<bool> AddRangeAsync(IEnumerable<InputDataStructure> entities);

    Task<IEnumerable<InputDataStructure>> GetAllAsync();

    Task<IEnumerable<InputDataStructure>> FindAsync(Func<InputDataStructure, bool> predicate);

    Task<InputDataStructure> GetUsingNameAsync(string name);

    Task GetInputDataStructureForAsync(Idea idea);

    Task GetInputDataStructureForAsync(IEnumerable<Idea> ideas);

    Task<InputDataStructure> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(InputDataStructure entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<InputDataStructure> entities);
  }
}
