﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.CRM.IProspectRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.CRM
{
  public interface IProspectRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Prospect> GetAsync(string id);

    Task<Prospect> SingleOrDefaultAsync(Func<Prospect, bool> predicate);

    Task<bool> AddAsync(Prospect entity);

    Task<bool> AddRangeAsync(IEnumerable<Prospect> entities);

    Task<IEnumerable<Prospect>> GetAllAsync();

    Task<IEnumerable<Prospect>> FindAsync(Func<Prospect, bool> predicate);

    Task<Prospect> GetUsingEmailAsync(string email);

    Task GetForClientTypeAsync(ClientType clientType);

    Task GetForClientTypeAsync(IEnumerable<ClientType> clientTypes);

    Task GetForCompanySizeAsync(CompanySize companySize);

    Task GetForCompanySizeAsync(IEnumerable<CompanySize> companySizes);

    Task GetForCountryAsync(Country country);

    Task GetForCountryAsync(IEnumerable<Country> countries);

    Task GetForJobLevelAsync(JobLevel jobLevel);

    Task GetForJobLevelAsync(IEnumerable<JobLevel> jobLevels);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Prospect entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Prospect> entities);
  }
}
