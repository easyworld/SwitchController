using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace SwitchController;

/// <summary>
/// SettingsWindow.xaml 的交互逻辑
/// </summary>
public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
        LoadFromSettings();
    }

    private void LoadFromSettings()
    {
        var s = Properties.Settings.Default;

        tbIp.Text = s.SwitchIP ?? "192.168.0.0";
        tbPort.Text = s.SwitchPort > 0 ? s.SwitchPort.ToString() : "6000";

        // 拉流模式：0=持续拉流  1=操作预览  2=关闭预览
        cbStreamMode.SelectedIndex = Math.Clamp(s.StreamMode, 0, 2);

        tbFps.Text = (s.TargetFps > 0 ? s.TargetFps : 20).ToString();
        tbPreviewSecs.Text = (s.PreviewSecs > 0 ? s.PreviewSecs : 2).ToString();

        tbDeadZone.Text = (s.DeadZonePx > 0 ? s.DeadZonePx : 8).ToString();
        tbSendInterval.Text = (s.SendIntervalMs > 0 ? s.SendIntervalMs : 15).ToString();

        chkAutoConnect.IsChecked = s.AutoConnect;
        // 主题：0=深色  1=浅色  2=传说Z-A限定
        cbTheme.SelectedIndex = s.ThemeName == "Dark" ? 0 : s.ThemeName == "Light" ? 1 : s.ThemeName == "ZA" ? 2 : 2;

    }

    private bool SaveToSettings()
    {
        var ip = tbIp.Text.Trim();
        var port = tbPort.Text.Trim();

        // 简单校验
        if (!Regex.IsMatch(ip, @"^(?:(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d\d?)$"))
        {
            MessageBox.Show("IP 地址格式不正确。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (!int.TryParse(port, out int p) || p <= 0 || p > 65535)
        {
            MessageBox.Show("端口必须是 1~65535 的整数。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        // 新值
        int mode = cbStreamMode.SelectedIndex; 
        int fps = ParsePositive(tbFps.Text, 20);
        int previewSecs = ParsePositive(tbPreviewSecs.Text, 2);
        int deadZone = ParsePositive(tbDeadZone.Text, 8);
        int sendInterval = ParsePositive(tbSendInterval.Text, 15);
        bool autoConnect = chkAutoConnect.IsChecked == true;
        string newTheme = (cbTheme.SelectedIndex == 2) ? "ZA" : (cbTheme.SelectedIndex == 1) ? "Light" : "Dark";

        // 旧值
        var s = Properties.Settings.Default;
        string oldIP = s.SwitchIP ?? "";
        int oldPort = s.SwitchPort;
        int oldMode = s.StreamMode;
        int oldFps = s.TargetFps;
        int oldPreview = s.PreviewSecs;
        int oldDeadZone = s.DeadZonePx;
        int oldInterval = s.SendIntervalMs;
        bool oldAuto = s.AutoConnect;
        string oldTheme = s.ThemeName ?? "Dark";

        // 比对是否有变化
        bool ipChanged = !string.Equals(oldIP, ip, StringComparison.OrdinalIgnoreCase);
        bool portChanged = oldPort != p;
        bool modeChanged = oldMode != mode;
        bool fpsChanged = oldFps != fps;
        bool previewChanged = oldPreview != previewSecs;
        bool deadZoneChanged = oldDeadZone != deadZone;
        bool intervalChanged = oldInterval != sendInterval;
        bool autoChanged = oldAuto != autoConnect;
        bool themeChanged = !string.Equals(oldTheme, newTheme, StringComparison.OrdinalIgnoreCase);

        bool anyChanged = ipChanged || portChanged || modeChanged || fpsChanged ||
                          previewChanged || deadZoneChanged || intervalChanged ||
                          autoChanged || themeChanged;

        // 没变化就直接返回
        if (!anyChanged)
            return true;

        // 写入新值
        s.SwitchIP = ip;
        s.SwitchPort = p;
        s.StreamMode = mode;
        s.TargetFps = fps;
        s.PreviewSecs = previewSecs;
        s.DeadZonePx = deadZone;
        s.SendIntervalMs = sendInterval;
        s.AutoConnect = autoConnect;
        s.ThemeName = newTheme;
        s.Save();

        if (themeChanged || modeChanged)
        {
            RestartApp();
            return true;
        }
        return true;
    }


    private static void RestartApp()
    {
        try
        {
            var exe = Process.GetCurrentProcess().MainModule?.FileName;
            if (!string.IsNullOrEmpty(exe))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = exe,
                    UseShellExecute = true
                });
            }
        }
        catch
        {
        }
        finally
        {
            Application.Current.Shutdown();
        }
    }

    private static int ParsePositive(string? txt, int fallback)
        => int.TryParse(txt, out var v) && v > 0 ? v : fallback;

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        if (SaveToSettings())
        {
            DialogResult = true;
            Close();
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        tbIp.Text = "192.168.0.0";
        tbPort.Text = "6000";
        cbStreamMode.SelectedIndex = 1;
        tbFps.Text = "20";
        tbPreviewSecs.Text = "2";
        tbDeadZone.Text = "8";
        tbSendInterval.Text = "15";
        chkAutoConnect.IsChecked = false;
        cbTheme.SelectedIndex = 0;
        RestartApp();
    }

    private void Close_Click(object sender, RoutedEventArgs e) => Cancel_Click(sender, e);

}
