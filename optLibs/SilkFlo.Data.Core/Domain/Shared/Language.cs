// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Shared.Language
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shared
{
  [XmlType(Namespace = "Shared")]
  public class Language : Abstract
  {
    private string _id = "";
    private string _locale = "";
    private string _name = "";

    public Language() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Locale
    {
      get => this._locale;
      set
      {
        value = value?.Trim();
        if (this._locale == value)
          return;
        this._locale = value;
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

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Client> Clients { get; set; } = new List<Client>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaApplicationVersion> IdeaApplicationVersions { get; set; } = new List<IdeaApplicationVersion>();

    public void Update(Language language)
    {
      this.Locale = language.Locale;
      this.Name = language.Name;
    }

    public override string ToString() => this.Name;
  }
}
