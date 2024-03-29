﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Shared.TaskFrequency
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using PetaPoco;
using SilkFlo.Data.Core.Domain.Business;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shared
{
    [TableName("TaskFrequencies")]
  [XmlType(Namespace = "Shared")]
  public class TaskFrequency : Abstract
  {
    private string _id = "";
    private int _hour;
    private string _name = "";
    private int _sort;

    public TaskFrequency() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public int Hour
    {
      get => this._hour;
      set
      {
        if (this._hour == value)
          return;
        this._hour = value;
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

        [Ignore]
        [IgnoreDataMember]
    [XmlIgnore]
    public List<Idea> Ideas { get; set; } = new List<Idea>();

    public void Update(TaskFrequency taskFrequency)
    {
      this.Hour = taskFrequency.Hour;
      this.Name = taskFrequency.Name;
      this.Sort = taskFrequency.Sort;
    }

    public override string ToString() => this.Name;
  }
}
