using System.Windows;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Data.Steam.Fields;

namespace SteamInventoryServiceTool.Windows
{
    /// <summary>
    /// Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow : Window
    {
        private ItemPreviewPage _previewPage;

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

        public void UpdatePreview()
        {
            var item = GetItem();
            _previewPage.UpdateItem(item);
        }

        private Item GetItem()
        {
            var backColor = ColorPickerBackground.SelectedColor.Value;
            var textColor = ColorPickerName.SelectedColor.Value;
            var item = new Item(0)
            {
                Name = NameTextBox.Text,
                Description = DescriptionTextBox.Text,
                BackgroundColor = new HexColor(backColor.R, backColor.G, backColor.B),
                NameColor = new HexColor(textColor.R, textColor.G, textColor.B),
                IconUrl = IconUrlTextBox.Text,
                IconUrlLarge = IconUrlLargeTextBox.Text
            };
            return item;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            UpdatePreview();
        }
    }
}
