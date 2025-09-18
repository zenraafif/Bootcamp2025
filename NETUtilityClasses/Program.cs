// using System;

// class Program
// {
//     static void Main()
//     {
//         Console.WriteLine($"OS: {Environment.OSVersion}");
//         Console.WriteLine($"Versi .NET: {Environment.Version}");
//         Console.WriteLine($"User: {Environment.UserName}");
//         Console.WriteLine($"Machine: {Environment.MachineName}");
//         Console.WriteLine($"Folder Desktop: {Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}");
//     }
// }

// using System;
// using System.Diagnostics;

// class Program
// {
//     static void Main()
//     {
//         Process process = Process.Start("notepad.exe"); // Buka Notepad
//         Console.WriteLine($"Notepad dibuka dengan ID: {process.Id}");

//         Console.WriteLine("Tekan ENTER untuk menutup Notepad...");
//         Console.ReadLine();

//         process.Kill(); // Tutup proses
//     }
// }


using System;

class Program
{
    static void Main()
    {
        // Simpan konfigurasi global
        AppContext.SetData("AppName", "MyConsoleApp");
        AppContext.SetData("EnableFeatureX", true);

        // Ambil kembali data
        Console.WriteLine($"Nama Aplikasi: {AppContext.GetData("AppName")}");
        Console.WriteLine($"Feature X Aktif: {AppContext.GetData("EnableFeatureX")}");
    }
}
