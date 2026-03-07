namespace Rewrite.Core.Controls;

public partial class SelectableChip : ContentView
{
    private Border? _border;

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(SelectableChip), string.Empty,
            propertyChanged: OnTextChanged);

    public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(SelectableChip), false, 
            propertyChanged: OnIsSelectedChanged);

    public static readonly BindableProperty IconProperty =
        BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(SelectableChip), null,
            propertyChanged: OnIconChanged);

    public static readonly BindableProperty FontIconProperty =
        BindableProperty.Create(nameof(FontIcon), typeof(string), typeof(SelectableChip), null,
            propertyChanged: OnFontIconChanged);

    public static readonly BindableProperty ActiveColorProperty =
        BindableProperty.Create(nameof(ActiveColor), typeof(Color), typeof(SelectableChip), Color.FromArgb("#2F9E8F"));

    public static readonly BindableProperty InactiveColorProperty =
        BindableProperty.Create(nameof(InactiveColor), typeof(Color), typeof(SelectableChip), Color.FromArgb("#E9EEF1"));

    public static readonly BindableProperty ActiveTextColorProperty =
        BindableProperty.Create(nameof(ActiveTextColor), typeof(Color), typeof(SelectableChip), Colors.White);

    public static readonly BindableProperty InactiveTextColorProperty =
        BindableProperty.Create(nameof(InactiveTextColor), typeof(Color), typeof(SelectableChip), Color.FromArgb("#1C1C1E"));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public ImageSource? Icon
    {
        get => (ImageSource?)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public string? FontIcon
    {
        get => (string?)GetValue(FontIconProperty);
        set => SetValue(FontIconProperty, value);
    }

    public Color ActiveColor
    {
        get => (Color)GetValue(ActiveColorProperty);
        set => SetValue(ActiveColorProperty, value);
    }

    public Color InactiveColor
    {
        get => (Color)GetValue(InactiveColorProperty);
        set => SetValue(InactiveColorProperty, value);
    }

    public Color ActiveTextColor
    {
        get => (Color)GetValue(ActiveTextColorProperty);
        set => SetValue(ActiveTextColorProperty, value);
    }

    public Color InactiveTextColor
    {
        get => (Color)GetValue(InactiveTextColorProperty);
        set => SetValue(InactiveTextColorProperty, value);
    }

    public event EventHandler<EventArgs>? Tapped;

    public SelectableChip()
    {
        InitializeComponent();

        _border = this.Content as Border;
        UpdateVisualState();
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SelectableChip chip && newValue is string text)
        {
            chip.ChipLabel.Text = text;
        }
    }

    private static void OnIconChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SelectableChip chip)
        {
            if (newValue is ImageSource imageSource)
            {
                chip.ChipIcon.Source = imageSource;
                chip.ChipIcon.IsVisible = true;
                chip.ChipFontIcon.IsVisible = false;
            }
            else
            {
                chip.ChipIcon.IsVisible = false;
            }
        }
    }

    private static void OnFontIconChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SelectableChip chip)
        {
            if (newValue is string iconName && !string.IsNullOrEmpty(iconName))
            {
                chip.ChipFontIcon.Text = iconName;
                chip.ChipFontIcon.IsVisible = true;
                chip.ChipIcon.IsVisible = false;
                chip.UpdateVisualState(); // Update colors after setting icon
            }
            else
            {
                chip.ChipFontIcon.IsVisible = false;
            }
        }
    }

    private static void OnIsSelectedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SelectableChip chip)
        {
            chip.UpdateVisualState();
        }
    }

    private void UpdateVisualState()
    {
        if (_border != null)
        {
            _border.BackgroundColor = IsSelected ? ActiveColor : InactiveColor;
        }

        if (ChipLabel != null)
        {
            ChipLabel.TextColor = IsSelected ? ActiveTextColor : InactiveTextColor;
        }

        if (ChipFontIcon != null && ChipFontIcon.IsVisible)
        {
            // Font icon matches text color - white when selected, dark when not
            ChipFontIcon.TextColor = IsSelected ? ActiveTextColor : InactiveTextColor;
        }

        if (ChipIcon != null && ChipIcon.IsVisible)
        {
            ChipIcon.Opacity = IsSelected ? 1.0 : 0.6;
        }
    }

    private void OnTapped(object? sender, TappedEventArgs e)
    {
        Tapped?.Invoke(this, EventArgs.Empty);
    }
}
