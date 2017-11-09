
using Xamarin.Forms;

namespace BaseApp.Controls
{
	public partial class ItemBar
	{
		public static readonly BindableProperty IsVisibleItemProperty = BindableProperty.Create(nameof(IsVisibleItem), typeof(bool), typeof(ItemBar), false);

		public bool IsVisibleItem
		{
			get { return (bool)GetValue(IsVisibleItemProperty); }
			set
			{
				SetValue(IsVisibleItemProperty, value);
				OnPropertyChanged(nameof(IsVisibleItem));
			}
		}

		public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(ItemBar), default(string));

		public string Icon
		{
			get { return (string)GetValue(IconProperty); }
			set
			{
				SetValue(IconProperty, value);
				OnPropertyChanged(nameof(Icon));
			}
		}

		public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(ItemBar), default(string));

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set
			{
				SetValue(TextProperty, value);
				OnPropertyChanged("Text");
			}
		}

		public bool IsIconVisible
		{
			get { return !string.IsNullOrEmpty(Icon); }
		}

		public bool IsTextVisible
		{
			get { return !string.IsNullOrEmpty(Text); }
		}

		private void Init()
		{
			InitializeComponent();
		}

		public ItemBar()
		{
			Init();
		}

		public ItemBar(string icon, string text = "")
		{
			Init();
			Icon = icon;
			Text = text;
		}

		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == nameof(IsVisibleItem))
			{
				IsVisible = IsVisibleItem;
			}
			else if (propertyName == nameof(Icon))
			{
				Image.Source = Icon;
				Image.IsVisible = IsIconVisible;
				ImageLayout.IsVisible = IsIconVisible;
			}
			else if (propertyName == nameof(Text))
			{
				Label.Text = Text;
				Label.IsVisible = IsTextVisible;
				ForceLayout();

			}
		}
	}
}