// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.ServiceLocation.IServiceLocator
// Assembly: fx.Configuration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98DE432C-119E-449E-A101-030B64638B2E
// Assembly location: A:\StockSharpBin\Data\fx.Configuration.dll

using System;
using System.Collections.Generic;

namespace Microsoft.Practices.ServiceLocation
{
  public interface IServiceLocator : IServiceProvider
  {
    object GetInstance(Type serviceType);

    object GetInstance(Type serviceType, string key);

    IEnumerable<object> GetAllInstances(Type serviceType);

    TService GetInstance<TService>();

    TService GetInstance<TService>(string key);

    IEnumerable<TService> GetAllInstances<TService>();
  }
}
