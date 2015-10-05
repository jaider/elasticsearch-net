﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.IndexSettings.IndexTemplates.GetIndexTemplate
{
	public class GetTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var name = "temp";
			await GET($"/_template/{name}")
				.Fluent(c => c.GetIndexTemplate(p=>p.Name(name)))
				.Request(c => c.GetIndexTemplate(new GetIndexTemplateRequest(name)))
				.FluentAsync(c => c.GetIndexTemplateAsync(p=>p.Name(name)))
				.RequestAsync(c => c.GetIndexTemplateAsync(new GetIndexTemplateRequest(name)))
				;

			await GET($"/_template")
				.Fluent(c => c.GetIndexTemplate())
				.Request(c => c.GetIndexTemplate(new GetIndexTemplateRequest()))
				.FluentAsync(c => c.GetIndexTemplateAsync())
				.RequestAsync(c => c.GetIndexTemplateAsync(new GetIndexTemplateRequest()))
				;
		}
	}
}