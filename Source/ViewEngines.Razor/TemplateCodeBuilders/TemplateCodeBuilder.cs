﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Razor;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser;

using Junior.Common;
using Junior.Route.Common;

namespace Junior.Route.ViewEngines.Razor.TemplateCodeBuilders
{
	public abstract class TemplateCodeBuilder : ITemplateCodeBuilder
	{
		private static readonly string[] _defaultNamespaces = new[] { "System", "System.Collections.Generic", "System.Linq" };
		private static readonly string _namespace = typeof(TemplateCodeBuilder).Namespace + ".DynamicTemplates";
		private readonly RazorCodeLanguage _codeLanguage;

		protected TemplateCodeBuilder(RazorCodeLanguage codeLanguage)
		{
			codeLanguage.ThrowIfNull("codeLanguage");

			_codeLanguage = codeLanguage;
		}

		public BuildCodeResult BuildCode<TTemplate>(string templateContents, string className, Action<CodeTypeDeclaration> typeConfigurationDelegate, IEnumerable<string> namespaceImports)
			where TTemplate : ITemplate
		{
			templateContents.ThrowIfNull("templateContents");
			className.ThrowIfNull("className");
			namespaceImports.ThrowIfNull("namespaceImports");

			string defaultBaseClass = BuildTemplateTypeName<TTemplate>();
			string templateWriterNamespaceAndTypeName = MakeGlobalNamespace(typeof(TemplateWriter).FullName);

			var host = new RazorEngineHost(_codeLanguage, () => new HtmlMarkupParser())
				{
					DefaultBaseClass = defaultBaseClass,
					DefaultClassName = className,
					DefaultNamespace = _namespace,
					GeneratedClassContext = new GeneratedClassContext("Execute", "Write", "WriteLiteral", "WriteTo", "WriteLiteralTo", templateWriterNamespaceAndTypeName, "DefineSection")
				};

			IEnumerable<string> namespaces = _defaultNamespaces.Concat(namespaceImports).Distinct();

			host.NamespaceImports.AddRange(namespaces);

			var templateEngine = new RazorTemplateEngine(host);
			GeneratorResults results = templateEngine.GenerateCode(new StringReader(templateContents));
			CodeTypeDeclaration codeTypeDeclaration = results.GeneratedCode.Namespaces[0].Types[0];
			ConstructorInfo[] constructors = typeof(TTemplate).GetConstructors(BindingFlags.Public | BindingFlags.Instance);

			GenerateConstructors(constructors, codeTypeDeclaration);

			if (typeConfigurationDelegate != null)
			{
				typeConfigurationDelegate(codeTypeDeclaration);
			}

			return new BuildCodeResult(results.GeneratedCode, String.Format("{0}.{1}", _namespace, className));
		}

		public BuildCodeResult BuildCode<TTemplate>(string templateContents, string className, Action<CodeTypeDeclaration> typeConfigurationDelegate, params string[] namespaceImports)
			where TTemplate : ITemplate
		{
			return BuildCode<TTemplate>(templateContents, className, typeConfigurationDelegate, (IEnumerable<string>)namespaceImports);
		}

		protected abstract string MakeGlobalNamespace(string @namespace);
		protected abstract string MakeTypeName(Type type);

		private string BuildTemplateTypeName<TTemplate>()
		{
			Type templateType = typeof(TTemplate);

			if (!templateType.IsGenericTypeDefinition && !templateType.IsGenericType)
			{
				return templateType.FullName;
			}

			return MakeTypeName(templateType);
		}

		private static void GenerateConstructors(ConstructorInfo[] constructors, CodeTypeDeclaration codeTypeDeclaration)
		{
			if (constructors == null || !constructors.Any())
			{
				return;
			}

			// Remove constructors generated by Razor
			foreach (CodeConstructor existingConstructor in codeTypeDeclaration.Members.OfType<CodeConstructor>().ToArray())
			{
				codeTypeDeclaration.Members.Remove(existingConstructor);
			}
			// Add the templateContents type's constructors
			foreach (ConstructorInfo constructor in constructors)
			{
				var newConstructor = new CodeConstructor { Attributes = MemberAttributes.Public };

				foreach (ParameterInfo parameter in constructor.GetParameters())
				{
					newConstructor.Parameters.Add(new CodeParameterDeclarationExpression(parameter.ParameterType, parameter.Name));
					newConstructor.BaseConstructorArgs.Add(new CodeSnippetExpression(parameter.Name));
				}

				codeTypeDeclaration.Members.Add(newConstructor);
			}
		}
	}
}