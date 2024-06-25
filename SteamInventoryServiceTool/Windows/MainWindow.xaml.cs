using SteamInventoryServiceTool.Windows.ToolMenuSetup;
using System.Windows;

namespace SteamInventoryServiceTool.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		private MainWindowToolMenu _toolMenu;
		private ItemPreviewPage _previewPage;

		public MainWindow()
		{
			InitializeComponent();

			CreateItemPreview();
			SetupToolMenu();

			var testWindow = new ItemWindow();
			testWindow.Show();
		}

		private void CreateItemPreview()
		{
            _previewPage = new ItemPreviewPage();
            this.PreviewFrame.Navigate(_previewPage);
        }
		
		private void SetupToolMenu()
		{
			_toolMenu = new MainWindowToolMenu(this, ToolMenu);
		}
    }
}
