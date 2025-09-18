using System;

class Program
{


    static void Main()
    {
        // =============================
        // BAGIAN 1: TimeSpan Constructor
        // =============================

        // 1) TimeSpan(int hours, int minutes, int seconds)
        TimeSpan ts01 = new TimeSpan(1, 30, 0);
        Console.WriteLine("Constructor (hours, minutes, seconds): " + ts01);
        // Output: 01:30:00 (1 jam 30 menit)

        // 2) TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
        TimeSpan ts02 = new TimeSpan(1, 2, 30, 45, 500);
        Console.WriteLine("Constructor (days, hours, minutes, seconds, ms): " + ts02);
        // Output: 1.02:30:45.5000000 (1 hari, 2 jam, 30 menit, 45 detik, 500 ms)

        // 3) TimeSpan(long ticks)  -> 1 tick = 100 nanoseconds
        TimeSpan ts03 = new TimeSpan(10000000);
        Console.WriteLine("Constructor (ticks): " + ts03);
        // 10.000.000 ticks = 1 detik
        // Output: 00:00:01


        // =============================
        // BAGIAN 2: TimeSpan.FromX
        // =============================

        // Dari hari
        TimeSpan ts1 = TimeSpan.FromDays(1.5);
        Console.WriteLine("FromDays(1.5): " + ts1);
        // 1.5 hari = 1 hari 12 jam -> Output: 1.12:00:00

        // Dari jam
        TimeSpan ts2 = TimeSpan.FromHours(2.75);
        Console.WriteLine("FromHours(2.75): " + ts2);
        // 2.75 jam = 2 jam 45 menit -> Output: 02:45:00

        // Dari menit
        TimeSpan ts3 = TimeSpan.FromMinutes(90);
        Console.WriteLine("FromMinutes(90): " + ts3);
        // 90 menit = 1 jam 30 menit -> Output: 01:30:00

        // Dari detik
        TimeSpan ts4 = TimeSpan.FromSeconds(125);
        Console.WriteLine("FromSeconds(125): " + ts4);
        // 125 detik = 2 menit 5 detik -> Output: 00:02:05

        // Dari milidetik
        TimeSpan ts5 = TimeSpan.FromMilliseconds(2500);
        Console.WriteLine("FromMilliseconds(2500): " + ts5);
        // 2500 ms = 2.5 detik -> Output: 00:00:02.5000000

#if NET7_0_OR_GREATER
        // Dari mikrodetik (baru ada di .NET 7)
        TimeSpan ts6 = TimeSpan.FromMicroseconds(2500000);
        Console.WriteLine("FromMicroseconds(2500000): " + ts6);
        // 2.500.000 µs = 2.5 detik -> Output: 00:00:02.5000000
#endif

        // ============================
        // Additional
        // ============================
        // Timespan Operations
        TimeSpan duration = TimeSpan.FromHours(2) + TimeSpan.FromMinutes(30); // 02:30:00
        Console.WriteLine("AAAAAAA Duration: " + duration);
        TimeSpan nearlyTenDays = TimeSpan.FromDays(10) - TimeSpan.FromSeconds(1); // 9.23:59:59
        Console.WriteLine("Nearly Ten Days: " + nearlyTenDays);
        
        // =============================
        // BAGIAN 3: DateTimeOffset Constructor
        // =============================
        DateTimeOffset dto1 = new DateTimeOffset(2025, 9, 17, 14, 30, 0, TimeSpan.FromHours(+7));
        Console.WriteLine("DateTimeOffset with explicit offset: " + dto1);

        // Dari DateTime dengan Kind = Utc
        DateTime dtUtc = new DateTime(2025, 9, 17, 7, 30, 0, DateTimeKind.Utc);
        DateTimeOffset dtoUtc = new DateTimeOffset(dtUtc);
        Console.WriteLine("From DateTime (Utc): " + dtoUtc);

        // Dari DateTime dengan Kind = Local
        DateTime dtLocal = new DateTime(2025, 9, 17, 14, 30, 0, DateTimeKind.Local);
        DateTimeOffset dtoLocal = new DateTimeOffset(dtLocal);
        Console.WriteLine("From DateTime (Local): " + dtoLocal);

        // Dari DateTime dengan Kind = Unspecified
        DateTime dtUnspecified = new DateTime(2025, 9, 17, 14, 30, 0, DateTimeKind.Unspecified);
        DateTimeOffset dtoUnspecified = new DateTimeOffset(dtUnspecified);
        Console.WriteLine("From DateTime (Unspecified): " + dtoUnspecified);

        // Override offset manual
        DateTime dt = new DateTime(2025, 9, 17, 14, 30, 0);
        DateTimeOffset dtoCustom = new DateTimeOffset(dt, TimeSpan.FromHours(-5));
        Console.WriteLine("DateTimeOffset with custom offset: " + dtoCustom);
    }
}
