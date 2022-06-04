using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ASPNET_Core_2_1.Models;
using System.Data;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;

namespace ASPNET_Core_2_1.DBLAYER
{
    public class DBClass: IEmployeRepository
    {
        public void set_data(string ad,string soyad,string yas)
        {

            string FName1 = "Files\\user.txt";
            string FLine = ad + "|" + soyad + "|" + yas;
            if (File.Exists(FName1))
            {
                using (StreamWriter FWrite = File.AppendText(FName1))
                {
                    FWrite.WriteLine(FLine);
                }
            }
            else
            {
                StreamWriter FWrite = new StreamWriter(FName1);
                try
                {
                    FWrite.WriteLine(FLine);
                }
                catch
                {
                }
                finally { FWrite.Close(); }
            }
        }

        public void set_datadb(string ad, string soyad, string yas)
        {
            string sql = "INSERT INTO EMPLOYEE (firstname, lastname, yas) Values ('" + ad+"', '"+soyad+"', "+yas+")";

            using (IDbConnection conn = Connection)
            {
                string s = conn.ConnectionString;
                conn.Open();
                var affectedRows = conn.Execute(sql);
            }
        }
        public void setup_datadb(string idn,string ad, string soyad, string yas)
        {
            string sql = "UPDATE EMPLOYEE set  firstname='" + ad + "',lastname='" + soyad + "', yas=" + yas + " where id='" + idn + "'";

            using (IDbConnection conn = Connection)
            {
                string s = conn.ConnectionString;
                conn.Open();
                var affectedRows = conn.Execute(sql);
            }
        }
        public void setdel_datadb(string idn)
        {
            string sql = "DELETE FROM EMPLOYEE  where id='" + idn + "'";

            using (IDbConnection conn = Connection)
            {
                string s = conn.ConnectionString;
                conn.Open();
                var affectedRows = conn.Execute(sql);
            }
        }
        public ListIsci LI(string surl)
        {
            ListIsci li = new ListIsci();
            string [] l = File.ReadAllLines(surl);
            for (int i = 0; i < l.Length; i++)
            {
                string ad = "ad";
                string soyad = "soyad";
                string yas = "yas";
                Isci isc = new Isci();
                isc.firstname = ad;
                isc.lastname = soyad;
                isc.yas = yas;
                li.LI.Add(isc);
            }
            return li;
        }
        private readonly IConfiguration _config;

        public DBClass(IConfiguration config)
        {
            _config = config;
        }
        public DBClass()
        {}
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("MyConnectionString"));
            }
        }
        public List<Isci> GetAll()
        {
            List<Isci> isl = new List<Isci>();
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, FirstName, LastName, YAS from Employee";
                string s = conn.ConnectionString; conn.Open();
                isl= conn.Query<Isci>(sQuery).ToList();
                
            }
            return isl;
        }
        public Isci GetIsci(string id)
        {
            Isci isc = new Isci();
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT ID, FirstName, LastName, YAS FROM Employee where id='"+id+"'";
                string s = conn.ConnectionString;conn.Open();
                List<Isci> isl = new List<Isci>();
                isl = conn.Query<Isci>(sQuery).ToList();
                if (isl.Count > 0)
                {
                    isc = isl[0];
                }
            }
            return isc;
        }
        public ListIsci LIDb()
        {
            ListIsci li = new ListIsci();
            List<Isci> isl = GetAll();
            li.LI = isl;
            return li;
        }
        public bool GetQeydiyyat(string name, string email, string parol)
        {
            string err = "";
            bool s = false;
            try
            {
                using (IDbConnection Con = Connection)
                {
                    var model = "select * from tblQeydiyyat where ad='" + name + "' and Email='" + email + "' and parol=" + parol + "";
                    Con.Open();
                    var result = Con.ExecuteReader(model);

                    if (result.Read())
                    {
                        s = true;
                    }
                }
               
            }
            catch(Exception ex)
            {
                err = ex.Message;
            }
            return s;
        }
        public void setAdd_datadb(string ad, string soyad, string Ataad, string Dogum_Tarixi, string email,string parol,string elaqenom,string yas)
        {
            string sql = "INSERT INTO tblQeydiyyat Values ('" + ad + "', '" + soyad + "', '" + Ataad + "','" + Dogum_Tarixi + "','" + email + "'," + parol + "," + elaqenom + ","+yas+")";

            using (IDbConnection conn = Connection)
            {
                string s = conn.ConnectionString;
                conn.Open();
                var affectedRows = conn.Execute(sql);
            }
        }
        public bool setCheck_datadb(string email)
        {
            string err = "";
            bool s = false;
            try
            {
                string sql = "select * from tblQeydiyyat where email='" + email + "'";
                using (IDbConnection conn = Connection)
                {
                    conn.Open();
                    var affectedRows = conn.ExecuteReader(sql);
                    if(affectedRows.Read())
                    {
                        s = true;
                    }
                }
            }
            catch(Exception ex)
            { err = ex.Message; }
            return s;
           
        }
        public Qeydiyyat GetParol(string Code)
        {
            Qeydiyyat qy = new Qeydiyyat();
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT Parol FROM tblQeydiyyat where elaqe_nomresi='" + Code + "'";
                string s = conn.ConnectionString; conn.Open();
                List<Qeydiyyat> isl = new List<Qeydiyyat>();
                isl = conn.Query<Qeydiyyat>(sQuery).ToList();
                if (isl.Count > 0)
                {
                    qy = isl[0];
                }
            }
            return qy;
        }
        //public bool GetParol_datadb(string code)
        //{
        //    string err = "";
        //    bool s = false;
        //    try
        //    {
        //        string sql = "select parol from tblQeydiyyat where elaqe_nomresi='" + code + "'";
        //        using (IDbConnection conn = Connection)
        //        {
        //            conn.Open();
        //            var affectedRows = conn.ExecuteReader(sql);
        //            if (affectedRows.Read())
        //            {
        //                s = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    { err = ex.Message; }
        //    return s;

        //}


        //public void GetPasswordb( string email)
        //{
        //    string sql = "select Parol tblQeydiyyat Values ('" + ad + "', '" + soyad + "', '" + Ataad + "','" + Dogum_Tarixi + "','" + email + "'," + parol + "," + elaqenom + "," + yas + ")";

        //    using (IDbConnection conn = Connection)
        //    {
        //        string s = conn.ConnectionString;
        //        conn.Open();
        //        var affectedRows = conn.Execute(sql);
        //    }
        //}
        //public void GetQeat()
        //{

        //    using (IDbConnection Con = Connection)
        //    {
        //        var model  = "select id, Cinsi from tblCins";
        //        Con.Open();
        //        var result = Con.ExecuteReader(model);

        //        if (result.Read())
        //        {

        //        }
        //    }

        //}

        //public List<Cinsler> GetCins()
        //{
        //    List<Cinsler> lc = new List<Cinsler>();
        //    using (IDbConnection conn = Connection)
        //    {
        //        string sQuery = "select id, Cinsi from tblCins";
        //        conn.Open();
        //        lc = conn.Query<Cinsler>(sQuery).ToList();

        //    }
        //    return lc;
        //}
        //public ListCins LIC()
        //{
        //    ListCins li = new ListCins();
        //    List<Cinsler> isl = GetCins();
        //    li.LCins = isl;
        //    return li;
        //}
        //public bool GetIstifadeci(string login, string parol)
        //{
        //    bool s = false;
        //    using (IDbConnection Con = Connection)
        //    {
        //        var model = "select Login,Parol from tblIstifadeci where login='" + login + "' and parol='" + parol + "'";
        //        Con.Open();
        //        var result = Con.ExecuteReader(model);

        //        if (result.Read())
        //        {
        //            s = true;
        //        }
        //    }
        //    return s;
        //}
    }
}
