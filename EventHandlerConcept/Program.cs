using System;

class Lamp
{
    // 1. Declare a delegate type
    public delegate void SwitchHandler();

    // 2. Declare an event of that delegate type
    public event SwitchHandler TurnedOn;

    // Method to turn on the lamp
    public void TurnOn()
    {
        Console.WriteLine("Lamp is ON");
        
        // 3. Run the event if there are subscribers 
        TurnedOn?.Invoke();
    }
}

class Program
{
    // 4. Event handler (must match the delegate signature void return, no parameter)
    static void ShowMessage()
    {
        Console.WriteLine("Event received: Someone turned on the lamp!");
    }

    static void Main()
    {
        Lamp lamp = new Lamp();

        // 5. Subscribe to the event TurnedOn with the event handler ShowMessage
        lamp.TurnedOn += ShowMessage;

        // 6. Turn on the lamp, which will trigger the event
        lamp.TurnOn();

        // Output:
        // Lamp is ON
        // Event received: Someone turned on the lamp!
    }
}
