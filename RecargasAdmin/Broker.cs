using System;
using NLog;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Broker
{
	public Broker()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // PARA ACCESS
    // private System.Data.OleDb.OleDbConnection getconnection()
    // {
    // web
    // string connAct = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= d:\HOME\KOLLECTINFO.COM\Gonur\App_Data\Gonur.mdb";
    // local
    // string connAct = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= c:\Logics\Gonur\App_Data\Gonur.mdb";
    // System.Data.OleDb.OleDbConnection cnx = new System.Data.OleDb.OleDbConnection(connAct);
    // return cnx;
    // }

    private static Logger logger;

    #region "Logging"
    public static void Log(char type, string proc, string msg, [CallerMemberName] string memberName = "")
    {
        if (logger == null)
            logger = LogManager.GetCurrentClassLogger();
        logger.Debug("Thread: " + Thread.CurrentThread.Name + ". Caller: " + memberName + ". Message: " + proc + "-" + msg + "\n");
    }
    #endregion

    // PARA SQLSERVER
    public System.Data.SqlClient.SqlConnection getconnection()
    {
        
        string ConnectionStr =
            ConfigurationManager.AppSettings["JPSConnectionString"];
            // @"Data Source=.\SQLEXPRESS;Initial Catalog=JPS;Persist Security Info=True;User ID=sa;Password=Qc@rg@!506";
        System.Data.SqlClient.SqlConnection cnx = new System.Data.SqlClient.SqlConnection(ConnectionStr);
        return cnx;
    }

    // PARA ACCESS
    // public System.Data.DataSet getdata(string OleDbSql)
    // {
        //System.Data.OleDb.OleDbDataAdapter dad = new System.Data.OleDb.OleDbDataAdapter(OleDbSql, this.getconnection());
        //System.Data.DataSet dset = new System.Data.DataSet();
        //dad.Fill(dset);
        //return dset;
    //}

    // PARA SQLSERVER
    public System.Data.DataSet getdata(string sql)
    {
        System.Data.SqlClient.SqlDataAdapter dad = new System.Data.SqlClient.SqlDataAdapter(sql, this.getconnection());
        dad.SelectCommand.CommandTimeout = 120;
        System.Data.DataSet dset = new System.Data.DataSet();
        dad.Fill(dset);
        return dset;
    }

    public string updatedata(string sql)
    {
        string result = "";

        // Para ACCESS
        // System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
        // System.Data.OleDb.OleDbConnection cnx = this.getconnection();
        // cnx.Open();
        // System.Data.OleDb.OleDbTransaction trn = cnx.BeginTransaction();

        // Para SQL SERVER
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        System.Data.SqlClient.SqlConnection cnx = this.getconnection();
        cnx.Open();
        System.Data.SqlClient.SqlTransaction trn = cnx.BeginTransaction();

        cmd.Connection = cnx;
        cmd.Transaction = trn;
        try
        {
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            trn.Commit();
        }
        catch (SystemException ex)
        {
            trn.Rollback();
            result = ex.Message;
        }
        finally
        {
            cnx.Close();
        }
        return result;
    }


    public System.Data.SqlClient.SqlTransaction BegTrans ( SqlConnection cnx )
    {
        return cnx.BeginTransaction();
    }

    public System.Data.DataSet getdataCnx(string sql, SqlConnection cnx, SqlTransaction trn )
    {
        var cmd = new SqlCommand(sql, cnx);
        cmd.CommandTimeout = 120;
        cmd.Transaction = trn;
        var dad = new SqlDataAdapter(cmd);

        // System.Data.SqlClient.SqlDataAdapter dad = new System.Data.SqlClient.SqlDataAdapter(sql, cnx);

        System.Data.DataSet dset = new System.Data.DataSet();
        dad.Fill(dset);
        return dset;
    }

    public string updatedataCnx(string sql, SqlConnection cnx, SqlTransaction trn )
    {
        string result = "";

        // Para SQL SERVER
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

        cmd.Connection = cnx;
        cmd.CommandTimeout = 120;
        cmd.Transaction = trn;
        try
        {
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            result = ex.Message;
        }
        catch (SystemException ex)
        {
            result = ex.Message;
        }
        return result;
    }

    public int insertdataCnx(string sql, SqlConnection cnx, SqlTransaction trn)
    {
        string result = "";
        int id = 0;

        // Para SQL SERVER
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

        cmd.Connection = cnx;
        cmd.CommandTimeout = 120;
        cmd.Transaction = trn;
        try
        {
            cmd.CommandText = sql;
            // cmd.ExecuteNonQuery();
            id = int.Parse(cmd.ExecuteScalar().ToString());
        }
        catch (SystemException ex)
        {
            result = ex.Message;
        }
        // return result;
        return id;
    }

    public void updatedataarray(System.Collections.ArrayList arr)
    {
        // Para ACCESS
        //System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
        //System.Data.OleDb.OleDbConnection cnx = this.getconnection();
        //cnx.Open();
        //System.Data.OleDb.OleDbTransaction trn = cnx.BeginTransaction();

        // Para SQL SERVER
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        System.Data.SqlClient.SqlConnection cnx = this.getconnection();
        cnx.Open();
        System.Data.SqlClient.SqlTransaction trn = cnx.BeginTransaction();

        cmd.Connection = cnx;
        cmd.Transaction = trn;
        try
        {
            foreach (string st in arr)
            {
                cmd.CommandText = st;
                cmd.ExecuteNonQuery();
            }
            trn.Commit();
        }
        catch (SystemException ex)
        {
            string x = ex.Message;
            trn.Rollback();
        }
        finally
        {
            cnx.Close();
        }
    }

    private System.Data.Odbc.OdbcConnection getODBCconnection()
    {
        System.Data.Odbc.OdbcConnection cnx = new System.Data.Odbc.OdbcConnection(@"Dsn=Brooklyn;dbq=BROOKLYN;codepage=1252");
        return cnx;
    }

    public System.Data.DataSet getOdbcData(string OdbcSql)
    {
        System.Data.Odbc.OdbcDataAdapter dad = new System.Data.Odbc.OdbcDataAdapter(OdbcSql, this.getODBCconnection());
        System.Data.DataSet dset = new System.Data.DataSet();
        dad.Fill(dset);
        return dset;
    }

    private System.Data.OleDb.OleDbConnection getMSAconnection()
    {
        System.Data.OleDb.OleDbConnection cnx = new System.Data.OleDb.OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Datos\Gonur\GonurLoad.mdb");
        return cnx;
    }

    public System.Data.DataSet getMSAData(string OleDbSql)
    {
        System.Data.OleDb.OleDbDataAdapter dad = new System.Data.OleDb.OleDbDataAdapter(OleDbSql, this.getMSAconnection());
        System.Data.DataSet dset = new System.Data.DataSet();
        dad.Fill(dset);
        return dset;
    }


}
