// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Role
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using PetaPoco;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain
{
    [TableName("Roles")]
  [XmlType(Namespace = "")]
  public class Role : Abstract
  {
    private string _id = "";
    private string _description = "";
    private string _name = "";
    private int _policyCount;
    private int _sort;

    public Role() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public int PolicyCount
    {
      get => this._policyCount;
      set
      {
        if (this._policyCount == value)
          return;
        this._policyCount = value;
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

        [Ignore]
        [IgnoreDataMember]
    [XmlIgnore]
    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public void Update(Role role)
    {
      this.Description = role.Description;
      this.Name = role.Name;
      this.PolicyCount = role.PolicyCount;
      this.Sort = role.Sort;
    }

    public override string ToString() => this.Name;
  }
}
