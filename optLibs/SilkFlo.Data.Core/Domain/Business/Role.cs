﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Business.Role
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class Role : Abstract
  {
    private string _id = "";
    private Client _client;
    private string _clientId;
    private string _description = "";
    private bool _isBuiltIn;
    private string _name = "";
    private int _sort;

    public Role() => this._createdDate = new System.DateTime?(System.DateTime.Now);

    public override bool IsNew => string.IsNullOrWhiteSpace(this.Id);

    public string Id
    {
      get => this._id;
      set
      {
        value = value?.Trim();
        if (this._id == value)
          return;
        this._id = value;
        this.IsSaved = false;
      }
    }

    public string ClientString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Client Client
    {
      get => this._client;
      set
      {
        if (this._client == value)
          return;
        this._client = value;
        this.ClientString = value == null ? "" : value.ToString();
      }
    }

    public string ClientId
    {
      get => this._client != null ? this._client.Id : this._clientId;
      set
      {
        value = value?.Trim();
        this._clientId = value;
        if (this._client != null && this._client.Id != this._clientId)
          this._client = (Client) null;
        this.IsSaved = false;
      }
    }

    public string Description
    {
      get => this._description;
      set
      {
        value = value?.Trim();
        if (this._description == value)
          return;
        this._description = value;
        this.IsSaved = false;
      }
    }

    public bool IsBuiltIn
    {
      get => this._isBuiltIn;
      set
      {
        if (this._isBuiltIn == value)
          return;
        this._isBuiltIn = value;
        this.IsSaved = false;
      }
    }

    public string Name
    {
      get => this._name;
      set
      {
        value = value?.Trim();
        if (this._name == value)
          return;
        this._name = value;
        this.IsSaved = false;
      }
    }

    public int Sort
    {
      get => this._sort;
      set
      {
        if (this._sort == value)
          return;
        this._sort = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<CollaboratorRole> CollaboratorRoles { get; set; } = new List<CollaboratorRole>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<ImplementationCost> ImplementationCosts { get; set; } = new List<ImplementationCost>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<RoleCost> RoleCosts { get; set; } = new List<RoleCost>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<RoleIdeaAuthorisation> RoleIdeaAuthorisations { get; set; } = new List<RoleIdeaAuthorisation>();

    public void Update(Role role)
    {
      if (role.ClientId == null)
        this.Client = (Client) null;
      this.ClientId = role.ClientId;
      this.Description = role.Description;
      this.IsBuiltIn = role.IsBuiltIn;
      this.Name = role.Name;
      this.Sort = role.Sort;
    }

    public override string ToString() => this.Name;
  }
}
