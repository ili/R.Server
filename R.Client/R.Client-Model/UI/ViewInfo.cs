using System;

namespace R.Client.Model
{
	public class ViewInfo
	{
		private readonly Type _viewType;
		private readonly Type _viewContract;
		private readonly string _technology;
		private readonly string _layer;

		public ViewInfo(Type viewType, Type viewContract, string technology, string layer)
		{
			if (viewType == null)
				throw new ArgumentNullException("viewType");
			if (viewContract == null)
				throw new ArgumentNullException("viewContract");
			if (string.IsNullOrEmpty(technology))
				throw new ArgumentNullException("technology");
			_viewType = viewType;
			_viewContract = viewContract;
			_technology = technology;
			_layer = layer;
		}

		public Type ViewType
		{
			get { return _viewType; }
		}

		public Type ViewContract
		{
			get { return _viewContract; }
		}

		public string Technology
		{
			get { return _technology; }
		}

		public string Layer
		{
			get { return _layer; }
		}
	}
}
