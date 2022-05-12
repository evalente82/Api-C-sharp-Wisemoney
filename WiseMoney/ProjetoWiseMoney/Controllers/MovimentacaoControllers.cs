using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoWiseMoney.Controllers
{
    public class MovimentacaoControllers : ControllerBase
    {
        [HttpGet]
        [Route("movimentacao")]
        public List<Movimentacao> Index()
        {
            return Movimentacao.Todos();
        }

        [HttpGet]
        [Route("extrato/{id}")]
        public List<Movimentacao> Extrato(int id)
        {
            return Movimentacao.ExtratoPorID(id);
        }

        [HttpPost]
        [Route("criar")]
        public Movimentacao Criar([FromBody] Movimentacao movimentacao)
        {
            return movimentacao.Salvar();
        }

        [HttpPost]
        [Route("tranferir/{id}/{valor}/{numeroConta}")]
        public Conta Tranferir( int id, float valor, int numeroConta)
        {
            Movimentacao transferir = new Movimentacao();

            Conta cliente = new Conta();
            Conta ccA = cliente.RetornaPorId(id);

            return transferir.TransferenciaBancaria(valor, ccA, numeroConta);
        }


    }
}
