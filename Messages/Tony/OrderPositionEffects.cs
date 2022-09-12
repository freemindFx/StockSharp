namespace StockSharp.Messages
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Runtime.Serialization;
	
    using StockSharp.Localization;

    /* -------------------------------------------------------------------------------------------------------------------------------------------
    * 
    *  Tony 04: OrderPositionEffects
    * 
    * ------------------------------------------------------------------------------------------------------------------------------------------- */


    /// <summary>
    /// Indicates whether the resulting position after a trade should be an opening position or closing position.
    /// </summary>
    [DataContract]
	[Serializable]
	public enum OrderPositionEffects
	{
		/// <summary>
		/// Default behaviour.
		/// </summary>
		[EnumMember]
		[Display(ResourceType = typeof(LocalizedStrings), Name = LocalizedStrings.DefaultKey, Description = LocalizedStrings.DefaultBehaviourKey)]
		Default = 0,

		/// <summary>
		/// A trade should open a position.
		/// </summary>
		[EnumMember]
		[Display(ResourceType = typeof(LocalizedStrings), Name = LocalizedStrings.OpenOnlyKey, Description = LocalizedStrings.PositionEffectOpenOnlyKey)]
		OpenOnly = 1,

		/// <summary>
		/// A trade should bring the position towards zero, i.e. close as much as possible of any existing position and open an opposite position for any remainder.
		/// </summary>
		[EnumMember]
		[Display(ResourceType = typeof(LocalizedStrings), Name = LocalizedStrings.CloseOnlyKey, Description = LocalizedStrings.PositionEffectCloseOnlyKey)]
		CloseOnly = 2,

		/// <summary>
		/// A trade to hedge All Long Positions.
		/// </summary>
		[EnumMember]
		[Display(ResourceType = typeof( LocalizedStrings ), Name = LocalizedStrings.HedgeLongKey, Description = LocalizedStrings.HedgeLongKey)]
		HedgeLong = 3,

		/// <summary>
		/// A trade to hedge All Short Positions.
		/// </summary>
		[EnumMember]
		[Display(ResourceType = typeof( LocalizedStrings ), Name = LocalizedStrings.HedgeShortKey, Description = LocalizedStrings.HedgeShortKey)]
		HedgeShort = 4,

		/// <summary>
		/// A trade to hedge All Positions.
		/// </summary>
		[EnumMember]
		[Display(ResourceType = typeof( LocalizedStrings ), Name = LocalizedStrings.HedgeAllKey, Description = LocalizedStrings.HedgeAllKey)]
		HedgeAll = 5,

		/// <summary>
		/// A trade to Set Safety Net
		/// </summary>
		[EnumMember]
		[Display(ResourceType = typeof( LocalizedStrings ), Name = LocalizedStrings.SetSafetyKey, Description = LocalizedStrings.SetSafetyKey)]
		SetSafety = 6,

		/// <summary>
		/// A trade to Set Safety Net
		/// </summary>
		[EnumMember]
		[Display(ResourceType = typeof( LocalizedStrings ), Name = LocalizedStrings.SetBreakEvenKey, Description = LocalizedStrings.SetBreakEvenKey)]
		SetBreakEven = 7,

		/// <summary>
		/// A trade to Set Take Profit target
		/// </summary>
		[EnumMember]
		[Display(ResourceType = typeof( LocalizedStrings ), Name = LocalizedStrings.SetTakeProfitKey, Description = LocalizedStrings.SetTakeProfitKey)]
		SetTakeProfit = 8,

		/// <summary>
		/// Hedge or Close existing trades and open trades in opposite direction
		/// </summary>
		[EnumMember]
		[Display(ResourceType = typeof( LocalizedStrings ), Name = LocalizedStrings.ReverseDirectionKey, Description = LocalizedStrings.ReverseDirectionKey)]
		ReverseDirection = 9,

		/// <summary>
		/// Hedge or Close existing trades and open trades in opposite direction
		/// </summary>
		[EnumMember]
		[Display(ResourceType = typeof( LocalizedStrings ), Name = LocalizedStrings.EscapeWithoutLossKey, Description = LocalizedStrings.EscapeWithoutLossKey)]
		EscapeWithoutLoss = 10,
	}
}