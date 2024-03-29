﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Shared.IClientTypeRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Shared
{
  public interface IClientTypeRepository
  {
    bool IncludeDeleted { get; set; }

    Task<ClientType> GetAsync(string id);

    Task<ClientType> SingleOrDefaultAsync(Func<ClientType, bool> predicate);

    Task<bool> AddAsync(ClientType entity);

    Task<bool> AddRangeAsync(IEnumerable<ClientType> entities);

    Task<IEnumerable<ClientType>> GetAllAsync();

    Task<IEnumerable<ClientType>> FindAsync(Func<ClientType, bool> predicate);

    Task<ClientType> GetUsingNameAsync(string name);

    Task GetTypeForAsync(Client client);

    Task GetTypeForAsync(IEnumerable<Client> clients);

    Task GetClientTypeForAsync(Prospect prospect);

    Task GetClientTypeForAsync(IEnumerable<Prospect> prospects);

    Task GetAgencyTypeForAsync(Subscription subscription);

    Task GetAgencyTypeForAsync(IEnumerable<Subscription> subscriptions);

    Task<ClientType> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(ClientType entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<ClientType> entities);
  }
}
