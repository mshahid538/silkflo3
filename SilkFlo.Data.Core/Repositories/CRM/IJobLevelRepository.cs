// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.CRM.IJobLevelRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.CRM;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.CRM
{
  public interface IJobLevelRepository
  {
    bool IncludeDeleted { get; set; }

    Task<JobLevel> GetAsync(string id);

    Task<JobLevel> SingleOrDefaultAsync(Func<JobLevel, bool> predicate);

    Task<bool> AddAsync(JobLevel entity);

    Task<bool> AddRangeAsync(IEnumerable<JobLevel> entities);

    Task<IEnumerable<JobLevel>> GetAllAsync();

    Task<IEnumerable<JobLevel>> FindAsync(Func<JobLevel, bool> predicate);

    Task<JobLevel> GetUsingNameAsync(string name);

    Task GetJobLevelForAsync(Prospect prospect);

    Task GetJobLevelForAsync(IEnumerable<Prospect> prospects);

    Task<JobLevel> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(JobLevel entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<JobLevel> entities);
  }
}
