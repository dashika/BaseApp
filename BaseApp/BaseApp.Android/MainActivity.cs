using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace BaseApp.Droid
{
	[Activity(Label = "BaseApp", Icon = "@drawable/Nerd_Cat_icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new App());
		}

		public override async void OnBackPressed()
		{
			bool? result = await App.CallHardwareBackPressed();
			if (result == true)
			{
				base.OnBackPressed();
			}
			else if (result == null)
			{
				Finish();
			}
		}
	}
}

