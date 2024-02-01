// Decompiled with JetBrains decompiler
// Type: SilkFlo.ZipArchiveExtensions
// Assembly: SilkFlo.Extensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A5768D04-3FB2-4269-90BC-6B3AC9BEFFB0
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Extensions.dll

using System.IO.Compression;

namespace SilkFlo
{
  public static class ZipArchiveExtensions
  {
    public static void Clear(this ZipArchive zipArchive)
    {
      for (int index = zipArchive.Entries.Count - 1; index >= 0; --index)
        zipArchive.Entries[index].Delete();
    }
  }
}
