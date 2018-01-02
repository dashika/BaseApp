using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views.InputMethods;
using BaseApp.CustomRenderers;
using BaseApp.Droid;
using BaseApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchPage), typeof(SearchPageRenderer))]

namespace BaseApp.Droid
{
    public class SearchPageRenderer : ViewRenderer
    {
        private SearchView _searchView;
        private Toolbar _toolbar;


        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            if (e?.NewElement == null || e.OldElement != null) return;
            AddSearchToToolBar();
        }

        private void AddSearchToToolBar()
        {
            var mainactivity = Forms.Context as MainActivity;
            _toolbar = mainactivity?.FindViewById<Toolbar>(Resource.Id.toolbar);
            if (_toolbar != null)
            {
                _toolbar.Title = "Kitty";
                _toolbar.InflateMenu(Resource.Menu.MainMenu);
                _searchView = _toolbar.Menu?.FindItem(Resource.Id.action_search)?.ActionView?.JavaCast<SearchView>();

                if (_searchView != null) _searchView.QueryTextChange += OnQueryTextChangeSearchView;
                MessagingCenter.Subscribe<SearchingPage>(this, "Remove", (sender) =>
                {
                    _toolbar.Menu.Clear();
                    Dispose(true);
                    MessagingCenter.Unsubscribe<SearchingPage>(this,"Remove");

                });
            }

            if (_searchView == null) return;
            _searchView.QueryTextChange += SearchView_QueryTextChange;
            _searchView.QueryTextSubmit += SearchView_QueryTextSubmit;
            _searchView.QueryHint = (Element as SearchPage)?.SearchPlaceHolderText;
            _searchView.ImeOptions = (int)ImeAction.Search;
            _searchView.InputType = (int)InputTypes.TextVariationNormal;
            _searchView.MaxWidth = int.MaxValue;
        }

        private static void OnQueryTextChangeSearchView(object sender, SearchView.QueryTextChangeEventArgs e)
        {

        }

        private void SearchView_QueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
        {
            if (e == null) return;

            if (!(Element is SearchPage searchPage)) return;
            searchPage.SearchText = e.Query;
            searchPage.SearchCommand?.Execute(e.Query);
            e.Handled = true;
        }

        private void SearchView_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            if (!(Element is SearchPage searchPage)) return;
            searchPage.SearchText = e?.NewText;
        }

        protected override void Dispose(bool disposing)
        {
            _toolbar?.Menu?.RemoveItem(Resource.Menu.MainMenu);
            base.Dispose(disposing);
        }
    }
}