# Api-C-sharp-Wisemoney


Script Create Table contas
SELECT * FROM wisemoney.contas;CREATE TABLE `contas` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `NOME` varchar(50) NOT NULL,
  `EMAIL` varchar(100) NOT NULL,
  `SENHA` varchar(50) NOT NULL,
  `NUMEROCONTA` int NOT NULL,
  `SALDO` float DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

Script Create Table movimentacao
CREATE TABLE `movimentacao` (
  `ID_MOV` int NOT NULL AUTO_INCREMENT,
  `DATA` datetime DEFAULT NULL,
  `VALOR` float DEFAULT NULL,
  `DC` varchar(1) DEFAULT NULL,
  `ID_CONTA` int DEFAULT NULL,
  PRIMARY KEY (`ID_MOV`),
  KEY `Id_Conta da tabela Conta_idx` (`ID_CONTA`),
  CONSTRAINT `Id_Conta da tabela Conta` FOREIGN KEY (`ID_CONTA`) REFERENCES `contas` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


Criar um projeto de API em c# para gestão de contas bancárias.
Requisitos:
- Criar conta com nome, e-mail e senha e gerar o número da conta aleatoriamente 
- Login com e-mail e senha
- Obter o saldo da conta
- Obter extrato da conta
- Transferir valores entre contas

Diferenciais:
- Documentação via Swagger
- Arquivo de build do Docker (Dockerfile ou compose.yaml)
- Arquitetura baseada em microserviços

Entrega:
- Disponibilizar o código fonte em um repositório público no Github




