using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.Services
{
    internal class DialogService
    {
        public static async Task OpenDialog(XamlRoot xamlRoot,string title, string content, string closeButtonText)
        {
            var dialog = new ContentDialog()
            {
                Title = title,
                Content = content,
                CloseButtonText = closeButtonText,
                XamlRoot = xamlRoot
            };

            await dialog.ShowAsync();
        }

    }
}
