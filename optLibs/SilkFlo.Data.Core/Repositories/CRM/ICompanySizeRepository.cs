// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.CRM.ICompanySizeRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.CRM;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.CRM
{
  public interface ICompanySizeRepository
  {
    bool IncludeDeleted { get; set; }

    Task<CompanySize> GetAsync(string id);

    Task<CompanySize> SingleOrDefaultAsync(Func<CompanySize, bool> predicate);

    Task<bool> AddAsync(CompanySize entity);

    Task<bool> AddRangeAsync(IEnumerable<CompanySize> entities);

    Task<IEnumerable<CompanySize>> GetAllAsync();

    Task<IEnumerable<CompanySize>> FindAsync(Func<CompanySize, bool> predicate);

    Task<CompanySize> GetUsingNameAsync(string name);

    Task GetCompanySizeForAsync(Prospect prospect);

    Task GetCompanySizeForAsync(IEnumerable<Prospect> prospects);

    Task<CompanySize> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(CompanySize entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<CompanySize> entities);
  }
}
