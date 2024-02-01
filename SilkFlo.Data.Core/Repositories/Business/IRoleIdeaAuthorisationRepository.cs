// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IRoleIdeaAuthorisationRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IRoleIdeaAuthorisationRepository
  {
    bool IncludeDeleted { get; set; }

    Task<RoleIdeaAuthorisation> GetAsync(string id);

    Task<RoleIdeaAuthorisation> SingleOrDefaultAsync(Func<RoleIdeaAuthorisation, bool> predicate);

    Task<bool> AddAsync(RoleIdeaAuthorisation entity);

    Task<bool> AddRangeAsync(IEnumerable<RoleIdeaAuthorisation> entities);

    Task<IEnumerable<RoleIdeaAuthorisation>> GetAllAsync();

    Task<IEnumerable<RoleIdeaAuthorisation>> FindAsync(Func<RoleIdeaAuthorisation, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForIdeaAuthorisationAsync(IdeaAuthorisation ideaAuthorisation);

    Task GetForIdeaAuthorisationAsync(IEnumerable<IdeaAuthorisation> ideaAuthorisations);

    Task GetForRoleAsync(Role role);

    Task GetForRoleAsync(IEnumerable<Role> roles);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(RoleIdeaAuthorisation entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<RoleIdeaAuthorisation> entities);
  }
}
