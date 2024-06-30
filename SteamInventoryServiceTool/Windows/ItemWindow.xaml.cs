using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Data.Steam.Fields;
using SteamInventoryServiceTool.Windows.Dialogs;
using SteamInventoryServiceTool.Workspaces;
using WebUtility = SteamInventoryServiceTool.Utility.WebUtility;

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
    private DispatcherTimer _disptacherTimer;

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
        TypeComboBox.SelectionChanged += OnTypeComboBoxSelectionChanged;
        TypeComboBox.ItemsSource = types;
        TypeComboBox.SelectedIndex = 0;

        var prices = Enum.GetNames(typeof(PriceCategories));
        for (var i = 0; i < prices.Length; i++)
        {
            prices[i] += $" => {PriceCategory.PriceToString((PriceCategories)i)}";
        }
        PriceComboBox.ItemsSource = prices;
        PriceComboBox.SelectedIndex = 0;
        
        TagsEditButton.Click += OpenTagEditor;
        BundleEditButton.Click += OpenBundleEditor;
        PromoEditButton.Click += OpenPromoEditor;

        AutoRefreshCheckBox.IsChecked = true;
        AutoRefreshCheckBox.Click += AutoRefreshClicked;

        _disptacherTimer = new DispatcherTimer();
        _disptacherTimer.Tick += OnTimerTick;
        _disptacherTimer.Interval = TimeSpan.FromMilliseconds(500);
        
        AddButton.Click += AddButtonOnClick;
        AddNCloseButton.Click += AddNCloseButtonOnClick;
        CloseButton.Click += CloseButtonOnClick;
        RefreshButton.Click += RefreshButtonOnClick;
        
        DropHelpHyperLink.RequestNavigate += OnDropHelpNavigateRequest;
        
        ToggleAutoRefresh();
    }

    private void OnTypeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentType = (ItemType)TypeComboBox.SelectedIndex;
        BundleEditButton.IsEnabled = currentType != ItemType.Item;
    }

    private void OpenTagEditor(object sender, RoutedEventArgs e)
    {
        var tagSelector = new SelectTagsDialogWindow(GetItem());
        tagSelector.ShowDialog();
        FillItem(tagSelector.Item);
    }

    private void OpenBundleEditor(object sender, RoutedEventArgs e)
    {
        var bundleSelector = new SelectBundleDialogWindow(GetItem());
        bundleSelector.ShowDialog();
        FillItem(bundleSelector.Item);
    }

    private void OpenPromoEditor(object sender, RoutedEventArgs e)
    {
        var promoSelector = new SelectPromoDialogWindow(GetItem());
        promoSelector.ShowDialog();
        FillItem(promoSelector.Item);
    }

    private void OnDropHelpNavigateRequest(object sender, RequestNavigateEventArgs e)
    {
        WebUtility.OpenDropTimeDocumentation();
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
            AddButton.Content = "Add & New";
            AddButton.ToolTip = "Adds item to workspace and you can continue on new one based on current.";
            AddNCloseButton.Content = "Add Item";
            AddNCloseButton.ToolTip = "Adds item to workspace and closes window.";
        }
        else if (type == ItemOpenType.Edit)
        {
            AddButton.Content = "Save & New";
            AddButton.ToolTip = "Saves item and you can continue creating new one based on current.";
            AddNCloseButton.Content = "Save Item";
            AddButton.ToolTip = "Saves item and closes window.";
        }
    }

    private void AddButtonOnClick(object sender, RoutedEventArgs e)
    {
        var item = GetItem();
        if (string.IsNullOrWhiteSpace(item.Name))
        {
            MessageBox.Show("Item name could not be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        AddCurrentItem();

        item.Id = _workspaceManager.ActiveWorkspace.GetNextItemId();
        item.Name = "";
        
        if (_openType == ItemOpenType.Edit)
        {
            SetOpenType(ItemOpenType.New);
        }
        FillItem(item);
    }

    private void AddNCloseButtonOnClick(object sender, RoutedEventArgs e)
    {
        var item = GetItem();
        if (string.IsNullOrWhiteSpace(item.Name))
        {
            MessageBox.Show("Item name could not be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
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
        TagsTextBox.Text = item.Tags.ToString();
        BundleTextBox.Text = item.Bundle.ToString();
        PromoTextBox.Text = item.Promo.ToString();
        GameOnlyCheckBox.IsChecked = item.GameOnly;
        HiddenCheckBox.IsChecked = item.Hidden;
        StoreHiddenCheckBox.IsChecked = item.StoreHidden;
        GrantedManuallyCheckBox.IsChecked = item.GrantedManually;
        UseBundlePriceCheckBox.IsChecked = item.UseBundlePrice;
        AutoStackCheckBox.IsChecked = item.AutoStack;
        UseDropLimitCheckBox.IsChecked = item.UseDropLimit;
        DropLimitTextBox.Text = item.DropLimit.ToString();
        DropIntervalTextBox.Text = item.DropInterval.ToString();
        UseDropWindowCheckBox.IsChecked = item.UseDropWindow;
        DropPerWindowTextBox.Text = item.DropMaxPerWindow.ToString();
    }

    private Item GetItem()
    {
        var backColor = ColorPickerBackground.SelectedColor!.Value;
        var textColor = ColorPickerName.SelectedColor!.Value;
        var id = int.Parse(IdTextBox.Text);
        var type = (ItemType)TypeComboBox.SelectedIndex;
        
        var item = new Item(id)
        {
            Name = NameTextBox.Text,
            Type = type,
            Description = DescriptionTextBox.Text,
            DisplayType = DisplayTypeTextBox.Text,
            Marketable = MarketableCheckBox.IsChecked!.Value,
            Tradable = TradableCheckBox.IsChecked!.Value,
            BackgroundColor = new HexColor(backColor.R, backColor.G, backColor.B),
            NameColor = new HexColor(textColor.R, textColor.G, textColor.B),
            IconUrl = IconUrlTextBox.Text,
            IconUrlLarge = IconUrlLargeTextBox.Text,
            PriceCategory = new PriceCategory((PriceCategories)PriceComboBox.SelectedIndex),
            Tags = new Tags(TagsTextBox.Text),
            Bundle = type == ItemType.Item ? new Bundle() : new Bundle(BundleTextBox.Text),
            Promo = new Promo(PromoTextBox.Text),
            GameOnly = GameOnlyCheckBox.IsChecked!.Value,
            Hidden = HiddenCheckBox.IsChecked!.Value,
            StoreHidden = StoreHiddenCheckBox.IsChecked!.Value,
            GrantedManually = GrantedManuallyCheckBox.IsChecked!.Value,
            UseBundlePrice = UseBundlePriceCheckBox.IsChecked!.Value,
            AutoStack = AutoStackCheckBox.IsChecked!.Value,
            UseDropLimit = UseDropLimitCheckBox.IsChecked!.Value,
            UseDropWindow = UseDropWindowCheckBox.IsChecked!.Value,
        };
        
        if(int.TryParse(DropIntervalTextBox.Text, out var dropLimitValue))
        {
            item.DropLimit = dropLimitValue;
        }
        
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
    
    private void AutoRefreshClicked(object sender, RoutedEventArgs e)
    {
        ToggleAutoRefresh();
    }

    private async void ToggleAutoRefresh()
    {
        var autoRefreshEnabled = AutoRefreshCheckBox.IsChecked.Value;

        RefreshButton.IsEnabled = !autoRefreshEnabled;
        if (autoRefreshEnabled)
        {   
            _disptacherTimer.Start();
        }
        else
        {
            _disptacherTimer.Stop();
        }
    }
    
    
    private void OnTimerTick(object? sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void UpdatePreview()
    {
        var item = GetItem();
        _previewPage.UpdateItem(item);
        Title = $"{item.Type} - {item.Name}";
    }
    #endregion
}