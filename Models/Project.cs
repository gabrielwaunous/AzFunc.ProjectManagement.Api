public class Project(int id, int userId, string name, string description)
{
    public int id { get; set; } = id;
    public int user_id { get; set; } = userId;
    public string name { get; set; } = name;
    public string description { get; set; } = description;
}