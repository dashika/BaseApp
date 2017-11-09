using Xamarin.Forms;

namespace BaseApp.Views.Templates
{
	public class BaseMainPagetemplate : StackLayout
	{
		public static readonly BindableProperty ParentContentPageProperty = BindableProperty.Create(nameof(ParentContentPage), typeof(ContentPage), typeof(MainPageTemplate), default(ContentPage));

		public ContentPage ParentContentPage
		{
			get { return (ContentPage)GetValue(ParentContentPageProperty); }
			set
			{
				SetValue(ParentContentPageProperty, value);
			}
		}

	}
}
