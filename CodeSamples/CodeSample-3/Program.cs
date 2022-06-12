class Foo
{
    public void DoSomething()
    {
        Console.WriteLine("Doing the foo do.");
    }
}

class Bar
{

}

interface IVariant<in F, out B>
    where F : Foo
    where B : Bar
{
    private F ConvertToFoo<T>(T item) where T : B
    public void DoSomething<T>(T item) where T : B => ConvertToFoo(item).DoSomething();
}