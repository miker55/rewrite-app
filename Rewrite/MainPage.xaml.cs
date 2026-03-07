using Rewrite.Core.Controls;

namespace Rewrite;

public partial class MainPage : ContentPage
{
    private SelectableChip? _activeChip;

    public MainPage()
    {
        InitializeComponent();

        // Set Professional as initially active
        _activeChip = ProfessionalChip;
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

