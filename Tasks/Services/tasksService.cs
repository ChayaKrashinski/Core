using Tasks.Modells;
using Tasks.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;

namespace Tasks.Services;

public static class TasksService:ServiceFunction
{
    private static List<Task> tasksList;

    private string fileName = "Tasks.json";

    List<Task> GetAll() => tasksList;

    public TasksService()
    {
        this.fileName = Path.Combine("Data", "Tasks.json");
                    using (var jsonFile = File.OpenText(fileName))
            {
#pragma warning disable CS8601 // Possible null reference assignment.
            tasksList = JsonSerializer.Deserialize<List<task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
#pragma warning restore CS8601 // Possible null reference assignment.
        }
    }

    private void saveToFile()
    {
        //save the list in json file...
        File.WriteAllText(fileName, JsonSerializer.Serialize(tasksList));
    }

    public Task GetById(int id)
    {
        return tasks.FirstOrDefault(t => t.id == id);
    }

    public int Add(Task task)
    {
        if(tasksList.Count == 0)
            tasks.id = 1;
        else
            tasks.id = tasks.Max(t => t.id)+1;
        tasks.Add(task);
        saveToFile();
        return task.id;
    }

    public bool Update(int id, Task task)
    {
        if(id != task.id)
            return false;

        var prevTask = GetById(id);
        if(prevTask == null)
            return false;

        int index = tasksList.IndexOf(prevTask);
        if(index == -1)
            return false;

        tasksList[index] = task;
        saveToFile();
        return true;
    }

    public bool Delete(int id)
    {
        var prevTask = GetById(id);
        if(prevTask == null)
            return false;

        int index = tasksList.IndexOf(prevTask);
        if(index == -1)
            return false;

        tasksList.RemoveAt(index);
        saveToFile();
        return true;
    }

}

public static class TaskUtils
{
    public static void AddTask(this IServiceCollection services)
    {
        services.AddSingleton<ITaskService, todoListServices>();
    }
}
