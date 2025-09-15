using System;

interface IAnimal
{
    void animalSound();
    void sleep();
}

class Kelinci : IAnimal
{
    public void animalSound()
    {
        Console.WriteLine("Kelinci bersuara: oink oink");
    }
    public void sleep()
    {
        Console.WriteLine("Kelinci 2 jam tidur");
    }
}
class Kucing : IAnimal
{
    public void animalSound()
    {
        Console.WriteLine("Kucing bersuara: meong meong");
    }
    public void sleep()
    {
        Console.WriteLine("Kucing 8 jam tidur");
    }
}

class Program 
{
    static void Main(string[] args)
    {
        IAnimal myKelinci = new Kelinci();
        Kucing myKucing = new Kucing();
        myKelinci.animalSound();
        myKucing.animalSound();
        myKucing.sleep();
        myKelinci.sleep();
  }
}