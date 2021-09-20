using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;

namespace ModifyingDatabaseData.Dal
{
    public class InvoiceAdapter
    {
        private string _connectionString = @"Data Source= Chinook_Sqlite_AutoIncrementPKs.sqlite; datetimeformat=CurrentCulture;";

        public List<Invoice> GetAll()
        {
            List<Invoice> returnValue = new List<Invoice>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "Select InvoiceId, CustomerId, InvoiceDate, Total FROM Invoice";
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    Invoice customer = GetFromReader(reader);

                    returnValue.Add(customer);
                }
                return returnValue;
            }
        }

        public List<Invoice> GetByCustomerId(int customerId)
        {
            List<Invoice> returnValue = new List<Invoice>();
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {

                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT InvoiceId, CustomerId, InvoiceDate, Total FROM Invoice WHERE CustomerId = " + customerId.ToString();
                connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Invoice invoice = GetFromReader(reader);
                    returnValue.Add(invoice);
                }

                return returnValue;
            }
        }

        public Invoice GetById(int invoiceId)
        {
            Invoice returnValue = null;
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {

                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT InvoiceId, CustomerId, InvoiceDate, Total FROM Invoice WHERE InvoiceId = " + invoiceId.ToString();
                connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    returnValue = GetFromReader(reader);
                }

                return returnValue;
            }
        }

        public bool InsertInvoice(Invoice invoice)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Invoice (CustomerId, InvoiceDate, Total) VALUES ("
                + invoice.CustomerId + "," + invoice.InvoiceDate + ",'" + invoice.Total + "')";
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool UpdateInvoice(Invoice invoice)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Invoice SET Total = " + invoice.Total + ", InvoiceDate = '"
                + invoice.InvoiceDate + "' WHERE InvoiceId = " + invoice.InvoiceId;
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteInvoice(int invoiceId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Invoice WHERE InvoiceId = " + invoiceId;
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private Invoice GetFromReader(DbDataReader reader)
        {
            Invoice invoice = new Invoice();
            invoice.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
            invoice.InvoiceId = reader.GetInt32(reader.GetOrdinal("InvoiceId"));
            invoice.Total = reader.GetDecimal(reader.GetOrdinal("Total"));
            invoice.InvoiceDate = reader.GetDateTime(reader.GetOrdinal("InvoiceDate"));
            return invoice;
        }
    }
}