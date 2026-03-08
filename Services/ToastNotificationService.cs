using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;
using WebhookToNotification.Services.Interop;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace WebhookToNotification.Services;

public partial class ToastNotificationService
{
    private const string APP_ID = "WebhookToNotification.App";

    public ToastNotificationService()
    {
        RegisterAppForNotifications();
    }

    private void RegisterAppForNotifications()
    {
        try
        {
            // 获取当前可执行文件路径
            string? exePath = Environment.ProcessPath ?? System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName;
            if (string.IsNullOrEmpty(exePath))
            {
                return;
            }

            // 注册 AppUserModelID 到注册表
            string regPath = $@"Software\Classes\AppUserModelId\{APP_ID}";
            using (RegistryKey? key = Registry.CurrentUser.CreateSubKey(regPath))
            {
                if (key is not null)
                {
                    key.SetValue("DisplayName", "Webhook Notification");
                    key.SetValue("IconUri", exePath);
                }
            }

            // 创建开始菜单快捷方式
            CreateStartMenuShortcut(exePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to register app: {ex.Message}");
        }
    }

    private void CreateStartMenuShortcut(string exePath)
    {
        try
        {
            string startMenuPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Windows\Start Menu\Programs");
            string shortcutPath = Path.Combine(startMenuPath, "Webhook Notification.lnk");

            IShellLink shellLink = (IShellLink)new ShellLink();

            shellLink.SetPath(exePath);
            shellLink.SetWorkingDirectory(Path.GetDirectoryName(exePath) ?? "");
            shellLink.SetDescription("Webhook Notification");

            // 设置 AppUserModelID
            IPropertyStore propertyStore = (IPropertyStore)shellLink;
            PropertyKey appIdKey = new(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 5);
            PropVariant appIdValue = new(APP_ID);
            propertyStore.SetValue(ref appIdKey, ref appIdValue);
            propertyStore.Commit();

            IPersistFile persistFile = (IPersistFile)shellLink;
            persistFile.Save(shortcutPath, true);

            Marshal.ReleaseComObject(shellLink);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to create shortcut: {ex.Message}");
        }
    }

    public void ShowToast(string title, string content)
    {
        try
        {
            // 获取图片的绝对路径
            string iconPath = Path.Combine(AppContext.BaseDirectory, "Icon.png");
            string toastXml = $"""
                <toast>
                    <visual>
                        <binding template='ToastGeneric'>
                            <image placement='appLogoOverride' src='file:///{iconPath.Replace("\\", "/")}'/>
                            <text>{SecurityElement.Escape(title)}</text>
                            <text>{SecurityElement.Escape(content)}</text>
                        </binding>
                    </visual>
                    <audio silent='true' />
                </toast>
                """;

            XmlDocument xmlDoc = new();
            xmlDoc.LoadXml(toastXml);

            ToastNotification toast = new(xmlDoc);
            ToastNotifier notifier = ToastNotificationManager.CreateToastNotifier(APP_ID);
            notifier.Show(toast);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to show toast: {ex.Message}");
        }
    }
}