using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModifyingDatabaseData.Dal;

namespace ModifyingDatabaseData
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer guest = new Customer();
            CustomerAdapter adapter = new CustomerAdapter();

            Console.Write("Please enter your first name: ");
            string first = Console.ReadLine();
            Console.Write("Please enter your last name: ");
            string last = Console.ReadLine();
            Console.Write("Please enter the name of your country: ");
            string country = Console.ReadLine();
            Console.Write("Please enter your email address: ");
            string email = Console.ReadLine();

            guest.FirstName = first;
            guest.LastName = last;
            guest.Country = country;
            guest.Email = email;

            adapter.InsertCustomer(guest);

            List<Customer> customers = adapter.GetAll();
            foreach(Customer customer in customers)
            {
                Console.WriteLine($"CustomerID: {customer.CustomerId} FirstName: {customer.FirstName} LastName: {customer.LastName} Country: {customer.Country} Email: {customer.Email}");
            }

            Console.ReadKey();
        }
    }
}
