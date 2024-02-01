// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.IRoleRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


#nullable enable
namespace SilkFlo.Data.Core.Repositories
{
  public interface IRoleRepository
  {
    bool IncludeDeleted { get; set; }


    #nullable disable
    Task<Role> GetAsync(string id);

    Task<Role> SingleOrDefaultAsync(Func<Role, bool> predicate);

    Task<bool> AddAsync(Role entity);

    Task<bool> AddRangeAsync(IEnumerable<Role> entities);

    Task<IEnumerable<Role>> GetAllAsync();

    Task<IEnumerable<Role>> FindAsync(Func<Role, bool> predicate);

    Task<Role> GetUsingNameAsync(string name);

    Task GetRoleForAsync(UserRole userRole);

    Task GetRoleForAsync(IEnumerable<UserRole> userRoles);

    Task<Role> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Role entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Role> entities);

    Task<bool> IsMemberAsync(
    #nullable enable
    string? userId, 
    #nullable disable
    string roleName);

    Task<bool> IsMemberAsync(User user, string roleName);
  }
}
