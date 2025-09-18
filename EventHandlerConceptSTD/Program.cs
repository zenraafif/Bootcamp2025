using System;

// 1. Buat class turunan EventArgs (untuk info tambahan)
//    Class ini opsional, bisa juga pakai EventArgs biasa tanpa data tambahan.
//    Jika tidak perlu data tambahan, bisa langsung pakai EventArgs.
//    public delegate void SwitchHandler();
class LampEventArgs : EventArgs   // 1. Buat class turunan EventArgs (untuk info tambahan)
{
    public string User { get; }
    public LampEventArgs(string user) => User = user;
}

class Lamp
{
    // 2. EVENT standard: pakai EventHandler<T>
    //    EVENT terbuat dari DELEGATE EventHandler<T>
    //    Tipe data T adalah class turunan EventArgs (bisa pakai EventArgs biasa jika tidak perlu data tambahan).
    //    EventHandler = DELEGATE dengan signature (object sender, EventArgs e)
    //    delegate bawanannya sudah mendefinisikan signature handler: (object sender, T e)
    //    EventHandler<LampEventArgs> adalah delegate yang sudah didefinisikan sebelumnya di .NET
    // BTS : public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgs;
    public event EventHandler<LampEventArgs> TurnedOn;

    public void TurnOn(string user)
    {
        Console.WriteLine("Lamp is ON");

        // 3. Trigger event
        TurnedOn?.Invoke(this, new LampEventArgs(user));
    }
}

class Program
{
    // 4. HANDLER sesuai aturan: (object sender, LampEventArgs e)
    static void ShowMessage(object sender, LampEventArgs e)
    {
        Console.WriteLine($"Event received: {e.User} menyalakan lampu!");
    }

    static void Main()
    {
        Lamp lamp = new Lamp();

        // 5. Subscribe handler
        lamp.TurnedOn += ShowMessage;

        // 6. Jalankan
        lamp.TurnOn("Budi");

        // Output:
        // Lamp is ON
        // Event received: Budi menyalakan lampu!
    }
}
