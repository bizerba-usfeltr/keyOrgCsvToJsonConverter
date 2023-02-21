namespace keyOrgCsvToJsonConverter;

public class Group
{
    public int id;
    public int parent;
    public string name;
    public List<Group> groups;
    public List<Item> items;
    public Boolean active;

    public Group()
    {
        this.id = -1;
        this.parent = -1;
        this.name = "";
        this.groups = new List<Group>();
        this.items = new List<Item>();
        this.active = true;
    }
    
    public Group(int id, int parent, string name, List<Group> groups, List<Item> items, Boolean active)
    {
        this.id = id;
        this.parent = parent;
        this.name = name;
        this.groups = groups;
        this.items = items;
        this.active = active;
    }
}
