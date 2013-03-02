﻿using System.Configuration;

namespace Junior.Route.Routing.AntiCsrf
{
	public class ConfigurationSectionAntiCsrfConfiguration : IAntiCsrfConfiguration
	{
		private readonly AntiCsrfConfigurationSection _configurationSection = (AntiCsrfConfigurationSection)ConfigurationManager.GetSection("antiCsrfHelper") ?? new AntiCsrfConfigurationSection();

		public bool Enabled
		{
			get
			{
				return _configurationSection.Enabled;
			}
		}

		public bool ValidateHttpPost
		{
			get
			{
				return _configurationSection.ValidateHttpPost;
			}
		}

		public bool ValidateHttpPut
		{
			get
			{
				return _configurationSection.ValidateHttpPut;
			}
		}

		public bool ValidateHttpDelete
		{
			get
			{
				return _configurationSection.ValidateHttpDelete;
			}
		}

		public string CookieName
		{
			get
			{
				return _configurationSection.CookieName;
			}
		}

		public string FormFieldName
		{
			get
			{
				return _configurationSection.FormFieldName;
			}
		}
	}
}