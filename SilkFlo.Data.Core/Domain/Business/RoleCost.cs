// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Business.RoleCost
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using PetaPoco;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
    [TableName("RoleCosts")]
  [XmlType(Namespace = "Business")]
  public class RoleCost : Abstract
  {
    private string _id = "";
    private Client _client;
    private string _clientId;
    private Decimal _cost;
    private Role _role;
    private string _roleId;

    public RoleCost() => this._createdDate = new System.DateTime?(System.DateTime.Now);

        [Ignore]
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

        [Ignore]
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

    public Decimal Cost
    {
      get => this._cost;
      set
      {
        if (this._cost == value)
          return;
        this._cost = value;
        this.IsSaved = false;
      }
    }

    public string RoleString { get; set; }

        [Ignore]
        [IgnoreDataMember]
    [XmlIgnore]
    public Role Role
    {
      get => this._role;
      set
      {
        if (this._role == value)
          return;
        this._role = value;
        this.RoleString = value == null ? "" : value.ToString();
      }
    }

    public string RoleId
    {
      get => this._role != null ? this._role.Id : this._roleId;
      set
      {
        value = value?.Trim();
        this._roleId = value;
        if (this._role != null && this._role.Id != this._roleId)
          this._role = (Role) null;
        this.IsSaved = false;
      }
    }

    public void Update(RoleCost roleCost)
    {
      this.ClientId = roleCost.ClientId;
      this.Cost = roleCost.Cost;
      this.RoleId = roleCost.RoleId;
    }
  }
}
