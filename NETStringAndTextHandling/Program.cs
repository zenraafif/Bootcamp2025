// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine(char.ToUpper('c'));     // C
Console.WriteLine(char.IsWhiteSpace('\n')); // True
Console.WriteLine(char.ToUpperInvariant('i')); // True
//GetUnicodeCategory()
Console.WriteLine(char.GetUnicodeCategory('a')); // LowercaseLetter
Console.WriteLine(char.GetUnicodeCategory('A')); // UppercaseLetter 

string s1 = "Hello";
string s2 = "Escape sequences --- > First Line\r\nSecond Line"; // Escape sequences
Console.WriteLine(s2);
string s3 = @"C:\Path\File.txt";       // Verbatim string literal for file paths makes it easier to read and write
Console.WriteLine(s3); // Concatenation
string s4 = @"First Line
Second Line"; // Verbatim string literal with new line
Console.WriteLine(s4);
string s5 = "Hello, " + "World!"; // Concatenation
string s6 = string.Concat("Hello, ", "World!"); // Concatenation using

//Repeating characters
Console.WriteLine(new string('*', 10)); // **********

// Convert char array to string
// Using the string constructor
// ToCharArray method is used to convert a string to a char array 
    // and then the string constructor is used to create a new string from that char array.
char[] ca = "Hello".ToCharArray();
string s = new string(ca); // s = "Hello"
Console.WriteLine(ca);
Console.WriteLine(s);

// String interpolation










//StringBuilder: Mutable Strings for Efficiency

