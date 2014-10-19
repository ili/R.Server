using System;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// �������� ���������� ������������� ���������� ��������.
	/// </summary>
	public class ServicePublishingStrategy : AttachmentStrategyBase<ServiceAttribute>
	{
		private readonly IServicePublisher _publisher;

		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public ServicePublishingStrategy(IServicePublisher publisher)
		{
			_publisher = publisher;
		}

		/// <summary>
		/// ���������� ����������.
		/// </summary>
		protected override void Attach(
			IExtensionAttachmentContext<Type, Attribute> context,
			ServiceAttribute attribute)
		{
			// Single instance for all contracts
			var holder = new ServiceHolder(context.ExtensionType);
			foreach (var contract in attribute.ContractTypes)
				_publisher.Publish(
					contract,
					(type, pub) => holder.CreateInstance(_publisher));
		}

		#region ServiceHolder class
		private class ServiceHolder
		{
			private readonly Type _implType;
			private object _instance;
			private readonly object _lockFlag = new object();

			public ServiceHolder(Type implType)
			{
				_implType = implType;
			}

			public object CreateInstance(IServiceProvider publisher)
			{
				if (_instance == null)
					lock (_lockFlag)
						if (_instance == null)
							_instance = _implType.CreateInstance(publisher);
				return _instance;
			}
		}
		#endregion
	}
}