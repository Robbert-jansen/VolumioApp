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
	public string Albumart { get; set; }
}

public class List
{
	public List<string> AvailableListViews { get; set; }
	public List<Item> Items { get; set; }
}