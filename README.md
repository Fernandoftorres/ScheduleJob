# Schedule Job

Tecnologias utilizadas:

 - Net Core 3.1
 - C#
 - FluentValidation
 - Swagger
 
 
 Passos para chegar no resultado do desafio.
 
 - Abrir a solution numa versão do visual studio que suporte o framework net core 3.1
 - Executar a aplicação.
 - Vai abrir uma tela do swagger com somente um endpoint: /schedule
 - Informar os dados abaixo:
 {
  "dataInicio": "2019-11-10 09:00:00",
  "dataFim": "2019-11-11 12:00:00",
  "jobsParaExecucao": [
  {
    "id": 1,
    "descricao": "Importação de arquivos de fundos",
    "dataMaximaConclusao": "2019-11-10 12:00:00",
    "tempoEstimado": 2
  },
  {
    "id": 2,
    "descricao": "Importação de dados da Base Legada",
    "dataMaximaConclusao": "2019-11-11 12:00:00",
    "tempoEstimado": 4
  },
  {
    "id": 3,
    "descricao": "Importação de dados de integração",
    "dataMaximaConclusao": "2019-11-11 08:00:00",
    "tempoEstimado": 6
  }
  ]
}
