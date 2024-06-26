using System.Windows;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Data.Steam.Fields;
using SteamInventoryServiceTool.Workspace;

namespace SteamInventoryServiceTool.Windows
{
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

        public ItemWindow()
        {
            InitializeComponent();
            CreateItemPreview();
        }

        private void CreateItemPreview()
        {
            _previewPage = new ItemPreviewPage();
            this.PreviewFrame.Navigate(_previewPage);
        }
        
        public void ShowAsNewItem()
        {
            SetOpenType(ItemOpenType.New);
            ShowDialog();
            var itemId = new Item(WorkspaceManager.Instance.ActiveWorkspace.GetNextItemId());
            FillItem(itemId);
        }

        public void ShowAsEditItem(Item item)
        {
            SetOpenType(ItemOpenType.Edit);
            ShowDialog();
            FillItem(item);
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
            
        }

        private void AddNCloseButtonOnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void CloseButtonOnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RefreshButtonOnClick(object sender, RoutedEventArgs e)
        {
            UpdatePreview();
        }

        //private void 

        #region ItemControl
        private void FillItem(Item item)
        {
            IdTextBox.Text = item.Id.ToString();
            NameTextBox.Text = item.Name;
            // itemtype
            DescriptionTextBox.Text = item.Description;
            DisplayTypeTextBox.Text = item.DisplayType;
            MarketableCheckBox.IsChecked = item.Marketable;
            TradableCheckBox.IsChecked = item.Tradable;
            ColorPickerBackground.SelectedColor = item.BackgroundColor.Color;
            ColorPickerName.SelectedColor = item.NameColor.Color;
            IconUrlTextBox.Text = item.IconUrl;
            IconUrlLargeTextBox.Text = item.IconUrlLarge;
            // pricecombobox
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
                // itemtype
                Description = DescriptionTextBox.Text,
                DisplayType = DisplayTypeTextBox.Text,
                Marketable = MarketableCheckBox.IsChecked!.Value,
                Tradable = TradableCheckBox.IsChecked!.Value,
                BackgroundColor = new HexColor(backColor.R, backColor.G, backColor.B),
                NameColor = new HexColor(textColor.R, textColor.G, textColor.B),
                IconUrl = IconUrlTextBox.Text,
                IconUrlLarge = IconUrlLargeTextBox.Text,
                // price
                // tags
                GameOnly = GameOnlyCheckBox.IsChecked!.Value,
                Hidden = HiddenCheckBox.IsChecked!.Value,
                StoreHidden = StoreHiddenCheckBox.IsChecked!.Value,
                GrantedManually = GrantedManuallyCheckBox.IsChecked!.Value,
                UseBundlePrice = UseBundlePriceCheckBox.IsChecked!.Value,
                AutoStack = AutoStackCheckBox.IsChecked!.Value,
                UseDropLimit = UseDropLimitCheckBox.IsChecked!.Value,
                DropInterval = int.Parse(DropIntervalTextBox.Text),
                UseDropWindow = UseDropWindowCheckBox.IsChecked!.Value,
                DropMaxPerWindow = int.Parse(DropPerWindowTextBox.Text)
            };
            return item;
        }

        private void UpdatePreview()
        {
            var item = GetItem();
            _previewPage.UpdateItem(item);
        }
        #endregion
    }
}
