namespace MashTodo.Service
{
    public class MashAppConfig
    {
        public string BaseAddress => "http://localhost:5000/api/TodoItems/";

        public int MiminumTaskNameLength => 3;
        public int MaximumTaskNameLength => 50;
    }
}