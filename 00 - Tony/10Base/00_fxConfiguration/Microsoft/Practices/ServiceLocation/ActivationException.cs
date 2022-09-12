// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.ServiceLocation.ActivationException
// Assembly: fx.Configuration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98DE432C-119E-449E-A101-030B64638B2E
// Assembly location: A:\StockSharpBin\Data\fx.Configuration.dll

using System;
using System.Runtime.Serialization;

namespace Microsoft.Practices.ServiceLocation
{
  [Serializable]
  public class ActivationException : Exception
  {
    public ActivationException()
    {
    }

    public ActivationException(string message)
      : base(message)
    {
    }

    public ActivationException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    protected ActivationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
