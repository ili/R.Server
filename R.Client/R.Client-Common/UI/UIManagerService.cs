using System;
using System.Collections.Generic;
using System.Reflection;

using R.Client.Model;

using Rsdn.SmartApp;

namespace R.Client.Common
{
	using ControllersSvc = IRegElementsService<UIControllerInfo>;
	using ViewsSvc = IRegElementsService<ViewInfo>;

	public class UIManagerService : IUIManagerService
	{
		private readonly IServiceProvider _provider;
		private readonly string[] _layers;

		private readonly Dictionary<ControllerDescriptor, UIControllerInfo> _controllers =
			new Dictionary<ControllerDescriptor, UIControllerInfo>();
		private readonly Dictionary<ViewDescriptor, ViewInfo> _views =
			new Dictionary<ViewDescriptor, ViewInfo>();

		public UIManagerService(IServiceProvider provider, string technology, string[] layers)
		{
			if (provider == null)
				throw new ArgumentNullException("provider");
			if (layers == null)
				throw new ArgumentNullException("layers");
			_provider = provider;
			_layers = layers;
			var controllersSvc = _provider.GetService<ControllersSvc>();
			if (controllersSvc != null)
				foreach (var info in controllersSvc.GetRegisteredElements())
					_controllers.Add(new ControllerDescriptor(info.ModelType, info.Layer), info);
			var viewsSvc = _provider.GetService<ViewsSvc>();
			if (viewsSvc != null)
				foreach (var info in viewsSvc.GetRegisteredElements())
					if (info.Technology == technology)
						_views.Add(new ViewDescriptor(info.ViewContract, info.Layer), info);
		}

		#region IUIManagerService Members
		public IUIController CreateControllerForModel(object model)
		{
			if (model == null)
				throw new ArgumentNullException("model");

			foreach (var layer in LayersWithDefault())
			{
				var desc = new ControllerDescriptor(model.GetType(), layer);
				if (_controllers.ContainsKey(desc))
					return CreateControllerInstance(_controllers[desc], model);
			}
			throw new ApplicationException("No controllers found for supplied model and layers.");
		}

		public C CreateView<C>() where C : IView
		{
			foreach (var layer in LayersWithDefault())
			{
				var desc = new ViewDescriptor(typeof (C), layer);
				if (_views.ContainsKey(desc))
					return (C)CreateViewInstance(_views[desc]);
			}
			throw new ApplicationException("No views found for supplied contract and layers.");
		}

		private IView CreateViewInstance(ViewInfo info)
		{
			return (IView)info.ViewType.CreateInstance(_provider);
		}

		private IUIController CreateControllerInstance(UIControllerInfo info, object model)
		{
			var ci = info.ControllerType.GetConstructor(
				BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null,
				new[] {typeof (IServiceProvider), model.GetType()}, null);
			if (ci == null)
				throw new ArgumentException("Appropriate constructor not found.");
			return (IUIController) ci.Invoke(new[] {_provider, model});
		}

		private IEnumerable<string> LayersWithDefault()
		{
			foreach (var layer in _layers)
				yield return layer;
			yield return "";
		}
		#endregion

		#region ControllerDescriptor class
		private struct ControllerDescriptor
		{
			private readonly Type _modelType;
			private readonly string _layer;

			public ControllerDescriptor(Type modelType, string layer)
			{
				_modelType = modelType;
				_layer = layer ?? "";
			}

			public override bool Equals(object obj)
			{
				if (!(obj is ControllerDescriptor))
					return false;
				ControllerDescriptor controllerDescriptor = (ControllerDescriptor) obj;
				return Equals(_modelType, controllerDescriptor._modelType)
				       && Equals(_layer, controllerDescriptor._layer);
			}

			public override int GetHashCode()
			{
				return _modelType.GetHashCode() + 29*_layer.GetHashCode();
			}
		}
		#endregion

		#region ViewDescriptor class
		private struct ViewDescriptor
		{
			private readonly Type _contract;
			private readonly string _layer;

			public ViewDescriptor(Type contract, string layer)
			{
				_contract = contract;
				_layer = layer;
			}

			public override bool Equals(object obj)
			{
				if (!(obj is ViewDescriptor))
					return false;
				ViewDescriptor viewDescriptor = (ViewDescriptor) obj;
				return Equals(_contract, viewDescriptor._contract)
					&& Equals(_layer, viewDescriptor._layer);
			}

			public override int GetHashCode()
			{
				return _contract.GetHashCode() + 29*_layer.GetHashCode();
			}
		}
		#endregion
	}
}
