using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Usuario
    {

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public float Saldo { get; set; }

        public object Salvar()
        {
            Movimentacao mov = new Movimentacao();
            Conta conta = new Conta();
            
            try
            {
                MySqlConnection conn = new MySqlConnection(Conexao.Dados);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into contas(NOME, EMAIL, SENHA, NUMEROCONTA, SALDO) values(@NOME, @EMAIL, @SENHA, @NUMEROCONTA, @SALDO)", conn);

                cmd.Parameters.AddWithValue("NOME", Nome);
                conta.Nome = Nome;
                cmd.Parameters.AddWithValue("EMAIL", Email);
                conta.Email = Email;
                cmd.Parameters.AddWithValue("SENHA", Senha);
                conta.Senha = Senha;
                
                Random numAleatorio = new Random();
                conta.NumeroConta = numAleatorio.Next(50000, 99999);
                cmd.Parameters.AddWithValue("NUMEROCONTA", conta.NumeroConta);

                cmd.Parameters.AddWithValue("SALDO", Saldo);
                conta.Saldo = Saldo;

                Conta dados = mov.RetornaPorNumeroConta(conta.NumeroConta);
                if(dados == null)
                {
                    conta = null;
                }
                cmd.ExecuteNonQuery();
                conn.Close();
                return conta;
            }
            catch (Exception)
            {
                conta = null;
                
                return conta;
            }
        }
    }   
}
