using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseApp.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchingPage : ContentPage
    {
        public SearchingPage()
        {
            InitializeComponent();
        }

        protected override async  void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(500);
            var content = this.Content as StackLayout;
            var hasSearch = false;
            if (content == null) return;
            foreach (var child in content.Children)
            {
                if (child is SearchPage)
                {
                    hasSearch = true;
                }
            }

            if (!hasSearch)
                content.Children.Add(new SearchPage());
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var content = this.Content as StackLayout;
            foreach (var child in content.Children)
            {
                if (child is SearchPage searchpage)
                {
                    content.Children.Remove(searchpage);
                    MessagingCenter.Send<SearchingPage>(this, "Remove");
                    return;
                }
            }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            OnDisappearing();

            Navigation.PushAsync(new DetailPage());
        }
    }
}