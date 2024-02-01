// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IDepartmentRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IDepartmentRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Department> GetAsync(string id);

    Task<Department> SingleOrDefaultAsync(Func<Department, bool> predicate);

    Task<bool> AddAsync(Department entity);

    Task<bool> AddRangeAsync(IEnumerable<Department> entities);

    Task<IEnumerable<Department>> GetAllAsync();

    Task<IEnumerable<Department>> FindAsync(Func<Department, bool> predicate);

    Task<Department> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetDepartmentForAsync(Idea idea);

    Task GetDepartmentForAsync(IEnumerable<Idea> ideas);

    Task GetDepartmentForAsync(Team team);

    Task GetDepartmentForAsync(IEnumerable<Team> teams);

    Task GetDepartmentForAsync(User user);

    Task GetDepartmentForAsync(IEnumerable<User> users);

    Task<Department> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Department entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Department> entities);
  }
}
