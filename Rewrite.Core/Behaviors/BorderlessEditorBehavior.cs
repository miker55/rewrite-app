using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;

namespace Rewrite.Core.Behaviors;

public class BorderlessEditorBehavior : Behavior<Editor>
{
    protected override void OnAttachedTo(Editor editor)
    {
        base.OnAttachedTo(editor);
        editor.HandlerChanged += OnHandlerChanged;
    }

    protected override void OnDetachingFrom(Editor editor)
    {
        editor.HandlerChanged -= OnHandlerChanged;
        base.OnDetachingFrom(editor);
    }

    private void OnHandlerChanged(object? sender, EventArgs e)
    {
        if (sender is Editor editor && editor.Handler is EditorHandler handler)
        {
#if WINDOWS
            handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
            handler.PlatformView.Padding = new Microsoft.UI.Xaml.Thickness(0);
            handler.PlatformView.Style = null;
            handler.PlatformView.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);

            // Remove the underline/border
            if (handler.PlatformView.Resources.ContainsKey("TextControlBorderBrush"))
            {
                handler.PlatformView.Resources["TextControlBorderBrush"] = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
            }
            if (handler.PlatformView.Resources.ContainsKey("TextControlBorderBrushFocused"))
            {
                handler.PlatformView.Resources["TextControlBorderBrushFocused"] = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
            }
#elif ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS || MACCATALYST
            handler.PlatformView.Layer.BorderWidth = 0;
#endif
        }
    }
}
