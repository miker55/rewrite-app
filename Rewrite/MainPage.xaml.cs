using Rewrite.Core.Controls;
using Rewrite.ViewModels;

namespace Rewrite;

public partial class MainPage : ContentPage
{
    private SelectableChip? _activeChip;
    private readonly MainPageViewModel _viewModel;

    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;

        // Subscribe to text changes
        MessageEditor.TextChanged += (s, e) =>
        {
            _viewModel.UpdateMessageState(MessageEditor.Text);

            // Deselect any active chip when text changes
            if (_activeChip != null)
            {
                _activeChip.IsSelected = false;
                _activeChip = null;
            }
        };
    }

    private void OnChipTapped(object? sender, EventArgs e)
    {
        if (sender is not SelectableChip tappedChip)
            return;

        // Don't do anything if it's already active
        if (tappedChip == _activeChip)
            return;

        // Deselect previous chip
        if (_activeChip != null)
        {
            _activeChip.IsSelected = false;
        }

        // Select new chip
        tappedChip.IsSelected = true;
        _activeChip = tappedChip;

        // Handle tone selection
        var selectedTone = tappedChip.Text;
        // TODO: Implement tone selection logic
    }
}

