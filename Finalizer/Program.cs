using System;

class MyClass
{
    public MyClass()
    {
        Console.WriteLine("Objek dibuat");
    }

    // Finalizer (destructor)
    ~MyClass()
    {
        Console.WriteLine("Objek DIHAPUS oleh GC");
    }
}

class Program
{
    static void Main()
    {
        MyClass obj = new MyClass();
        obj = null;

        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("Program selesai");
    }
}
class ResourceHolder : IDisposable
{
    private bool disposed = false;

    // Finalizer
    ~ResourceHolder()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this); // finalizer tidak perlu jalan
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // bersihkan managed resource
            }
            // bersihkan unmanaged resource
            disposed = true;
        }
    }
}
