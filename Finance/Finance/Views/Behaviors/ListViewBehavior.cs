using Finance.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Finance.Views.Behaviors
{
    public class ListViewBehavior : Behavior<ListView>
    {
        ListView listView;
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            listView = bindable;
            listView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Item selectedItem = (listView.SelectedItem) as Item;   //converted the listView.SelectedItem to Item and store in selectedItem variable
            // when the item is selected, you want to navigate to the selected item post page
            // at this time, you need to create another page within Views called PostPage, which is the detailed page.
            // this will be just a simple webview
            Application.Current.MainPage.Navigation.PushAsync(new PostPage(selectedItem));
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            // detach the list view
            listView.ItemSelected -= ListView_ItemSelected;
        }
    }
}
