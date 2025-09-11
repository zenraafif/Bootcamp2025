class Hewan
{
    public virtual void Speak()
    {
        Console.WriteLine("Hewan bersuara");
    }
}

class Anjing : Hewan
{
    public override void Speak()
    {
        Console.WriteLine("Anjing menggonggong");
    }
}

class Kucing : Hewan
{
    public override void Speak()
    {
        Console.WriteLine("Kucing mengeong");
    }
}

class Program
{
    static void Main()
    {
        Hewan a1 = new Anjing();
        Hewan a2 = new Kucing();

        a1.Speak();
        a2.Speak();
    }
}
