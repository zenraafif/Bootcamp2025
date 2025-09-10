using System;

public class BankAccount
{
    public string Owner { get; }
    public string NoRek { get; }
    public string Pajak { get; }
    public decimal balance;

    public BankAccount(string number, string name, decimal initialBalance)
    {
        this.Owner = name;
        this.NoRek = number;

        Deposit(initialBalance);
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Jumlah deposit minimal Rp.0 !!!");
            Environment.Exit(1);
        }
            
        balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Jumlah penarikan harus lebih dari Rp.0 !!!");
            Environment.Exit(1);
        }
        if (amount > balance)
        {
            Console.WriteLine("Jumlah saldo tidak cukup");
            Environment.Exit(1);   
        }
        balance -= amount;
    }

    public void PajakTransaksi(decimal amount)
    {
        if (amount > balance)
        {
            Console.WriteLine("Transaksi Gagal");
            Console.WriteLine("Jumlah saldo tidak cukup untuk pajak");
            Environment.Exit(1);   
        }
        balance -= amount;
    }

    public decimal GetBalance()
    {
        return balance;
    }

    public void DisplayAccount()
    {
        Console.WriteLine($"Pemilik: {Owner}, Nomor Akun: {NoRek}, Saldo: {balance:C}, Pajak Transaksi {Pajak}");
    }
}