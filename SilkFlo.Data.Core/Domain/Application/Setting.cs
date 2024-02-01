// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Application.Setting
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using PetaPoco;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Application
{
    [TableName("Settings")]
  [XmlType(Namespace = "Application")]
  public class Setting : Abstract
  {
    private string _id = "";
    private string _value = "";

    public Setting() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Value
    {
      get => this._value;
      set
      {
        value = value?.Trim();
        if (this._value == value)
          return;
        this._value = value;
        this.IsSaved = false;
      }
    }

    public void Update(Setting setting) => this.Value = setting.Value;
  }
}
