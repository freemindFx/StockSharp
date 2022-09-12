// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.ServiceLocation.ServiceLocator
// Assembly: fx.Configuration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98DE432C-119E-449E-A101-030B64638B2E
// Assembly location: A:\StockSharpBin\Data\fx.Configuration.dll

using fx.Configuration.Properties;
using System;

namespace Microsoft.Practices.ServiceLocation
{
  public static class ServiceLocator
  {
    private static ServiceLocatorProvider currentProvider;

    public static IServiceLocator Current
    {
      get
      {
        if (!IsLocationProviderSet )
          throw new InvalidOperationException(Resources.ServiceLocationProviderNotSetMessage);
        return currentProvider();
      }
    }

    public static void SetLocatorProvider(ServiceLocatorProvider newProvider) => currentProvider = newProvider;

    public static bool IsLocationProviderSet => currentProvider != null;
  }
}
