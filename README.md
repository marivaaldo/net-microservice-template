# net-microservice-template
> A abordagem desse template segue os princípios de Clean Architecture, DDD (Domain-Driven Design) e práticas modernas de desenvolvimento de software.

## Estrutura de Projetos

```
├── NetMicroserviceTemplate.API
│   ├── Configuration
│   ├── Endpoints
│   ├── Middlewares
├── NetMicroserviceTemplate.Application
│   ├── Contracts
│   ├── DTOs
│   ├── Services
│   ├── UseCases
│   ├── ViewModels
├── NetMicroserviceTemplate.Domain
│   ├── Aggregates
│   ├── Contracts
│   │   └── Repositories
│   ├── Entities
│   ├── Events
│   ├── Services
│   ├── ValueObjects
├── NetMicroserviceTemplate.Infrastructure.Data
│   ├── Context
│   ├── EntityConfigurations
│   ├── Repositories
├── NetMicroserviceTemplate.Tests
│   ├── IntegrationTests
│   ├── Mocks
│   ├── UnitTests
```

Estrutura do Projeto
A solução é organizada em múltiplos projetos que seguem a separação de responsabilidades, promovendo modularidade e facilidade de manutenção.

## NetMicroserviceTemplate.Api
> Projeto principal que expõe endpoints RESTful.

* **Configuration**: Configurações de inicialização da aplicação (ex.: Swagger, CORS).
* **Endpoints**: Contém os controladores da API responsáveis por receber as requisições HTTP e encaminhá-las para os use cases.
* **Middlewares**: Implementações de middlewares para manipulação de solicitações HTTP (ex.: tratamento de erros, autenticação).

## NetMicroserviceTemplate.Application
> Camada de aplicação que contém a lógica de orquestração dos casos de uso.

* **Contracts**: Interfaces para serviços externos ou dependências, facilitando a injeção de dependências.
* **DTOs** (Data Transfer Objects): Estruturas de dados utilizadas para transferência de informações entre camadas.
* **Services**: Contém as implementações das `Interfaces`
* **UseCases**: Implementações dos casos de uso que coordenam a interação entre a camada de domínio e a camada de infraestrutura.
* **ViewModels**: Objetos utilizados especificamente em retorno de endpoints

## NetMicroserviceTemplate.Domain
> Camada de domínio, contendo as regras de negócios principais e modelos.

* **Aggregates**: Raízes de agregados que agrupam entidades relacionadas com regras de consistência.
* **Contracts**: Interfaces que definem contratos para repositórios e serviços do domínio.
* **Entities**: Definições de entidades de negócio que representam o núcleo do domínio.
* **Events**: Eventos de domínio
* **Services**: Serviços de domínio que encapsulam lógica complexa que não se encaixa diretamente em uma entidade.
* **ValueObjects**: Objetos de valor que representam conceitos imutáveis e sem identidade única.

## NetMicroserviceTemplate.Infrastructure
> Camada que implementa a persistência de dados e integrações externas.

* **EntityConfigurations**: Configurações de mapeamento do Entity Framework Core.
* **Repositories**: Implementações dos repositórios que interagem com o banco de dados.

## NetMicroserviceTemplate.Tests
> Projeto de testes automatizados para garantir a qualidade do código.

* **UnitTests**: Testes unitários focados em métodos individuais das classes.
* **IntegrationTests**: Testes de integração para verificar a interação entre diferentes componentes do sistema.
* **Mocks**: Implementações simuladas de dependências para facilitar os testes.

## Dependências entre os Projetos
- **NetMicroserviceTemplate.API** depende de **NetMicroserviceTemplate.Application** e **NetMicroserviceTemplate.Infrastructure**.
- **NetMicroserviceTemplate.Application** depende de **NetMicroserviceTemplate.Domain**.
- **NetMicroserviceTemplate.Infrastructure** depende de **NetMicroserviceTemplate.Domain** e **NetMicroserviceTemplate.Application** para implementar repositórios e serviços.

# Conceitos-Chave

## Clean Architecture
Separa a lógica de negócios do código de infraestrutura e apresentação, promovendo independência entre as camadas.

## DDD (Domain-Driven Design)
Define as entidades, agregados, serviços de domínio e interfaces de repositório com base no modelo do domínio.

## Testes Automatizados
Garante que as funcionalidades do microserviço funcionem conforme esperado, minimizando a introdução de bugs em futuras alterações.

## Resumo
Essa estrutura modular proporciona uma base sólida para a construção de microserviços escaláveis, testáveis e de fácil manutenção.

## Boas Práticas
- Mantenha a separação de responsabilidades clara entre as camadas.
- Use DTOs para a comunicação entre a API e a aplicação para proteger a camada de domínio.
- Certifique-se de que os testes cubram tanto a lógica de negócio (unitários) quanto a integração entre serviços (de integração).