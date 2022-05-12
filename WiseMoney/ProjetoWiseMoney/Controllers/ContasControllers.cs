using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoWiseMoney.Controllers
{
    [Route("contas/")]
    [ApiController]
    public class ContasControllers : ControllerBase
    {
        [HttpGet]
        public List<Conta> Index()
        {
            return Conta.Todos();
        }

        [HttpGet]
        [Route("{id}")]
        public Conta RetornaPorIdCliente(int id)
        {
            Conta conta = new Conta();
            var dados = conta.RetornaPorId(id);

            return dados;
        }

        [HttpPost]
        [Route("criar")]
        public object Criar([FromBody] Usuario usuario)
        {
            return usuario.Salvar();
        }

        [HttpDelete]
        [Route("{id}")]
        public void Excluir(int id)
        {
            try
            {
                Conta conta = new Conta();
                conta.Delete(id);

            }
            catch (Exception)
            {

                return;
            }

        }

        [HttpPut]
        [Route("{id}")]
        public Conta Alterar(int id, Conta conta)
        {

            var dados = conta.AlterarConta(id, conta);

            return dados;
        }

        [HttpGet]
        [Route("login/{email}/{senha}")]
        public Conta Login(string email, string senha)
        {
            Conta conta = new Conta();
            var dados = conta.UsuarioLogin(email, senha);

            return dados;
        }

        [HttpGet]
        [Route("saldo/{id}")]
        public float Saldo(int id)
        {
            Conta meuSaldo = new Conta();
            Conta dados = meuSaldo.RetornaPorId(id);
            return dados.Saldo;
        }
    }
}
