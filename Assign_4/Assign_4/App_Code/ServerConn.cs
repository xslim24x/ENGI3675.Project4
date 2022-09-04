//-----------------------------------------------------------------------
// <copyright file="ServerConn.cs" company="LakeheadU">
//     Copyright ENGI-3675. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Assign_4.App_Code.ServerConn
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using Npgsql;

    /// <summary>
    /// Class for server connection methods
    /// </summary>
    public static class ServerConn
    {
        /// <summary>
        /// Function to execute incoming SQL queries
        /// </summary>
        /// <param name="sql">Takes an SQL query as a parameter to execute on the database</param>
        /// <returns>Returns data read from executed query</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities",
            Justification = "Risky to change manually")]
        public static DataTable MyQuery(string sql)
        {
            DataTable dt = new DataTable();
            NpgsqlDataReader dr = null;

            using (NpgsqlConnection conn = new NpgsqlConnection(
                "Server=127.0.0.1; Port=5432; Database=Assignment_4; Integrated Security=true;"))
            {
                conn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            } 
            
            return dt;
        }

        /// <summary>
        /// Function which uses string concatenation to insert data into database
        /// </summary>
        /// <param name="name">Takes the student name as a parameter to insert into database</param>
        /// <param name="gpa">Takes the student GPA as a parameter to insert into database</param>
        public static void UnsafeAdd(string name, float gpa)
        {
            string sql = "INSERT INTO public.\"students\"(name,gpa) VALUES ('";
                sql += name + "'," + gpa + ");";
            MyQuery(sql);
        }

        /// <summary>
        /// Function utilizing a prepared query to insert data into database
        /// </summary>
        /// <param name="name">Takes the student name as a parameter to insert into database</param>
        /// <param name="gpa">Takes the student GPA as a parameter to insert into database</param>
        public static void SafeAdd(string name, float gpa)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(
                "Server=127.0.0.1; Port=5432; Database=Assignment_4; Integrated Security=true;"))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO public.\"students\"(name,gpa) VALUES (:name,:gpa);";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                    cmd.Parameters.Add(
                        new NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Text) { Value = name });

                    cmd.Parameters.Add(
                        new NpgsqlParameter("gpa", NpgsqlTypes.NpgsqlDbType.Real) { Value = gpa });

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    //// conn.Close();    //code analysis error that object gets disposed of automatically
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}