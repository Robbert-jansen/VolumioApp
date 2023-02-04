using VolumioApp.Constants;
using VolumioModelLibrary.Models;

namespace VolumioApp.TemplateSelectors;

public class VolumioItemDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate SongTemplate { get; set; }

    public DataTemplate FolderTemplate { get; set; }

    public DataTemplate DefaultTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        switch (((Item)item).Type)
        {
            case VolumioItemTypes.Song:
                {
                    return SongTemplate;
                }
                case VolumioItemTypes.Folder:
                {
                    return FolderTemplate;
                }
                case VolumioItemTypes.StreamingCategory: 
                {
                    return DefaultTemplate;
                }
            default:
                return DefaultTemplate;
        }
    }
}
