class Program
{
    static void Main()
    {
        Console.WriteLine("=== REKENING BANK ===");

        BankAccount akun1 = new BankAccount("1", "Andi", 1000_000);
        akun1.Deposit(1000);
        akun1.Withdraw(99000);
        akun1.PajakTransaksi(5099);
        akun1.DisplayAccount();

        BankAccount akun2 = new BankAccount("2","Siti", 1_000_000);
        akun2.Withdraw(300_000);
        akun2.PajakTransaksi(5099);
        akun2.DisplayAccount();

        Console.WriteLine("Transaksi selesai.");
    }
}
