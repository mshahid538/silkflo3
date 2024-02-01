// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Application.HotSpot
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using PetaPoco;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Application
{
    [TableName("HotSpots")]
  [XmlType(Namespace = "Application")]
  public class HotSpot : Abstract
  {
    private string _id = "";
    private string _name = "";
    private string _text = "";
    private string _width = "";

    public HotSpot() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Text
    {
      get => this._text;
      set
      {
        value = value?.Trim();
        if (this._text == value)
          return;
        this._text = value;
        this.IsSaved = false;
      }
    }

    public string Width
    {
      get => this._width;
      set
      {
        value = value?.Trim();
        if (this._width == value)
          return;
        this._width = value;
        this.IsSaved = false;
      }
    }

    public void Update(HotSpot hotSpot)
    {
      this.Name = hotSpot.Name;
      this.Text = hotSpot.Text;
      this.Width = hotSpot.Width;
    }

    public override string ToString() => this.Name;
  }
}
