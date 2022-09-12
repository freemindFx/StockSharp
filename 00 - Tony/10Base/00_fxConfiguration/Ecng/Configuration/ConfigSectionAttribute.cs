// Decompiled with JetBrains decompiler
// Type: fx.Configuration.ConfigSectionAttribute
// Assembly: fx.Configuration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98DE432C-119E-449E-A101-030B64638B2E
// Assembly location: A:\StockSharpBin\Data\fx.Configuration.dll

using Ecng.Common;
using System;
using System.Configuration;

namespace fx.Configuration
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = true)]
  public class ConfigSectionAttribute : FactoryAttribute
  {
    public ConfigSectionAttribute(Type sectionType)
      : base(sectionType)
    {
      if (!typeof (ConfigurationSection).IsAssignableFrom(sectionType))
        throw new ArgumentException(nameof (sectionType));
    }

    public Type SectionType => FactoryType;
  }
}
