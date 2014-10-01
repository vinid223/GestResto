using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immobilus.Logic.Services.Helpers
{
    public class MySqlConnexion
    {

        private static readonly string CONNECTION_STRING;
        private MySqlConnection connection;
        private MySqlTransaction transaction;


        static MySqlConnexion()
        {
            CONNECTION_STRING = ConfigurationManager.ConnectionStrings["MySqlConnexion"].ConnectionString;
        }

        public MySqlConnexion()
        {
            try
            {
                connection = new MySqlConnection(CONNECTION_STRING);
            }
            catch (MySqlException)
            {
                throw;
            }
        }


        private bool Open()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                    return true;

                connection.Open();
                return true;
            }
            catch (MySqlException)
            {
                throw;
            }

        }

        public void OpenWithTransaction()
        {
            try
            {
                if (Open())
                {
                    transaction = connection.BeginTransaction();
                }
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        private bool Close()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException)
            {
                throw;
            }
        }



        public void Commit()
        {
            try
            {
                transaction.Commit();
                transaction = null;
                connection.Close();
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public void Rollback()
        {
            try
            {
                transaction.Rollback();
                transaction = null;
                connection.Close();
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public int NonQuery(string query)
        {
            int nbResultat = 0;
            try
            {
                if (Open() || connection.State == ConnectionState.Open)
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    nbResultat = command.ExecuteNonQuery();
                }

                return nbResultat;
            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                if (transaction == null)
                    Close();
            }
        }


        public DataSet Query(string query)
        {

            DataSet dataset = new DataSet();

            try
            {
                if (Open() || transaction != null)
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(query, connection);
                    adapter.Fill(dataset);


                }
                return dataset;

            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                Close();
            }

        }


        public DataSet StoredProcedure(string query, IList<MySqlParameter> parameters = null)
        {
            DataSet dataset = new DataSet();

            try
            {
                if (Open() || transaction != null)
                {

                    MySqlCommand commande = new MySqlCommand(query, connection);
                    commande.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        commande.Parameters.AddRange(parameters.ToArray());
                    }
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = commande;
                    adapter.Fill(dataset);


                }
                return dataset;

            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                if (transaction == null)
                    Close();
            }
        }



    }
}
