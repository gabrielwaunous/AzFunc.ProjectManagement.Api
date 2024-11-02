using System;

public class TaskModel
{
    public int id { get; set; }
    public int project_id { get; set; }
    public string name { get; set; }
    public DateTime due_date { get; set; }
    public string priority { get; set; }
    public string status { get; set; }
}