// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.ITeamRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface ITeamRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Team> GetAsync(string id);

    Task<Team> SingleOrDefaultAsync(Func<Team, bool> predicate);

    Task<bool> AddAsync(Team entity);

    Task<bool> AddRangeAsync(IEnumerable<Team> entities);

    Task<IEnumerable<Team>> GetAllAsync();

    Task<IEnumerable<Team>> FindAsync(Func<Team, bool> predicate);

    Task<Team> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForDepartmentAsync(Department department);

    Task GetForDepartmentAsync(IEnumerable<Department> departments);

    Task GetTeamForAsync(Idea idea);

    Task GetTeamForAsync(IEnumerable<Idea> ideas);

    Task GetTeamForAsync(Process process);

    Task GetTeamForAsync(IEnumerable<Process> processes);

    Task<Team> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Team entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Team> entities);
  }
}
