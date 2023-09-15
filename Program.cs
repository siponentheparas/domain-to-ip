using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

internal class Program
{
    private static void Main(string[] args)
    {
        string? domain;

        if (args.Length == 0)
        {
            Console.Write("Domain: ");
            domain = Console.ReadLine();
        }
        else
        {
            domain = args[0];
        }

        if (string.IsNullOrWhiteSpace(domain))
        {
            Console.WriteLine("Domain cannot be nothing");
            Environment.Exit(0);
        }

        if (!domain.Contains("."))
        {
            Console.WriteLine("Domain has to contain at least one dot.");
            Environment.Exit(0);
        }

        try
        {
            IPAddress[] addresses = Dns.GetHostAddresses(domain);

            if (addresses.Length == 0)
            {
                Console.WriteLine("No addresses found.");
            } else if (addresses.Length == 1)
            {
                Console.WriteLine("{0}", addresses[0]);
            } else
            {
                foreach (IPAddress address in addresses)
                {
                    Console.WriteLine("{0}", address);
                }
            }
            Environment.Exit(0);
        }
        catch (SocketException)
        {
            Console.WriteLine("An exception happened. Reason:");
            Console.WriteLine("Could not resolve hostname or address: \"{0}\"", domain);
            Environment.Exit(0);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("An exception happened. Reason:");
            Console.WriteLine("Invalid hostname or address: \"{0}\"", domain);
            Environment.Exit(0);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An exception happened. Reason:");
            Console.WriteLine(ex);
            Environment.Exit(0);
        }
    }
}