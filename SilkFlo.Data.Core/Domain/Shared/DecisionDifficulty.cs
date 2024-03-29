﻿// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Domain.Shared.DecisionDifficulty
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using PetaPoco;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Shared
{
    [TableName("DecisionDifficulties")]
  [XmlType(Namespace = "Shared")]
  public class DecisionDifficulty : Abstract
  {
    private string _id = "";
    private string _colour = "";
    private string _name = "";
    private string _shortName = "";
    private Decimal _weighting;

    public DecisionDifficulty() => this._createdDate = new System.DateTime?(System.DateTime.Now);

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

        [Ignore]
        [IgnoreDataMember]
    [XmlIgnore]
    public List<Idea> Ideas { get; set; } = new List<Idea>();

    public void Update(DecisionDifficulty decisionDifficulty)
    {
      this.Colour = decisionDifficulty.Colour;
      this.Name = decisionDifficulty.Name;
      this.ShortName = decisionDifficulty.ShortName;
      this.Weighting = decisionDifficulty.Weighting;
    }

    public override string ToString() => this.Name;
  }
}
