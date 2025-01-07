using FrisanIonutLab7.Models;
using System.Diagnostics;
namespace FrisanIonutLab7;

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnDeleteItemButonClicked(object sender, EventArgs e)
    { 
        if (listView.SelectedItem != null)
        {
            var slist = (ShopList)BindingContext;
            var p = listView.SelectedItem as Product;
            var products = await App.Database.GetListProductsAsync(slist.ID);
            var product = products.Where(lp => lp.ID == p.ID).FirstOrDefault();
            
            if (product != null)
            {
                await App.Database.DeleteListProductAsync(slist.ID, product.ID);
                listView.ItemsSource = await App.Database.GetListProductsAsync(slist.ID);
            }
        }
        Debug.WriteLine("*****");
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
        {
            BindingContext = new Product()
        });
    }
}