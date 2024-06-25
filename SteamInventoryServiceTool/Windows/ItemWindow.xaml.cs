using System.Windows;

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
    }
}
