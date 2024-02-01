// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.ChildDependencyException
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using System;

namespace SilkFlo.Data.Core
{
  public class ChildDependencyException : Exception
  {
    public ChildDependencyException()
    {
    }

    public ChildDependencyException(string message)
      : base(message)
    {
    }

    public ChildDependencyException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
