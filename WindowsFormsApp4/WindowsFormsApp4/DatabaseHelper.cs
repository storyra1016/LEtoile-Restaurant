using System;
using System.Data.SqlClient;

namespace WindowsFormsApp4
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsVIP { get; set; }
        public decimal DiscountRate { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal TotalPurchases { get; set; }
    }

    public static class DatabaseHelper
    {
        private const string ConnStr =
            @"Data Source=(localdb)\MSSQLLocalDB;" +
            "Initial Catalog=RestaurantDB;" +
            "Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnStr);
        }

        public static Member GetMemberByPhone(string phone)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        "SELECT * FROM Members WHERE Phone = @Phone", conn);
                    cmd.Parameters.AddWithValue("@Phone", phone);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Member
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString(),
                                IsVIP = (bool)reader["IsVIP"],
                                DiscountRate = (decimal)reader["DiscountRate"],
                                RegisterDate = (DateTime)reader["RegisterDate"],
                                TotalPurchases = (decimal)reader["TotalPurchases"]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("查詢錯誤: " + ex.Message);
            }
            return null;
        }

        public static bool AddMember(Member m, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        INSERT INTO Members 
                        (Name, Phone, Email, IsVIP, DiscountRate, RegisterDate, TotalPurchases)
                        VALUES 
                        (@Name, @Phone, @Email, @IsVIP, @DiscountRate, @RegisterDate, 0)", conn);

                    cmd.Parameters.AddWithValue("@Name", m.Name);
                    cmd.Parameters.AddWithValue("@Phone", m.Phone);
                    cmd.Parameters.AddWithValue("@Email", m.Email ?? "");
                    cmd.Parameters.AddWithValue("@IsVIP", m.IsVIP);
                    cmd.Parameters.AddWithValue("@DiscountRate", m.DiscountRate);
                    cmd.Parameters.AddWithValue("@RegisterDate", DateTime.Now);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                    errorMessage = "此電話號碼已經註冊過了！";
                else
                    errorMessage = "資料庫錯誤：" + ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = "錯誤：" + ex.Message;
                return false;
            }
        }
    }
}