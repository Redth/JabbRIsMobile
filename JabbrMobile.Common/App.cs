using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common.ViewModels;
using Cirrious.CrossCore.IoC;

namespace JabbRIsMobile
{
	public class App : MvxApplication
	{
		public App()
		{
			CreatableTypes()
				.EndingWith("Service")
					.AsInterfaces()
					.RegisterAsLazySingleton();

			RegisterAppStart<HomeViewModel>();
		}
	}
}