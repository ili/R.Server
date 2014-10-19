using System;

namespace R.Client.Model
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ViewAttribute : Attribute
	{
		private readonly Type _viewContract;
		private readonly string _technology;
		private readonly string _layer;

		public ViewAttribute(Type viewContract, string technology, string layer)
		{
			_viewContract = viewContract;
			_technology = technology;
			_layer = layer;
		}

		/// <summary>
		/// View public contract.
		/// </summary>
		public Type ViewContract
		{
			get { return _viewContract; }
		}

		/// <summary>
		/// View technology.
		/// </summary>
		public string Technology
		{
			get { return _technology; }
		}

		/// <summary>
		/// Layer of extension.
		/// </summary>
		public string Layer
		{
			get { return _layer; }
		}
	}
}
