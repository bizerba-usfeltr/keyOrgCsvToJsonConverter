// See https://aka.ms/new-console-template for more information

using System.Globalization;
using System.Text.Json.Nodes;
using CsvHelper;
using CsvHelper.Configuration;
using keyOrgCsvToJsonConverter;
using Newtonsoft.Json;

if(args.Length != 2)
{
    Console.WriteLine("wrong amount of arguments. Usage: <input csv filename> <output json filename>");
    return 1;
}

var configuration = new CsvConfiguration(CultureInfo.CurrentCulture)
{
    HasHeaderRecord = true,
};

try
{
    var reader = new StreamReader(args[0]);
    var csv = new CsvReader(reader, configuration);
    var records = csv.GetRecords<CsvRecord>().ToList();
    List<Item> itemList = new List<Item>();
    List<Group> groupList = new List<Group>();

    foreach (var row in records)
    {
        if (row.active == "")
        {
            row.active = "true";
        }

        if (row.parent == "")
        {
            row.parent = "0";
        }

        if (row.category == "items")
        {
            if (row.plu == "0" || row.plu == "" || row.department == "0" || row.department == "")
            {
                continue;
            }

            itemList.Add(new Item(row.id, Int32.Parse(row.parent), row.plu, row.department, row.name, row.defText,
                bool.Parse(row.active)));
        }
        else if (row.category == "groups")
        {
            if (row.name == "")
            {
                continue;
            }

            groupList.Add(new Group(row.id, Int32.Parse(row.parent), row.name, new List<Group>(), new List<Item>(),
                bool.Parse(row.active)));
        }
    }

    foreach (var i in groupList)
    {
        i.groups = groupList.Where(n => n.parent == i.id).ToList();
        i.items = itemList.Where(n => n.parent == i.id).ToList();
    }

    Dictionary<string, List<Group>> root = new Dictionary<string, List<Group>>();
    root.Add("groups", groupList.Where(n => n.parent == 0).ToList());
    File.WriteAllText(args[1], JsonConvert.SerializeObject( root ) );
    
}
catch (Exception e)
{
    Console.WriteLine(e);
    return 1;
}

return 0;