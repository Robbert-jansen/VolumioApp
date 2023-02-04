using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumioModelLibrary.Models;

namespace VolumioApp.TemplateSelectors;

public class VolumioListDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate DefaultTemplate { get; set; }

    public DataTemplate EmptyTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if(((List)item).Name != null) 
        {
            return DefaultTemplate;
        }
        return EmptyTemplate;
    }
}
