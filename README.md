# core-digital-school-messaging-rabbitmq

Aplicação .Net Core 5.0 Web API desenvolvida com o objetivo didático de demonstrar o funcionamento de mensageria utilizando o RabbitMQ como messaging broker.

# Este projeto contém:

- Arquitetura Microsserviços;
- RabbitMQ como messaging broker;
- Message Bus;
- Atomicidade com o armazenamento dos Integration Events;
- Pattern CQRS com MediatR;
- Pattern Repository;
- Fluent Validation;
- Mapeamento das entidades por Fluent API;
- Entity Framework (EF) Core; 
- Persistência em SQLServer;
- DTO e AutoMapper;
- Versionamento da API;
- Swagger/Swagger UI;
- Testes Unitários com Moq e xUnit (Padrões AAA e Given-When-Then)

# Sobre Microsserviços:
- O projeto é didático, logo aceita-se que os serviços não tenham BD heterogêneos e que estes persistam no mesmo.

# Como executar:
- Clonar / baixar o repositório em seu workplace.
- Baixar o .Net Core SDK e o Visual Studio / Code mais recentes.
- Instalar o RabbitMQ local ou em container.
- Nas configurações da API, ajustar o PATH do XML do Swagger.

# Sobre
Este projeto foi desenvolvido por Anderson Hansen sob [MIT license](LICENSE).
