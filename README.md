# Task Manager API

## Sobre o projeto

Task Manager API é uma API REST para gerenciamento de tarefas, permitindo criar, listar, consultar, iniciar, completar, cancelar, atualizar e remover tarefas.

O projeto é construído em .NET, utilizando ASP.NET Core Web API para expor os endpoints HTTP e Entity Framework Core como ORM para acesso a dados. O banco de dados utilizado é o SQLite, com o arquivo de dados armazenado localmente (`tasks.db`).

## Como rodar o projeto

Pré-requisitos:

- .NET SDK 10 instalado

Passos, a partir da raiz do repositório:

```bash
cd task-manager-api

dotnet restore

dotnet ef database update

dotnet run
```

Caso a ferramenta `dotnet-ef` não esteja instalada, instale com `dotnet tool install --global dotnet-ef`.

Por padrão, a API sobe nos seguintes endereços (conforme `Properties/launchSettings.json`):

- HTTP: `http://localhost:5161`
- HTTPS: `https://localhost:7126`

## Como executar as APIs

Na raiz do projeto existe a pasta `collections`, com duas collections prontas para testar os endpoints da API:

- `bruno-collection.zip`: collection para o cliente HTTP Bruno. Fica a indicação de uso do Bruno como cliente HTTP para testar os endpoints.
- `postman-collection.json`: collection no formato do Postman, pronta para importação.

## Camadas da arquitetura

O projeto segue uma separação em camadas:

- **Controller** (`Controllers/TaskController.cs`): recebe as requisições HTTP, delega a execução para o Use Case correspondente e retorna a resposta.
- **Use Case** (`Domain/UseCases/`): concentra a regra de aplicação de cada operação (ex.: `CreateTask`, `StartTask`, `CompleteTask`, `CancelTask`, `UpdateTask`, `DeleteTask`, `GetAllTasks`, `GetTaskById`), cada um dependendo de `ITaskRepository` para acessar os dados.
- **Entidade + Repositório** (`Domain/Entities/TaskEntity.cs`, `Domain/Interfaces/ITaskRepository.cs`, `Infrastructure/Repositories/TaskRepository.cs`): a entidade concentra as regras do domínio da tarefa, e o repositório implementa a persistência através do Entity Framework Core (`Infrastructure/Persistence/AppDbContext.cs`).

### Tratamento de exceções de domínio

As regras de negócio que são violadas lançam exceções de domínio, representadas pela classe base `DomainException` (`Domain/Exceptions/DomainException.cs`) e suas especializações (`InvalidTaskDataException`, `InvalidTaskStateException`, `TaskNotFoundException`).

Essas exceções são capturadas por um `DomainExceptionFilter` (`Infrastructure/ExceptionHandling/DomainExceptionFilter.cs`), aplicado ao controller, que as converte em uma resposta HTTP com o status code e a mensagem de erro apropriados. Esse filtro é implementado a partir do mecanismo de filtros de exceção do ASP.NET Core (`IExceptionFilter`), documentado em: https://learn.microsoft.com/pt-br/dotnet/api/microsoft.aspnetcore.mvc.filters.exceptionfilterattribute?view=aspnetcore-10.0

## Regras de negócio implementadas

As regras abaixo estão implementadas em `Domain/Entities/TaskEntity.cs` (a entidade é responsável por proteger seu próprio estado) e são acionadas pelos endpoints de `Controllers/TaskController.cs`:

- **Título obrigatório**: uma tarefa não pode ser criada nem atualizada com título vazio ou apenas espaços em branco (`POST /tasks` e `PUT /tasks/{id}`). Violação lança `InvalidTaskDataException` (HTTP 400).
- **Data prevista de conclusão não pode ser anterior à data atual**: validado tanto na criação quanto na atualização, comparando a data informada com a data atual (UTC). Violação lança `InvalidTaskDataException` (HTTP 400).
- **Status restrito a um conjunto fechado de valores**: `Pendente`, `Em andamento`, `Concluída` e `Cancelada` (enum `TaskStatus`). O status não é um campo editável livremente — só muda através dos endpoints dedicados (`/start`, `/complete`, `/cancel`), o que evita que a API receba um status inválido ou arbitrário.
- **Transições de status controladas**:
  - `PATCH /tasks/{id}/start`: só é permitido iniciar tarefas que estejam `Pendente`. Caso contrário, lança `InvalidTaskStateException` (HTTP 409).
  - `PATCH /tasks/{id}/complete`: só é permitido completar tarefas `Pendente` ou `Em andamento`. Caso contrário, lança `InvalidTaskStateException` (HTTP 409).
  - `PATCH /tasks/{id}/cancel`: uma tarefa já `Cancelada` não pode ser cancelada novamente (`InvalidTaskStateException`, HTTP 409). Tarefas em qualquer outro status, incluindo `Concluída`, podem ser canceladas — essa foi uma decisão consciente, já que o enunciado não veda esse caso.
- **Uma tarefa concluída não pode voltar para "Pendente"**: como o status só é alterado pelos endpoints acima e não existe nenhuma operação que leve uma tarefa de volta a `Pendente`, essa regra é garantida por construção (não há um "des-complete").
- **Data de conclusão registrada automaticamente**: ao completar uma tarefa (`/complete`), o campo `CompletedAt` é preenchido com a data/hora atual (UTC) pela própria entidade.
- **Tarefa inexistente**: qualquer operação sobre um ID que não existe (`GET /tasks/{id}`, `PUT`, `DELETE`, `/start`, `/complete`, `/cancel`) lança `TaskNotFoundException` (HTTP 404).

## O que eu melhoraria se tivesse mais tempo

- Autenticação (para restringir quem pode criar/alterar tarefas).
- Paginação e filtros no `GET /tasks` (por status, por período de conclusão prevista, etc.).
- Empacotar tudo em um `docker-compose.yml`, subindo a API junto de um MySQL ou MariaDB no lugar do SQLite.
- Um log de auditoria (quem criou/alterou/cancelou cada tarefa e quando), possivelmente em MongoDB.
