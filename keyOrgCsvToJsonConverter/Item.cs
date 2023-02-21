namespace keyOrgCsvToJsonConverter;

public class Item
{
    public int id;
    public int parent;
    public string plu;
    public string department;
    public string name;
    public string defText;
    public bool active;

    public Item()
    {
        this.id = -1;
        this.parent = -1;
        this.plu = "";
        this.department = "";
        this.name = "";
        this.defText = "";
        this.active = true;
    }
    
    public Item(int id, int parent, string plu, string department, string name, string defText, bool active)
    {
        this.id = id;
        this.parent = parent;
        this.plu = plu;
        this.department = department;
        this.name = name;
        this.defText = defText;
        this.active = active;
    }
}