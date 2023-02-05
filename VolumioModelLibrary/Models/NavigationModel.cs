namespace VolumioModelLibrary.Models;

public class NavigationRoot
{
	public Navigation Navigation { get; set; }
}

public class Navigation
{
    public Prev prev { get; set; }
    public Info info { get; set; }
    public List<List> Lists { get; set; }
}

public class Item
{
	public string Service { get; set; }
	public string Type { get; set; }
	public string Title { get; set; }
    public int Duration { get; set; }
    public string TrackNumber { get; set; }
    public string TrackType { get; set; }
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
            if (value.StartsWith("http"))
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
            if (value.StartsWith("http"))
                _albumArt = value;
            else
                _albumArt = "http://192.168.2.21" + value;
        }
    }
}

public class Info
{
    public string uri { get; set; }
    public string service { get; set; }
    public string artist { get; set; }
    public string album { get; set; }
    public string albumart { get; set; }
    public string year { get; set; }
    public string genre { get; set; }
    public string type { get; set; }
    public string trackType { get; set; }
    public string duration { get; set; }
}

public class Prev
{
    public string uri { get; set; }
}

