using System.ComponentModel;

namespace VolumioApp.PageModels;
public class BasePageModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public BasePageModel()
    {
    }
}
