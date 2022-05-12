using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int NumeroConta { get; set; }
        public float Saldo { get; set; }

        public static List<Conta> Todos()
        {
            try
            {
                var lista = new List<Conta>();
                MySqlConnection conn = new MySqlConnection(Conexao.Dados);
                MySqlCommand cmd = new MySqlCommand("select * from contas", conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Conta
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        Nome = reader["NOME"].ToString(),
                        Email = reader["EMAIL"].ToString(),
                        Senha = reader["SENHA"].ToString(),
                        NumeroConta = Convert.ToInt32(reader["NUMEROCONTA"]),
                        Saldo = float.Parse(reader["SALDO"].ToString())
                    });
                }
                reader.Close();
                conn.Close();
                return lista;
            }
            catch (Exception ex)
            {
                List<Conta> erro = null;
                return erro;
            }
        }

        public Conta Delete(int id)
        {
            try
            {
                bool resultado = false;

                MySqlConnection conn = new MySqlConnection(Conexao.Dados);
                MySqlCommand cmd = new MySqlCommand("delete from contas where Id = @Id", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("Id", id);
                int i = cmd.ExecuteNonQuery();
                resultado = i > 0;
                conn.Close();
                if (!resultado)
                {
                    Conta erro = null;
                    return erro;
                }
                return this;
            }
            catch (Exception)
            {
                Conta erro = null;
                return erro;
            }

        }

        public Conta RetornaPorId(int Id)
        {
            try
            {
                Conta conta = null;
                MySqlConnection conn = new MySqlConnection(Conexao.Dados);
                MySqlCommand cmd = new MySqlCommand("SELECT ID, NOME, EMAIL, SENHA, NUMEROCONTA, SALDO FROM contas where ID = @Id", conn);
                conn.Open();

                cmd.Parameters.AddWithValue("ID", Id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    conta = new Conta()
                    {
                        Id = reader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID"]),
                        Nome = reader["NOME"] == DBNull.Value ? string.Empty : reader["NOME"].ToString(),
                        Email= reader["EMAIL"] == DBNull.Value ? string.Empty : reader["EMAIL"].ToString(),
                        Senha = reader["SENHA"] == DBNull.Value ? string.Empty : reader["SENHA"].ToString(),
                        NumeroConta = reader["NUMEROCONTA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUMEROCONTA"]),
                        Saldo = reader["SALDO"] == DBNull.Value ? 0 : float.Parse( reader["SALDO"].ToString())
                    };
                }
                conn.Close();
                return conta;
            }
            catch (Exception)
            {
                Conta erro = null;
                return erro;
            }
        }        

        public Conta Salvar()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Conexao.Dados);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into contas(NOME, EMAIL, SENHA, NUMEROCONTA, SALDO) values(@NOME, @EMAIL, @SENHA, @NUMEROCONTA, @SALDO)", conn);

                cmd.Parameters.AddWithValue("NOME", Nome);
                cmd.Parameters.AddWithValue("EMAIL", Email);
                cmd.Parameters.AddWithValue("SENHA", Senha);
                cmd.Parameters.AddWithValue("NUMEROCONTA", NumeroConta);
                cmd.Parameters.AddWithValue("SALDO", Saldo);

                cmd.ExecuteNonQuery();
                conn.Close();
                return this;
            }
            catch (Exception)
            {
                Conta erro = null;
                return erro;
            }
        }

        public Conta AlterarConta(int id, Conta conta)
        {
            bool resultado = false;
            if (conta == null) throw new ArgumentNullException("conta");
            if (id == 0) throw new ArgumentNullException("id");

            MySqlConnection conn = new MySqlConnection(Conexao.Dados);
            MySqlCommand cmd = new MySqlCommand("update contas set NOME = @NOME, EMAIL = @EMAIL, SENHA = @SENHA, NUMEROCONTA = @NUMEROCONTA,SALDO = @SALDO where ID = @ID", conn);
            conn.Open();
            cmd.Parameters.AddWithValue("ID", id);
            cmd.Parameters.AddWithValue("NOME", conta.Nome);
            cmd.Parameters.AddWithValue("EMAIL", conta.Email);
            cmd.Parameters.AddWithValue("SENHA", conta.Senha);
            cmd.Parameters.AddWithValue("NUMEROCONTA", conta.NumeroConta);
            cmd.Parameters.AddWithValue("SALDO", conta.Saldo);
            int i = cmd.ExecuteNonQuery();
            resultado = i > 0;
            conn.Close();

            if (!resultado)
            {
                Conta erro = null;
                return erro;
            }
            return this;
        }

        public Conta UsuarioLogin(string email, string senha)
        {
            try
            {
                Conta usuario = null;
                MySqlConnection conn = new MySqlConnection(Conexao.Dados);
                MySqlCommand cmd = new MySqlCommand("SELECT ID, NOME, EMAIL, SENHA, NUMEROCONTA, SALDO FROM contas where EMAIL = @EMAIL and SENHA = @SENHA", conn);
                conn.Open();

                cmd.Parameters.AddWithValue("EMAIL", email);
                cmd.Parameters.AddWithValue("SENHA", senha);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuario = new Conta()
                    {
                        Id = reader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID"]),
                        Nome = reader["NOME"] == DBNull.Value ? string.Empty : reader["NOME"].ToString(),
                        Email = reader["EMAIL"] == DBNull.Value ? string.Empty : reader["EMAIL"].ToString(),
                        Senha = reader["SENHA"] == DBNull.Value ? string.Empty : reader["SENHA"].ToString(),
                        NumeroConta = reader["NUMEROCONTA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUMEROCONTA"]),
                        Saldo = reader["SALDO"] == DBNull.Value ? 0 : float.Parse(reader["SALDO"].ToString())
                    };
                }
                conn.Close();
                return usuario;
            }
            catch (Exception)
            {
                Conta erro = null;
                return erro;
            }
        }        
    }
}
