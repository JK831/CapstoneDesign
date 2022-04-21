using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using ServerCore;

namespace Server.DB
{
    public class DbManager
    {
        static private MySqlConnection conn;
        static public MySqlCommand cmd;

        //추후 정적으로 바꿈
        static private string DB_IP = "database-codingisland.c37r4fnqfff9.ap-northeast-2.rds.amazonaws.com";
        static string DB_TARGET = "test_DB";
        static string DB_UID = "";
        static string DB_PWD = "";
        static string User_DB_Table = "user_data";
        static string Challenge_DB_Table = "challenge_data";

        public static void challenge_Insert_DB(String UID, byte Stage, byte Star)
        {
            string connectString = string.Format("Server={0};Database={1};Uid ={2};Pwd={3};", DB_IP, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("Insert Into '{0}'  (UID, Stage, Star) values ('{1}', '{2}', '{3}')", Challenge_DB_Table, UID, Stage, Star);

            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public static void challenge_UpdateStar(String UID, byte STAGE, byte Star)
        {
            string connectString = string.Format("Server={0};Database={1};Uid ={2};Pwd={3};", DB_IP, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("Select Star from '{0}' where Uid ='{1}' AND STAGE = {2}", Challenge_DB_Table, UID, STAGE);
            int remaining_Star;

            using (MySqlConnection conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                remaining_Star = Convert.ToInt32(cmd.ExecuteScalar());
                if (remaining_Star < Star)
                {
                    sql = String.Format("Update '{0}' Set Star ={1} where Uid ='{2}' AND STAGE = {3}", Challenge_DB_Table, Star, UID, STAGE);
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int challenge_Check_Ranking(String UID)
        {
            string connectString = string.Format("Server={0};Database={1};Uid ={2};Pwd={3};", DB_IP, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("Select Rank_Table.ranking from (Select Uid, RANK() OVER (ORDER BY SUM(Star) DESC) AS ranking from '{0}' group by Uid) Rank_Table where Rank_Table.Uid ='{1}'", Challenge_DB_Table, UID);
            int user_Ranking;

            using (MySqlConnection conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                user_Ranking = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return user_Ranking;
        }

        public static  byte challenge_Total_Star(String UID)
        {
            byte total_Got_Star;
            string connectString = string.Format("Server={0};Database={1};Uid ={2};Pwd={3};", DB_IP, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("Select SUM(Star) from '{0}' where Uid ='{1}'", Challenge_DB_Table, UID);

            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                total_Got_Star = Convert.ToByte(cmd.ExecuteScalar());
            }
            return total_Got_Star;
        }

        public static Dictionary<byte, byte> Load_star(String UID)
        {
            string connectString = string.Format("Server={0};Database={1};Uid ={2};Pwd={3};", DB_IP, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("Select Stage, Star from '{0}' where Uid ='{1}' order by Stage", Challenge_DB_Table, UID);
            Dictionary<byte, byte> star_Dic = new Dictionary<byte, byte>();
            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    star_Dic.Add(Convert.ToByte(reader["Stage"]), Convert.ToByte(reader["Star"]));
                }
            }
            return star_Dic;
        }

        public static void study_Enroll_DB(String UID)
        {
            string connectString = string.Format("Server={0};Database={1};Uid ={2};Pwd={3};", DB_IP, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("Insert Into '{0}' (Uid, Stage) values ('{1}', 1)", User_DB_Table, UID);

            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }


        public static void study_Open_Next_Stage(String UID, byte Stage)
        {
            string connectString = string.Format("server={0};database={1};uid ={2};pwd={3};", DB_IP, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("Update '{0}' Set Stage ={1} where Uid ='{2}'", User_DB_Table, Stage, UID);

            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public static byte study_Get_Max_Stage(String UID)
        {
            byte Max_Stage;
            string connectString = string.Format("Server={0};Database={1};Uid ={2};Pwd={3};", DB_IP, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("Select Stage from '{0}' where Uid ='{1}'", User_DB_Table, UID);

            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                Max_Stage = Convert.ToByte(cmd.ExecuteScalar());
            }
            return Max_Stage;
        }

        public static List<S_ChallengeTop30.Rank> Study_ChallengeTop30(List<S_ChallengeTop30.Rank> list)
        {
            string connectString = string.Format("Server={0};Database={1};Uid ={2};Pwd={3};", DB_IP, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("Select Uid, SUM(Star) as 'totalStars' , Rank_Table.ranking from (Select Uid, RANK() OVER (ORDER BY SUM(Star) DESC) AS ranking from '{0}' group by Uid) Rank_Table limit 30", Challenge_DB_Table);

            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new S_ChallengeTop30.Rank()
                    {
                        UId = Convert.ToString(reader["Uid"]),
                        ranking = Convert.ToByte(reader["ranking"]),
                        totalStars = Convert.ToByte(reader["totalStars"])
                    });
                }
            }
            return list;
        }
    }
}
