using System;
using System.Windows;
using System.Windows.Documents;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Data.Steam.Fields;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows;

public enum ItemOpenType
{
    New,
    Edit
}
    
/// <summary>
/// Interaction logic for ItemWindow.xaml
/// </summary>
public partial class ItemWindow : Window
{
    private ItemPreviewPage _previewPage;
    private ItemOpenType _openType;
    private WorkspaceManager _workspaceManager;

    public ItemWindow()
    {
        _workspaceManager = WorkspaceManager.Instance;
            
        InitializeComponent();
        SetupElements();
        CreateItemPreview();
    }

    private void SetupElements()
    {
        var types = Enum.GetNames(typeof(ItemType));
        TypeComboBox.ItemsSource = types;
        TypeComboBox.SelectedIndex = 0;
            
        var prices = Enum.GetNames(typeof(PriceCategories));
        for (var i = 0; i < prices.Length; i++)
        {
            prices[i] += $" => {PriceCategory.PriceToString((PriceCategories)i)}";
        }
        PriceComboBox.ItemsSource = prices;
        PriceComboBox.SelectedIndex = 0;
    }

    private void CreateItemPreview()
    {
        _previewPage = new ItemPreviewPage();
        this.PreviewFrame.Navigate(_previewPage);
    }
        
    public void ShowAsNewItem()
    {
        SetOpenType(ItemOpenType.New);
        var item = GetItem();
        item.Id = _workspaceManager.ActiveWorkspace.GetNextItemId();
        FillItem(item);
        ShowDialog();
    }

    public void ShowAsEditItem(Item item)
    {
        SetOpenType(ItemOpenType.Edit);
        FillItem(item);
        ShowDialog();
    }

    private void SetOpenType(ItemOpenType type)
    {
        _openType = type;
        SetButtons(type);
    }

    private void SetButtons(ItemOpenType type)
    {
        if (type == ItemOpenType.New)
        {
            AddButton.Content = "Add Item";
            AddNCloseButton.Content = "Add & Close";
        }
        else if (type == ItemOpenType.Edit)
        {
            AddButton.Content = "Save & New Item";
            AddNCloseButton.Content = "Save & Close";
        }
            
        AddButton.Click += AddButtonOnClick;
        AddNCloseButton.Click += AddNCloseButtonOnClick;
        CloseButton.Click += CloseButtonOnClick;
        RefreshButton.Click += RefreshButtonOnClick;
    }

    private void AddButtonOnClick(object sender, RoutedEventArgs e)
    {
        AddCurrentItem();
        var item = GetItem();
        item.Id = _workspaceManager.ActiveWorkspace.GetNextItemId();
        FillItem(item);
    }

    private void AddNCloseButtonOnClick(object sender, RoutedEventArgs e)
    {
        AddCurrentItem();
        Close();
    }

    private void CloseButtonOnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void RefreshButtonOnClick(object sender, RoutedEventArgs e)
    {
        UpdatePreview();
    }

    private void AddCurrentItem()
    {
        var item = GetItem();
        _workspaceManager.ActiveWorkspace.AddItem(item);
    }

    #region ItemControl
    private void FillItem(Item item)
    {
        IdTextBox.Text = item.Id.ToString();
        NameTextBox.Text = item.Name;
        TypeComboBox.SelectedIndex = (int)item.Type;
        DescriptionTextBox.Text = item.Description;
        DisplayTypeTextBox.Text = item.DisplayType;
        MarketableCheckBox.IsChecked = item.Marketable;
        TradableCheckBox.IsChecked = item.Tradable;
        ColorPickerBackground.SelectedColor = item.BackgroundColor.Color;
        ColorPickerName.SelectedColor = item.NameColor.Color;
        IconUrlTextBox.Text = item.IconUrl;
        IconUrlLargeTextBox.Text = item.IconUrlLarge;
        PriceComboBox.SelectedIndex = (int)item.PriceCategory.Category;
        // TagsTextBox.Text
        GameOnlyCheckBox.IsChecked = item.GameOnly;
        HiddenCheckBox.IsChecked = item.Hidden;
        StoreHiddenCheckBox.IsChecked = item.StoreHidden;
        GrantedManuallyCheckBox.IsChecked = item.GrantedManually;
        UseBundlePriceCheckBox.IsChecked = item.UseBundlePrice;
        AutoStackCheckBox.IsChecked = item.AutoStack;
        UseDropLimitCheckBox.IsChecked = item.UseDropLimit;
        DropIntervalTextBox.Text = item.DropInterval.ToString();
        UseDropWindowCheckBox.IsChecked = item.UseDropWindow;
        DropPerWindowTextBox.Text = item.DropMaxPerWindow.ToString();
    }

    private Item GetItem()
    {
        var backColor = ColorPickerBackground.SelectedColor!.Value;
        var textColor = ColorPickerName.SelectedColor!.Value;
        var id = int.Parse(IdTextBox.Text);
        var item = new Item(id)
        {
            Name = NameTextBox.Text,
            Type = (ItemType)TypeComboBox.SelectedIndex,
            Description = DescriptionTextBox.Text,
            DisplayType = DisplayTypeTextBox.Text,
            Marketable = MarketableCheckBox.IsChecked!.Value,
            Tradable = TradableCheckBox.IsChecked!.Value,
            BackgroundColor = new HexColor(backColor.R, backColor.G, backColor.B),
            NameColor = new HexColor(textColor.R, textColor.G, textColor.B),
            IconUrl = IconUrlTextBox.Text,
            IconUrlLarge = IconUrlLargeTextBox.Text,
            PriceCategory = new PriceCategory((PriceCategories)PriceComboBox.SelectedIndex),
            // tags
            GameOnly = GameOnlyCheckBox.IsChecked!.Value,
            Hidden = HiddenCheckBox.IsChecked!.Value,
            StoreHidden = StoreHiddenCheckBox.IsChecked!.Value,
            GrantedManually = GrantedManuallyCheckBox.IsChecked!.Value,
            UseBundlePrice = UseBundlePriceCheckBox.IsChecked!.Value,
            AutoStack = AutoStackCheckBox.IsChecked!.Value,
            UseDropLimit = UseDropLimitCheckBox.IsChecked!.Value,
            UseDropWindow = UseDropWindowCheckBox.IsChecked!.Value,
        };
            
        if(int.TryParse(DropIntervalTextBox.Text, out var dropIntervalValue))
        {
            item.DropInterval = dropIntervalValue;
        }

        if (int.TryParse(DropPerWindowTextBox.Text, out var dropPerWindowValue))
        {
            item.DropMaxPerWindow = dropPerWindowValue;
        }
            
        return item;
    }

    private void UpdatePreview()
    {
        var item = GetItem();
        _previewPage.UpdateItem(item);
    }
    #endregion
}