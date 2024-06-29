using System;
using System.Windows;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Data.Steam.Misc;
using SteamInventoryServiceTool.Windows.ListViews;

namespace SteamInventoryServiceTool.Windows.Dialogs;

public partial class SelectPromoDialogWindow : Window
{
    public Item Item { get; }

    private SelectPromoListViewHandler _promoListHandler;
    
    public SelectPromoDialogWindow(Item item)
    {
        InitializeComponent();

        Item = item;
        _promoListHandler = new SelectPromoListViewHandler(PromoListView, Item);

        AddPromoButton.Click += AddPromo;
        CloseButton.Click += CloseDialog;
        IsManualCheckBox.Click += OnIsManualClicked;
        
        UpdateComboBox();
        OnIsManualClicked(null, null);
    }

    private void OnIsManualClicked(object sender, RoutedEventArgs e)
    {
        var isChecked = IsManualCheckBox.IsChecked.Value;
        PromoTypeComboBox.IsEnabled = !isChecked;
        PromoValueTextBox.IsEnabled = !isChecked;
        PromoListView.IsEnabled = !isChecked;
        AddPromoButton.IsEnabled = !isChecked;
        if (isChecked)
        {
            Item.Promo.PromoRequirements.Add(new ManualPromoRequirement());
        }
        else
        {
            Item.Promo.PromoRequirements.RemoveAll(x => x.Type == PromoType.Manual);
        }
    }

    private void UpdateComboBox()
    {
        PromoTypeComboBox.ItemsSource = Enum.GetNames<PromoType>();
        PromoTypeComboBox.SelectedIndex = 0;
    }

    private void AddPromo(object sender, RoutedEventArgs e)
    {
        var promoType = (PromoType)PromoTypeComboBox.SelectedIndex;
        var promoValue = PromoValueTextBox.Text;

        switch (promoType)
        {
            case PromoType.OwnApp:
                AddPromoOwnApp(promoValue);
                break;
            case PromoType.OwnAchievement:
                AddPromoOwnAchievement(promoValue);
                break;
            case PromoType.PlayerTime:
                AddPromoPlayerTime(promoValue);
                break;
            case PromoType.Manual:
                SetPromoManual();
                break;
        }
    }

    private void AddPromoOwnApp(string promoValue)
    {
        if (string.IsNullOrWhiteSpace(promoValue))
        {
            MessageBox.Show("Value cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (!int.TryParse(promoValue, out var intValue))
        {
            MessageBox.Show("Value must be numeric", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Item.Promo.PromoRequirements.Add(new OwnAppPromoRequirement(intValue));
        _promoListHandler.Update();
    }

    private void AddPromoOwnAchievement(string promoValue)
    {
        if (string.IsNullOrWhiteSpace(promoValue))
        {
            MessageBox.Show("Value cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Item.Promo.PromoRequirements.Add(new OwnAchievementPromoRequirement(promoValue));
        _promoListHandler.Update();
    }

    private void AddPromoPlayerTime(string promoValue)
    {
        if (string.IsNullOrWhiteSpace(promoValue))
        {
            MessageBox.Show("Value cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var separated = promoValue.Split('/');
        if (separated.Length != 2 || !int.TryParse(separated[0], out var appId) || !int.TryParse(separated[1], out var minutes))
        {
            MessageBox.Show("Player time should be in format [appid/minutes]", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Item.Promo.PromoRequirements.Add(new PlayerTimePromoRequirement(appId, minutes));
        _promoListHandler.Update();
    }

    private void SetPromoManual()
    {
        IsManualCheckBox.IsChecked = true;
        OnIsManualClicked(null, null);
    }


    private void CloseDialog(object sender, RoutedEventArgs e)
    {
        Close();
    }
}