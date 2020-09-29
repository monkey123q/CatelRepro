using Catel.Data;
using Catel.MVVM;
using NUnit.Framework;

namespace Repro
{
	[TestFixture]
	internal class TestFixture
	{
		#region Public
		[Test]
		public void ViewModelInit_DoesNotThrows()
		{
			Assert.DoesNotThrow(() =>
			{
				var dogModel = new DogModel
				{
					Name = "name"
				};
				_ = new DogViewModel(dogModel);
			});
		}
		#endregion
	}

	public abstract class AnimalModelBase : ModelBase
	{
	}

	public class DogModel : AnimalModelBase
	{
		#region Data
		#region Static
		public static readonly PropertyData NameProperty = RegisterProperty(nameof(Name), typeof(string));
		#endregion
		#endregion

		#region Properties
		public string Name
		{
			get => GetValue<string>(NameProperty);
			set => SetValue(NameProperty, value);
		}
		#endregion
	}

	public abstract class AnimalViewModelBase : ViewModelBase
	{
		#region Data
		#region Static
		public static readonly PropertyData AnimalProperty = RegisterProperty(nameof(Animal), typeof(AnimalModelBase));
		#endregion
		#endregion

		#region .ctor
		public AnimalViewModelBase(AnimalModelBase model)
		{
			Animal = model;
		}
		#endregion

		#region Properties
		[Model]
		public AnimalModelBase Animal
		{
			get => GetValue<AnimalModelBase>(AnimalProperty);
			set => SetValue(AnimalProperty, value);
		}
		#endregion
	}

	public class DogViewModel : AnimalViewModelBase
	{
		#region Data
		#region Static
		public static readonly PropertyData NameProperty = RegisterProperty(nameof(Name), typeof(string));
		#endregion
		#endregion

		#region .ctor
		public DogViewModel(DogModel model)
			: base(model)
		{
		}
		#endregion

		#region Properties
		[ViewModelToModel(nameof(Animal), nameof(DogModel.Name))]
		public string Name
		{
			get => GetValue<string>(NameProperty);
			set => SetValue(NameProperty, value);
		}
		#endregion
	}
}
