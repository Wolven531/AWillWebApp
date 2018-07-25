// <copyright file="GlobalSuppressions.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
	"StyleCop.CSharp.SpacingRules",
	"SA1027:Tabs must not be used",
	Justification = "Developer experience")
]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
	"StyleCop.CSharp.DocumentationRules",
	"SA1636:File header copyright text must match",
	Justification = "Still have not found where PlaceHolderCompany setting is for copyright...")
]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
	"StyleCop.CSharp.DocumentationRules",
	"SA1652:Enable XML documentation output",
	Justification = "Unsure if I want to deal with all the warnings this may cause... see https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA0001.md")
]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
	"StyleCop.CSharp.ReadabilityRules",
	"SA1101:Prefix local calls with this",
	Justification = "Unnecessary code")
]
