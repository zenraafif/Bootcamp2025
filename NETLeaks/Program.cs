using System;
using System.Linq;

// The event publisher
class Host
{
    public event EventHandler Click; // Declares an event
}

// The event subscriber
class Client
{
    Host _host;
    public Client(Host host)
    {
        _host = host;
        _host.Click += HostClicked; // Client subscribes to Host's Click event
    }
    void HostClicked(object sender, EventArgs e) 
    { 
        Console.WriteLine("Host clicked!"); 
    }
}

// A test class to demonstrate the leak
class Test
{
    static Host _host = new Host(); // A static Host, acting as a root
    public static void CreateClients()
    {
        Client[] clients = Enumerable.Range(0, 1000)
            .Select(i => new Client(_host)) // Creates 1000 Client instances
            .ToArray();

        // 'clients' array goes out of scope here
    }
}

class Program
{
    static void Main()
    {
        Test.CreateClients();
        Console.WriteLine("Clients created!");
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
