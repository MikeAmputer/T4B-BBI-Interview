var task1 = Task.Run(() => Foo.Instance);
var task2 = Task.Run(() => Foo.Instance);
Console.WriteLine(task1.Result == task2.Result);

class Foo
{
    private static Foo? _instance;
    public static Foo Instance
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }
}