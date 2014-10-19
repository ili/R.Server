using System;
using System.Linq;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	public class RegSecurityPolicyStrategy
		: RegKeyedElementsStrategy<string, SecurityPolicyInfo, SecurityPolicyAttribute>
	{
		public RegSecurityPolicyStrategy(IServicePublisher publisher) : base(publisher)
		{
		}

		public override SecurityPolicyInfo CreateElement(
			IExtensionAttachmentContext<Type, Attribute> context,
			SecurityPolicyAttribute attr)
		{
			if (context.ExtensionType.IsGenericTypeDefinition)
				throw new ArgumentException("Generic type definitions not supported");
			var ifaces = context.ExtensionType.GetInterfaces().Where(
				iface => iface.GetGenericTypeDefinition() == typeof (ISecurityPolicy<>)).ToArray();
			if (ifaces.Length == 0)
				throw new ArgumentException("Security policy must implement at least one of generic "
					+ "interfaces, based on '" + typeof (ISecurityPolicy<>).FullName + "' type definition");
			return new SecurityPolicyInfo(attr.Name, context.ExtensionType,
				ifaces.ConvertAll(iface => iface.GetGenericArguments()[0]));
		}
	}
}