﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.RoleRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Repositories.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class RoleRepository : IRoleRepository
  {
    private readonly 
    
    UnitOfWork _unitOfWork;

    public RoleRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public BusinessRole Get(string id) => this.GetAsync(id).Result;

    public async Task<BusinessRole> GetAsync(string id)
    {
      if (id == null)
        return (BusinessRole) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRoles.SingleOrDefault<BusinessRole>((Func<BusinessRole, bool>) (x => x.Id == id));
    }

    public BusinessRole SingleOrDefault(Func<BusinessRole, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<BusinessRole> SingleOrDefaultAsync(Func<BusinessRole, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRoles.Where<BusinessRole>(predicate).FirstOrDefault<BusinessRole>();
    }

    public bool Add(BusinessRole entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(BusinessRole entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<BusinessRole> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<BusinessRole> entities)
    {
      if (entities == null)
        return false;
      foreach (BusinessRole entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<BusinessRole> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<BusinessRole>> GetAllAsync()
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<BusinessRole>) dataSetAsync.BusinessRoles.OrderBy<BusinessRole, int>((Func<BusinessRole, int>) (m => m.Sort)).ThenBy<BusinessRole, string>((Func<BusinessRole, string>) (m => m.Name));
    }

    public IEnumerable<BusinessRole> Find(Func<BusinessRole, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<BusinessRole>> FindAsync(Func<BusinessRole, bool> predicate)
    {
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return (IEnumerable<BusinessRole>) dataSetAsync.BusinessRoles.Where<BusinessRole>(predicate).OrderBy<BusinessRole, int>((Func<BusinessRole, int>) (m => m.Sort)).ThenBy<BusinessRole, string>((Func<BusinessRole, string>) (m => m.Name));
    }

    public BusinessRole GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<BusinessRole> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (BusinessRole) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRoles.SingleOrDefault<BusinessRole>((Func<BusinessRole, bool>) (x => x.Name == name));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<BusinessRole> lst;
      if (client == null)
        lst = (List<BusinessRole>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<BusinessRole>) null;
      }
      else
      {
        var dataSet = await _unitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessRoles.Where<BusinessRole>((Func<BusinessRole, bool>) (x => x.ClientId == client.Id)).OrderBy<BusinessRole, int>((Func<BusinessRole, int>) (x => x.Sort)).ThenBy<BusinessRole, string>((Func<BusinessRole, string>) (x => x.Name)).ToList<BusinessRole>();
        //dataSet = (DataSet) null;
        foreach (BusinessRole item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Roles = lst;
        lst = (List<BusinessRole>) null;
      }
    }

    public void GetForClient(IEnumerable<Client> clients) => this.GetForClientAsync(clients).RunSynchronously();

    public async Task GetForClientAsync(IEnumerable<Client> clients)
    {
      if (clients == null)
        return;
      foreach (Client client in clients)
        await this.GetForClientAsync(client);
    }

    public void GetRoleFor(IEnumerable<CollaboratorRole> collaboratorRoles) => this.GetRoleForAsync(collaboratorRoles).RunSynchronously();

    public async Task GetRoleForAsync(IEnumerable<CollaboratorRole> collaboratorRoles)
    {
      if (collaboratorRoles == null)
        return;
      foreach (CollaboratorRole collaboratorRole in collaboratorRoles)
        await this.GetRoleForAsync(collaboratorRole);
    }

    public void GetRoleFor(CollaboratorRole collaboratorRole) => this.GetRoleForAsync(collaboratorRole).RunSynchronously();

    public async Task GetRoleForAsync(CollaboratorRole collaboratorRole)
    {
      if (collaboratorRole == null)
        ;
      else
      {
        CollaboratorRole collaboratorRole1 = collaboratorRole;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        collaboratorRole1.Role = dataSet.BusinessRoles.SingleOrDefault<BusinessRole>((Func<BusinessRole, bool>) (x => x.Id == collaboratorRole.RoleId));
        collaboratorRole1 = (CollaboratorRole) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetRoleFor(
      IEnumerable<ImplementationCost> implementationCosts)
    {
      this.GetRoleForAsync(implementationCosts).RunSynchronously();
    }

    public async Task GetRoleForAsync(
      IEnumerable<ImplementationCost> implementationCosts)
    {
      if (implementationCosts == null)
        return;
      foreach (ImplementationCost implementationCost in implementationCosts)
        await this.GetRoleForAsync(implementationCost);
    }

    public void GetRoleFor(ImplementationCost implementationCost) => this.GetRoleForAsync(implementationCost).RunSynchronously();

    public async Task GetRoleForAsync(ImplementationCost implementationCost)
    {
      if (implementationCost == null)
        ;
      else
      {
        ImplementationCost implementationCost1 = implementationCost;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        implementationCost1.Role = dataSet.BusinessRoles.SingleOrDefault<BusinessRole>((Func<BusinessRole, bool>) (x => x.Id == implementationCost.RoleId));
        implementationCost1 = (ImplementationCost) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetRoleFor(IEnumerable<RoleCost> roleCosts) => this.GetRoleForAsync(roleCosts).RunSynchronously();

    public async Task GetRoleForAsync(IEnumerable<RoleCost> roleCosts)
    {
      if (roleCosts == null)
        return;
      foreach (RoleCost roleCost in roleCosts)
        await this.GetRoleForAsync(roleCost);
    }

    public void GetRoleFor(RoleCost roleCost) => this.GetRoleForAsync(roleCost).RunSynchronously();

    public async Task GetRoleForAsync(RoleCost roleCost)
    {
      if (roleCost == null)
        ;
      else
      {
        RoleCost roleCost1 = roleCost;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        roleCost1.Role = dataSet.BusinessRoles.SingleOrDefault<BusinessRole>((Func<BusinessRole, bool>) (x => x.Id == roleCost.RoleId));
        roleCost1 = (RoleCost) null;
        //dataSet = (DataSet) null;
      }
    }

    public void GetRoleFor(
      IEnumerable<RoleIdeaAuthorisation> roleIdeaAuthorisations)
    {
      this.GetRoleForAsync(roleIdeaAuthorisations).RunSynchronously();
    }

    public async Task GetRoleForAsync(
      IEnumerable<RoleIdeaAuthorisation> roleIdeaAuthorisations)
    {
      if (roleIdeaAuthorisations == null)
        return;
      foreach (RoleIdeaAuthorisation roleIdeaAuthorisation in roleIdeaAuthorisations)
        await this.GetRoleForAsync(roleIdeaAuthorisation);
    }

    public void GetRoleFor(RoleIdeaAuthorisation roleIdeaAuthorisation) => this.GetRoleForAsync(roleIdeaAuthorisation).RunSynchronously();

    public async Task GetRoleForAsync(RoleIdeaAuthorisation roleIdeaAuthorisation)
    {
      if (roleIdeaAuthorisation == null)
        ;
      else
      {
        RoleIdeaAuthorisation ideaAuthorisation = roleIdeaAuthorisation;
        var dataSet = await _unitOfWork.GetDataSetAsync();
        ideaAuthorisation.Role = dataSet.BusinessRoles.SingleOrDefault<BusinessRole>((Func<BusinessRole, bool>) (x => x.Id == roleIdeaAuthorisation.RoleId));
        ideaAuthorisation = (RoleIdeaAuthorisation) null;
        //dataSet = (DataSet) null;
      }
    }

    public BusinessRole GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<BusinessRole> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (BusinessRole) null;
      var dataSetAsync = await _unitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessRoles.SingleOrDefault<BusinessRole>((Func<BusinessRole, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
            BusinessRole entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(BusinessRole entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(BusinessRole entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<BusinessRole> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<BusinessRole> entities)
    {
      if (entities == null)
        throw new DuplicateException("The roles are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
