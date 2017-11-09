using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BaseApp
{
	public partial class MainPage 
	{
		public MainViewModel Model;

		public MainPage()
		{
			InitializeComponent();

			Model = BindingContext as MainViewModel;
			if (null == Model) return;
			Model.View = this;
		}
	}
}
