// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Business.Department
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using PetaPoco;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
    [TableName("Departments")]
  [XmlType(Namespace = "Business")]
  public class Department : Abstract
  {
    private string _id = "";
    private Client _client;
    private string _clientId;
    private string _name = "";

    public Department() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

        [Ignore]
        [IgnoreDataMember]
    [XmlIgnore]
    public List<Idea> Ideas { get; set; } = new List<Idea>();

        [Ignore]
        [IgnoreDataMember]
    [XmlIgnore]
    public List<Team> Teams { get; set; } = new List<Team>();

        [Ignore]
        [IgnoreDataMember]
    [XmlIgnore]
    public List<User> Users { get; set; } = new List<User>();

    public void Update(Department department)
    {
      this.ClientId = department.ClientId;
      this.Name = department.Name;
    }

    public override string ToString() => this.Name;
  }
}
