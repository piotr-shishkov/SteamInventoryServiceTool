using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Data.Steam.Misc;

namespace SteamInventoryServiceTool.Windows.ListViews;

public class SelectPromoListViewHandler
{
    private ListView _listView;
    private Item _item;

    public SelectPromoListViewHandler(ListView listView, Item item)
    {
        _listView = listView;
        _item = item;
        
        _listView.KeyDown += OnListKeyDown;
        Update();
    }

    private void OnListKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete)
        {
            var selectedItems = _listView.SelectedItems.Cast<PromoRequirement>().ToList();
            if (selectedItems.Any())
            {
                var result = MessageBox.Show(
                    $"Are you sure want to delete {selectedItems.Count} tag(s)?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var item in selectedItems)
                    {
                        _item.Promo.PromoRequirements.Remove(item);
                    }
                }
                Update();
            }
        }
    }

    public void Update()
    {
        FillItemPromos();
    }

    private void FillItemPromos()
    {
        _listView.ItemsSource = null;
        _listView.ItemsSource = _item.Promo.PromoRequirements.ToList();
    }
}