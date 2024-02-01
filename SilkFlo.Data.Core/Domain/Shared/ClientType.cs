// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Shared.ClientType
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using PetaPoco;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shop;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shared
{
    [TableName("ClientTypes")]
  [XmlType(Namespace = "Shared")]
  public class ClientType : Abstract
  {
    private string _id = "";
    private string _description = "";
    private string _name = "";

    public ClientType() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

        [Ignore]
        [IgnoreDataMember]
    [XmlIgnore]
    public List<Client> Clients { get; set; } = new List<Client>();

        [Ignore]
        [IgnoreDataMember]
    [XmlIgnore]
    public List<Prospect> Prospects { get; set; } = new List<Prospect>();

        [Ignore]
        [IgnoreDataMember]
    [XmlIgnore]
    public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public void Update(ClientType clientType)
    {
      this.Description = clientType.Description;
      this.Name = clientType.Name;
    }

    public override string ToString() => this.Name;
  }
}
