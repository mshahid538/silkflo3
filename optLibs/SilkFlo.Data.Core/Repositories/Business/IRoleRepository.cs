// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IRoleRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IRoleRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Role> GetAsync(string id);

    Task<Role> SingleOrDefaultAsync(Func<Role, bool> predicate);

    Task<bool> AddAsync(Role entity);

    Task<bool> AddRangeAsync(IEnumerable<Role> entities);

    Task<IEnumerable<Role>> GetAllAsync();

    Task<IEnumerable<Role>> FindAsync(Func<Role, bool> predicate);

    Task<Role> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetRoleForAsync(CollaboratorRole collaboratorRole);

    Task GetRoleForAsync(IEnumerable<CollaboratorRole> collaboratorRoles);

    Task GetRoleForAsync(ImplementationCost implementationCost);

    Task GetRoleForAsync(
      IEnumerable<ImplementationCost> implementationCosts);

    Task GetRoleForAsync(RoleCost roleCost);

    Task GetRoleForAsync(IEnumerable<RoleCost> roleCosts);

    Task GetRoleForAsync(RoleIdeaAuthorisation roleIdeaAuthorisation);

    Task GetRoleForAsync(
      IEnumerable<RoleIdeaAuthorisation> roleIdeaAuthorisations);

    Task<Role> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Role entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Role> entities);
  }
}
