using SwitchController.Properties;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SwitchController
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string theme = Settings.Default.ThemeName;
            if (string.IsNullOrWhiteSpace(theme))
                theme = "Dark"; // 默认主题

            var dicts = Current.Resources.MergedDictionaries;
            dicts.Clear(); // 清除旧主题
            dicts.Add(new ResourceDictionary
            {
                Source = new Uri($"Themes/Theme.{theme}.xaml", UriKind.Relative)
            });
        }
    }

}
