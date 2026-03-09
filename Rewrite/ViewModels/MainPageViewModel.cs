using CommunityToolkit.Mvvm.ComponentModel;

namespace Rewrite.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private bool hasMessage;

    public void UpdateMessageState(string? text)
    {
        HasMessage = !string.IsNullOrWhiteSpace(text);
    }
}
