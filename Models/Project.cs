public class Project
{
    public int id { get; set; }
    public int user_id { get; set; }
    public string name { get; set; }
    public string description { get; set; }

    public Project()
    {
        
    }

    public Project(int Id, int UserId, string Name, string Description)
    {
        id = Id;
        user_id = UserId;
        name = Name;
        description = Description;
    }
    
}