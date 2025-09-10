using System;

public class Produk
{
    private string nama;
    private double harga;

    public string Nama
    {
        get {
            return nama;
        }
        set {
            nama = value;
        }
    }

    public double Harga
    {
        get { return harga; }
        set {
            if (value >= 0)
            {
                harga = value;
            }
            else
            {
                Console.WriteLine("Harga tidak boleh negatif");   
            }
        }
    }

    public void TampilkanInfo()
    {
        Console.WriteLine($"Nama: {Nama}, Harga: {Harga:C}");
    }
}
