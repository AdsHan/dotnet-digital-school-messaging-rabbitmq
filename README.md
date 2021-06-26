# core-digital-school-messaging-rabbitmq

Aplicação .Net Core 5.0 Web API desenvolvida com o objetivo didático de demonstrar o funcionamento de mensageria utilizando o RabbitMQ como messaging broker. Foi utilizada uma abordagem simplificada que não levou em conta a atomicidade.

Cenário: Aplicativo que envia um resumo diário para os pais/guardiões do dia a dia do aluno. Quando o serviço de Aluno recebe a requisição para incluir um novo aluno, são disparadas mensagens para que o serviço de autenticação já crie os usuários de autenticação (Identity) dos pais/guardiões.

# Este projeto contém:

- Arquitetura Microsserviços;
- RabbitMQ como messaging broker;
- Message Bus;
- Pattern CQRS com MediatR;
- Pattern Repository;
- Fluent Validation;
- Mapeamento das entidades por Fluent API;
- Entity Framework (EF) Core; 
- Persistência em SQLServer;
- DTO e AutoMapper;
- Versionamento da API;
- Swagger/Swagger UI;

# Sobre Microsserviços:
- O projeto é didático, logo aceita-se que os serviços não tenham BD heterogêneos e que estes persistam no mesmo.

# Como executar:
- Clonar / baixar o repositório em seu workplace.
- Baixar o .Net Core SDK e o Visual Studio / Code mais recentes.
- Intalar o RabbitMQ local ou em container

# Sobre
Este projeto foi desenvolvido por Anderson Hansen sob [MIT license](LICENSE).
