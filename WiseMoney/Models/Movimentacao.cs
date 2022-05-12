using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Movimentacao
    {
        public int Id_Mov { get; set; }
        public string Data { get; set; }
        public float Valor { get; set; }
        public string DC { get; set; }
        public int Id_Conta { get; set; }

        public static List<Movimentacao> Todos()
        {
            try
            {
                var lista = new List<Movimentacao>();
                MySqlConnection conn = new MySqlConnection(Conexao.Dados);
                MySqlCommand cmd = new MySqlCommand("select * from movimentacao", conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Movimentacao
                    {
                        Id_Mov = Convert.ToInt32(reader["ID_MOV"]),
                        Data = reader["DATA"].ToString(),
                        DC = reader["DC"].ToString(),
                        Valor = float.Parse(reader["VALOR"].ToString()),
                        Id_Conta = Convert.ToInt32(reader["ID_CONTA"])
                    });
                }
                reader.Close();
                conn.Close();
                return lista;
            }
            catch (Exception ex)
            {
                List<Movimentacao> erro = null;
                return erro;
            }

        }

        public static List<Movimentacao> ExtratoPorID(int ID_CONTA)
        {
            try
            {
                var lista = new List<Movimentacao>();
                MySqlConnection conn = new MySqlConnection(Conexao.Dados);
                MySqlCommand cmd = new MySqlCommand("SELECT * from movimentacao where ID_CONTA = @Id", conn);
                conn.Open();

                cmd.Parameters.AddWithValue("Id", ID_CONTA);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Movimentacao
                    {
                        Id_Mov = reader["ID_MOV"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_MOV"]),
                        Data = reader["DATA"] == DBNull.Value ? string.Empty : reader["DATA"].ToString(),
                        Valor = reader["VALOR"] == DBNull.Value ? 0 : float.Parse(reader["VALOR"].ToString()),
                        DC = reader["DC"] == DBNull.Value ? string.Empty : reader["DC"].ToString(),
                        Id_Conta= reader["ID_CONTA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CONTA"])
                    });
                }
                conn.Close();
                return lista;
            }
            catch (Exception)
            {
                List<Movimentacao> erro = null;
                return erro;
            }

        }

        public Movimentacao Salvar()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Conexao.Dados);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into movimentacao(DATA, VALOR, DC, ID_CONTA) values(@DATA, @VALOR, @DC, @ID_CONTA)", conn);

                cmd.Parameters.AddWithValue("DATA", DateTime.Now);
                cmd.Parameters.AddWithValue("VALOR", Valor);
                cmd.Parameters.AddWithValue("DC", DC);
                cmd.Parameters.AddWithValue("ID_CONTA", Id_Conta);

                cmd.ExecuteNonQuery();
                conn.Close();
                return this;
            }
            catch (Exception)
            {
                Movimentacao erro = null;
                return erro;
            }

        }

        public Conta RetornaPorNumeroConta(int numeroConta)
        {
            try
            {
                Conta conta = null;
                MySqlConnection conn = new MySqlConnection(Conexao.Dados);
                MySqlCommand cmd = new MySqlCommand("SELECT ID, NOME, EMAIL, SENHA, NUMEROCONTA, SALDO FROM contas where NUMEROCONTA = @numeroConta", conn);
                conn.Open();

                cmd.Parameters.AddWithValue("numeroConta", numeroConta);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    conta = new Conta()
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
                return conta;
            }
            catch (Exception)
            {
                Conta erro = null;
                return erro;
            }

        }

        public Conta TransferenciaBancaria(float valor, Conta ccA , int numeroContaB)
        {
            Movimentacao movimentacao = new Movimentacao();
            if (ccA.Saldo >= valor)
            {
                
                Conta ccB = RetornaPorNumeroConta(numeroContaB);
                ccA.Saldo -= valor;
                ccB.Saldo += valor;
                ccA.AlterarConta(ccA.Id, ccA);
                ccB.AlterarConta(ccB.Id, ccB);

                //salva movimentação da conta A
                movimentacao.Id_Conta = ccA.Id;
                movimentacao.DC = "D";
                movimentacao.Valor = valor;
                movimentacao.Salvar();

                //salva movimentação da conta B
                movimentacao.Id_Conta = ccB.Id;
                movimentacao.DC = "C";
                movimentacao.Valor = valor;
                movimentacao.Salvar();

            }
            return ccA;
        }



    }

    
}
