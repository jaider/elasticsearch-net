﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.CodeStandards
{
	public class Descriptors
	{
		/**
		* Every descriptor should inherit from `DescriptorBase`, this hides object members from the fluent interface
		*/
		[U]
		public void ShouldInheritFromDescriptorBase()
		{
			var descriptors = from t in typeof(DescriptorBase<,>).Assembly.Types()
							  where t.Name.EndsWith("Descriptor", StringComparison.Ordinal) && t.IsClass
								&& (!t.GetInterfaces().Any(i => i == typeof(IDescriptor)))
							  select t.FullName;
			descriptors.Should().BeEmpty();
		}

		/**
		* Methods taking a func should have that func return an interface
		*/
		[U] public void SelectorsReturnInterface()
		{
			var descriptors =
				from t in typeof(DescriptorBase<,>).Assembly.Types()
				where t.IsClass
				select t;
			var selectorMethods =
				from d in descriptors
				from m in d.GetMethods()
				let parameters = m.GetParameters()
				from p in parameters
				let type = p.ParameterType
				let isGeneric = type.IsGenericType
				where isGeneric
				let isFunc = type.GetGenericTypeDefinition() == typeof(Func<,>)
				where isFunc
				let lastArgIsNotInterface = !type.GetGenericArguments().Last().IsInterface
				where lastArgIsNotInterface
				select $"{m.Name} on {m.DeclaringType.Name}";

			selectorMethods.Should().BeEmpty();
		}

		//TODO methods taking params should also have a version taking IEnumerable
	}
}