using System;

// 1. Custom Delegate Version
// --------------------------

// Definisi delegate bernama PriceChangedHandler.
// Delegate ini menentukan "bentuk method" yang bisa dipakai sebagai event handler.
// Artinya, setiap subscriber harus punya method dengan return type void 
// dan dua parameter: decimal oldPrice, decimal newPrice.
public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

public class StockCustom
{
    // Property Symbol hanya untuk menyimpan nama saham.
    // get; hanya bisa dibaca dari luar (read-only).
    public string Symbol { get; }

    // Field untuk menyimpan harga saham saat ini (default = 0 karena decimal).
    decimal price;

    // Constructor: dipanggil ketika membuat objek StockCustom baru.
    // Parameter "symbol" di-assign ke property Symbol.
    // public StockCustom(string symbol) => Symbol = symbol;
    public StockCustom(string symbol)
    {
        Symbol = symbol;
    }

    // Deklarasi event bernama PriceChanged.
    // Event ini menggunakan delegate PriceChangedHandler sebagai "tipe event".
    // Jadi, semua subscriber harus sesuai dengan bentuk delegate tersebut.
    public event PriceChangedHandler PriceChanged; 

    // Property Price digunakan untuk membaca/menulis harga saham.
    public decimal Price
    {
        get => price;  // Getter: kembalikan nilai field "price".
        set
        {
            // Jika harga baru sama dengan harga lama, tidak ada perubahan ---> langsung return.
            if (price == value) return;


            // Simpan harga lama sebelum diubah.
            decimal oldPrice = price;

            // Ubah harga menjadi nilai baru.
            price = value;

            // Jika ada subscriber (PriceChanged tidak null), panggil event.
            // .Invoke(oldPrice, price) ---> memanggil semua handler yang sudah subscribe,
            // sambil memberikan data harga lama dan harga baru.
            PriceChanged?.Invoke(oldPrice, price);
        }
    }
}




// 2. Standard Event Pattern
// --------------------------
public class PriceChangedEventArgs : EventArgs
{
    public decimal OldPrice { get; }
    public decimal NewPrice { get; }

    public PriceChangedEventArgs(decimal oldPrice, decimal newPrice)
    {
        OldPrice = oldPrice;
        NewPrice = newPrice;
    }
}

public class Stock
{
    public string Symbol { get; }
    decimal price;

    public Stock(string symbol) => Symbol = symbol;

    public event EventHandler<PriceChangedEventArgs> PriceChanged;

    public decimal Price
    {
        get => price;
        set
        {
            if (price == value) return;

            decimal oldPrice = price;
            price = value;

            OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
        }
    }

    protected virtual void OnPriceChanged(PriceChangedEventArgs e)
    {
        PriceChanged?.Invoke(this, e);
    }
}


class Program
{
    static void Main()
    {
        Console.WriteLine("------ Custom Delegate Version ------");
        var cdiaCustom = new StockCustom("CDIA");
        cdiaCustom.PriceChanged += (oldP, newP) =>
            Console.WriteLine($"Price changed from {oldP} to {newP}");
        cdiaCustom.Price = 100;
        cdiaCustom.Price = 120;

        Console.WriteLine("\n------ Standard Event Pattern Version ------");
        var coin = new Stock("COIN");
        coin.PriceChanged += Stock_PriceChanged;
        coin.Price = 300;
        coin.Price = 280;
    }

    // Handler untuk Standard Event Pattern
    static void Stock_PriceChanged(object sender, PriceChangedEventArgs e)
    {
        var stock = (Stock)sender;
        Console.WriteLine($"{stock.Symbol} changed from {e.OldPrice} to {e.NewPrice}");
    }
}
