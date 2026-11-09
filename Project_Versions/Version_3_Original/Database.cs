using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace RestaurantBooking
{
    public static class Database
    {
        private static string dbPath = "restaurant.db";
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        // ВИРТУАЛЬНЫЙ СПИСОК ЗАНЯТЫХ СТОЛОВ (не пишется в базу данных!)
        // Формат: "Столик №1|2026-10-31|12:00"
        public static List<string> VirtualBusyTables = new List<string>();

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string createUsers = @"CREATE TABLE IF NOT EXISTS Users (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            Login TEXT NOT NULL,
                                            Password TEXT NOT NULL);";
                    string createBookings = @"CREATE TABLE IF NOT EXISTS Bookings (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            ClientName TEXT NOT NULL,
                                            Email TEXT NOT NULL,
                                            Phone TEXT NOT NULL,
                                            Guests TEXT NOT NULL,
                                            TableNumber TEXT NOT NULL,
                                            [Time] TEXT NOT NULL,
                                            [Date] TEXT NOT NULL);";
                    using (var command = new SQLiteCommand(createUsers, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (var command = new SQLiteCommand(createBookings, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    string insertAdmin = "INSERT INTO Users (Login, Password) VALUES ('admin', '1234');";
                    using (var command = new SQLiteCommand(insertAdmin, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static bool ValidateUser(string login, string password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Login = @login AND Password = @password";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static bool IsTableBusy(string table, string date, string time)
        {
            // 1. Проверяем реальные брони в базе данных
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Bookings WHERE TableNumber = @table AND [Date] = @date AND [Time] = @time";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@table", table);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@time", time);
                    long count = (long)command.ExecuteScalar();
                    if (count > 0) return true;
                }
            }

            // 2. Проверяем виртуальную (тестовую) занятость
            string mockKey = $"{table}|{date}|{time}";
            return VirtualBusyTables.Contains(mockKey);
        }

        public static void AddBooking(string name, string email, string phone, string guests, string table, string time, string date)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Bookings (ClientName, Email, Phone, Guests, TableNumber, [Time], [Date]) VALUES (@name, @email, @phone, @guests, @table, @time, @date)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@guests", guests);
                    command.Parameters.AddWithValue("@table", table);
                    command.Parameters.AddWithValue("@time", time);
                    command.Parameters.AddWithValue("@date", date);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static DataTable GetBookings()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Bookings";
                using (var adapter = new SQLiteDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public static void DeleteBooking(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Bookings WHERE Id = @id";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Метод для создания виртуальной (тестовой) занятости столов
        public static void SimulateRandomBookings()
        {
            VirtualBusyTables.Clear();
            Random rnd = new Random();
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            string[] times = { "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00" };

            // Генерируем 30 случайных занятых слотов на сегодня
            for (int i = 0; i < 30; i++)
            {
                string table = $"Столик №{rnd.Next(1, 11)}";
                string time = times[rnd.Next(times.Length)];
                string mockKey = $"{table}|{today}|{time}";

                if (!VirtualBusyTables.Contains(mockKey))
                {
                    VirtualBusyTables.Add(mockKey);
                }
            }
        }
    }
}