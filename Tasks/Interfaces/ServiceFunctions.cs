using Tasks.Modells;

namespace Tasks.Interfaces;

public interface ServiceFunctions
{
    List<Task> GetAll();

    Task GetById(int id);

    int Add(Task task);

    bool Update(int id, Task task);

    bool Delete(int id);
}