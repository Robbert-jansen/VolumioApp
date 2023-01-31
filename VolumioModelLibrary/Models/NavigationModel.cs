namespace VolumioModelLibrary.Models;

public class NavigationRoot
{
	public Navigation Navigation { get; set; }
}

public class Navigation
{
	public List<List> Lists { get; set; }
}

public class Item
{
	public string Service { get; set; }
	public string Type { get; set; }
	public string Title { get; set; }
	public string Artist { get; set; }
	public object Year { get; set; }
	public string Album { get; set; }
	public string Uri { get; set; }
	private string _albumArt;
    public string? AlbumArt
    {
        get => _albumArt;
        set
        {
            if (value.Contains("http"))
                _albumArt = value;
            else
                _albumArt = "http://192.168.2.21" + value;
        }
    }
}

public class List
{
	public List<string> AvailableListViews { get; set; }
	public List<Item> Items { get; set; }
	public string Name { get; set; }
	public string Uri { get; set; }
	public string Plugin_Type { get; set; }
	public string Plugin_Name { get; set; }
	public string Icon { get; set; }

    private string _albumArt;
    public string? AlbumArt
    {
        get => _albumArt;
        set
        {
            if (value.Contains("http"))
                _albumArt = value;
            else
                _albumArt = "http://192.168.2.21" + value;
        }
    }
}