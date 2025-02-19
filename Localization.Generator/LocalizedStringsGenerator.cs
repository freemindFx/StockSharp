﻿namespace StockSharp.Localization
{
	using System.Linq;
	using System.Text;
	using System.IO;
	using System.Text.Json;
	using System.Collections.Generic;

	using Microsoft.CodeAnalysis;

	[Generator]
	public class LocalizedStringsGenerator : ISourceGenerator
	{
		private class Pair
		{
			public string en { get; set; }
		}

		void ISourceGenerator.Initialize(GeneratorInitializationContext context)
		{
		}

		void ISourceGenerator.Execute(GeneratorExecutionContext context)
		{
			//System.Diagnostics.Debugger.Launch();

			var items = new StringBuilder();

			var dict = JsonSerializer.Deserialize<IDictionary<string, Pair>>(File.ReadAllText(Path.Combine(Path.GetDirectoryName(context.Compilation.SyntaxTrees.First().FilePath), "translation.json")));

			foreach (var p in dict)
			{
				var prop = p.Key;
				var xmlComment = p.Value.en.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

				items.AppendLine($@"		/// <summary>
		/// {xmlComment}
		/// </summary>
		public const string {prop}Key = nameof({prop});

		/// <summary>
		/// {xmlComment}
		/// </summary>
		public static string {prop} => GetString({prop}Key);").AppendLine();
			}

			var source = $@"// <auto-generated />

namespace StockSharp.Localization
{{
	partial class LocalizedStrings
	{{
{items}
	}}
}}";
			context.AddSource($"LocalizedStrings_Items.cs", source);
		}
	}
}
