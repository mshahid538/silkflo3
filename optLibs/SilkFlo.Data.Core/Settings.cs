// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Settings
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using System.Diagnostics;
using System.Reflection;

namespace SilkFlo.Data.Core
{
  public class Settings
  {
    public static bool IsOpening { get; set; }

    public static string DateFormatLong => "dd MMM yyyy";

    public static string DateFormatShort => "yyyy-MM-dd";

    public static string DateTimeFormatShort => "yyyy-MM-dd hh:mm:ss";

    public static string ApplicationName => "SilkFlo";

    public static string Version => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

    public static string VersionString => "Version " + Settings.Version;
  }
}
