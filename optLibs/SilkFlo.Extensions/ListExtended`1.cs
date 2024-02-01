// Decompiled with JetBrains decompiler
// Type: SilkFlo.ListExtended`1
// Assembly: SilkFlo.Extensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A5768D04-3FB2-4269-90BC-6B3AC9BEFFB0
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Extensions.dll

using System;
using System.Collections.Generic;

namespace SilkFlo
{
  public class ListExtended<T> : List<T>
  {
    public ListExtended()
    {
    }

    public ListExtended(IEnumerable<T> collection)
      : base(collection)
    {
    }

    public ListExtended(int capacity)
      : base(capacity)
    {
    }

    public bool IsRemoved { get; set; }

    public new void Clear()
    {
      base.Clear();
      this.IsRemoved = true;
    }

    public new bool Remove(T item)
    {
      this.IsRemoved = base.Remove(item);
      return this.IsRemoved;
    }

    public new int RemoveAll(Predicate<T> match)
    {
      int num = base.RemoveAll(match);
      this.IsRemoved = num > 0;
      return num;
    }

    public new void RemoveAt(int index)
    {
      try
      {
        base.RemoveAt(index);
        this.IsRemoved = true;
      }
      catch
      {
        throw;
      }
    }

    public new void RemoveRange(int index, int count)
    {
      try
      {
        base.RemoveRange(index, count);
        this.IsRemoved = true;
      }
      catch
      {
        throw;
      }
    }
  }
}
