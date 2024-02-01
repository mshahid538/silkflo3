// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Shop.Currency
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shop
{
  [XmlType(Namespace = "Shop")]
  public class Currency : Abstract
  {
    private string _id = "";
    private string _symbol = "";

    public Currency() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Symbol
    {
      get => this._symbol;
      set
      {
        value = value?.Trim();
        if (this._symbol == value)
          return;
        this._symbol = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Client> Clients { get; set; } = new List<Client>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Price> Prices { get; set; } = new List<Price>();

    public void Update(Currency currency) => this.Symbol = currency.Symbol;

    public override string ToString() => this.Id + " (" + this.Symbol + ")";
  }
}
