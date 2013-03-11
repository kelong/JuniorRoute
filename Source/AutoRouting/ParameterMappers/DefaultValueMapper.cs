﻿using System;
using System.Reflection;
using System.Web;

using Junior.Common;

namespace Junior.Route.AutoRouting.ParameterMappers
{
	public class DefaultValueMapper : IParameterMapper
	{
		public bool CanMapType(HttpContextBase context, Type parameterType)
		{
			context.ThrowIfNull("context");
			parameterType.ThrowIfNull("parameterType");

			return true;
		}

		public MapResult Map(HttpContextBase context, Type type, MethodInfo method, ParameterInfo parameter)
		{
			context.ThrowIfNull("context");
			type.ThrowIfNull("type");
			method.ThrowIfNull("method");
			parameter.ThrowIfNull("parameter");

			return MapResult.ValueMapped(parameter.ParameterType.GetDefaultValue());
		}
	}
}