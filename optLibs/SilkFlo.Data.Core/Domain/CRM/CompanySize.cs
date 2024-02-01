// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.CRM.CompanySize
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.CRM
{
  [XmlType(Namespace = "CRM")]
  public class CompanySize : Abstract
  {
    private string _id = "";
    private string _name = "";
    private int _sort;

    public CompanySize() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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
    public List<Prospect> Prospects { get; set; } = new List<Prospect>();

    public void Update(CompanySize companySize)
    {
      this.Name = companySize.Name;
      this.Sort = companySize.Sort;
    }

    public override string ToString() => this.Name;
  }
}
