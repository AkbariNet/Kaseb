using System.ComponentModel;
using UraniumUI.Material.Controls;

namespace Kaseb.Views.Element.Buttons;

public partial class HamburgerMenuItem : ButtonView
{
	public HamburgerMenuItem()
	{
        this.BindingContext = VM;

        InitializeComponent();
	}
    HamburgerMenuItemVM VM = new HamburgerMenuItemVM();
    public string Title {
		get
		{
			return VM.Title;
		}
		set
		{
			VM.Title = value;
		}
	}
	public string Description
    {
        get
        {
            return VM.Description;
        }
        set
        {
            VM.Description = value;
        }
    }
    public FontImageSource Icon
    {
        get
        {
            return VM.Icon;
        }
        set
        {
            VM.Icon = value;
        }
    } 
}

public class HamburgerMenuItemVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private string _title = string.Empty;
    public string Title
    {
        get { return _title; }
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    private string _description = string.Empty;
    public string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }
    private FontImageSource _icon = new FontImageSource();
    public FontImageSource Icon
    {
        get { return _icon; }
        set
        {
            _icon = value;
            OnPropertyChanged(nameof(Icon));
        }
    }
}