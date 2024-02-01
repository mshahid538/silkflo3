﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Shared.NumberOfWaysToCompleteProcess
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shared
{
  [XmlType(Namespace = "Shared")]
  public class NumberOfWaysToCompleteProcess : Abstract
  {
    private string _id = "";
    private string _colour = "";
    private string _name = "";
    private string _shortName = "";
    private Decimal _weighting;

    public NumberOfWaysToCompleteProcess() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

    public string Colour
    {
      get => this._colour;
      set
      {
        value = value?.Trim();
        if (this._colour == value)
          return;
        this._colour = value;
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

    public string ShortName
    {
      get => this._shortName;
      set
      {
        value = value?.Trim();
        if (this._shortName == value)
          return;
        this._shortName = value;
        this.IsSaved = false;
      }
    }

    public Decimal Weighting
    {
      get => this._weighting;
      set
      {
        if (this._weighting == value)
          return;
        this._weighting = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Idea> Ideas { get; set; } = new List<Idea>();

    public void Update(
      NumberOfWaysToCompleteProcess numberOfWaysToCompleteProcess)
    {
      this.Colour = numberOfWaysToCompleteProcess.Colour;
      this.Name = numberOfWaysToCompleteProcess.Name;
      this.ShortName = numberOfWaysToCompleteProcess.ShortName;
      this.Weighting = numberOfWaysToCompleteProcess.Weighting;
    }

    public override string ToString() => this.Name;
  }
}
