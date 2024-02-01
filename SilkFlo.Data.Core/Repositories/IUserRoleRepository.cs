// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.IUserRoleRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories
{
  public interface IUserRoleRepository
  {
    bool IncludeDeleted { get; set; }

    Task<UserRole> GetAsync(string id);

    Task<UserRole> SingleOrDefaultAsync(Func<UserRole, bool> predicate);

    Task<bool> AddAsync(UserRole entity);

    Task<bool> AddRangeAsync(IEnumerable<UserRole> entities);

    Task<IEnumerable<UserRole>> GetAllAsync();

    Task<IEnumerable<UserRole>> FindAsync(Func<UserRole, bool> predicate);

    Task GetForRoleAsync(Role role);

    Task GetForRoleAsync(IEnumerable<Role> roles);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(UserRole entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<UserRole> entities);
  }
}
