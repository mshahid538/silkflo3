// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Business.Process
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using PetaPoco;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
    [TableName("Processes")]
  [XmlType(Namespace = "Business")]
  public class Process : Abstract
  {
    private string _id = "";
    private Client _client;
    private string _clientId;
    private string _name = "";
    private Team _team;
    private string _teamId;

    public Process() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string TeamString { get; set; }

        [Ignore]
        [IgnoreDataMember]
    [XmlIgnore]
    public Team Team
    {
      get => this._team;
      set
      {
        if (this._team == value)
          return;
        this._team = value;
        this.TeamString = value == null ? "" : value.ToString();
      }
    }

    public string TeamId
    {
      get => this._team != null ? this._team.Id : this._teamId;
      set
      {
        value = value?.Trim();
        this._teamId = value;
        if (this._team != null && this._team.Id != this._teamId)
          this._team = (Team) null;
        this.IsSaved = false;
      }
    }

        [Ignore]
        [IgnoreDataMember]
    [XmlIgnore]
    public List<Idea> Ideas { get; set; } = new List<Idea>();

    public void Update(Process process)
    {
      this.ClientId = process.ClientId;
      this.Name = process.Name;
      this.TeamId = process.TeamId;
    }

    public override string ToString() => this.Name;
  }
}
