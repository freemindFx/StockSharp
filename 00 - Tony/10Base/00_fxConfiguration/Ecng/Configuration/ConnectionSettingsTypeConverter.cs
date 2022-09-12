// Decompiled with JetBrains decompiler
// Type: fx.Configuration.ConnectionSettingsTypeConverter
// Assembly: fx.Configuration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98DE432C-119E-449E-A101-030B64638B2E
// Assembly location: A:\StockSharpBin\Data\fx.Configuration.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace fx.Configuration
{
  public class ConnectionSettingsTypeConverter : TypeConverter
  {
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      string connectionStringName = (string) value;
      return ConfigManagerEx.GetSection<ConnectionStringsSection>().ConnectionStrings.Cast<ConnectionStringSettings>().Single( settings => settings.Name == connectionStringName ).ConnectionString;
    }
  }
}
