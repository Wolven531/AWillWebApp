// <copyright file="GlobalSuppressions.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
	"StyleCop.CSharp.SpacingRules",
	"SA1027:Tabs must not be used",
	Justification = "Developer experience")
]
[assembly: SuppressMessage(
	"StyleCop.CSharp.DocumentationRules",
	"SA1636:File header copyright text must match",
	Justification = "Still have not found where PlaceHolderCompany setting is for copyright...")
]
[assembly: SuppressMessage(
	"StyleCop.CSharp.DocumentationRules",
	"SA1652:Enable XML documentation output",
	Justification = "Unsure if I want to deal with all the warnings this may cause... see https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA0001.md")
]
[assembly: SuppressMessage(
	"StyleCop.CSharp.ReadabilityRules",
	"SA1101:Prefix local calls with this",
	Justification = "Unnecessary code")
]
[assembly: SuppressMessage(
	"Reliability",
	"CA2007:Do not directly await a Task",
	Justification = "Unnecessary code")
]
[assembly: SuppressMessage(
	"StyleCop.CSharp.SpacingRules",
	"SA1005:Single line comments must begin with single space",
	Justification = "Fights convention applied by auto-commenting")
]
[assembly: SuppressMessage(
	"StyleCop.CSharp.NamingRules",
	"SA1309:Field names must not begin with underscore",
	Justification = "Private members should begin with underscore")
]
[assembly: SuppressMessage(
	"StyleCop.CSharp.NamingRules",
	"SA1306:Field names must begin with lower-case letter",
	Justification = "Private members should begin with underscore")
]
[assembly: SuppressMessage(
	"StyleCop.CSharp.LayoutRules",
	"SA1515:Single-line comment must be preceded by blank line",
	Justification = "Fights with the auto commenter")
]
[assembly: SuppressMessage(
	"StyleCop.CSharp.LayoutRules",
	"SA1512:Single-line comments must not be followed by blank line",
	Justification = "Fights with the auto commenter")
]
[assembly: SuppressMessage(
	"StyleCop.CSharp.ReadabilityRules",
	"SA1413:Use trailing comma in multi-line initializers",
	Justification = "Trailing commas are messy")
]
[assembly: SuppressMessage(
	"StyleCop.CSharp.NamingRules",
	"SA1300:Element must begin with upper-case letter",
	Justification = "Ugly variable names")
]
