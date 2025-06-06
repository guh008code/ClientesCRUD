# Projeto ClientesCRUD

## Descrição
CRUD de Clientes utilizando .NET Framework 4.8, NHibernate, SQL Server, Com Json e Redis para cache.

## Tecnologias
- ASP.NET MVC
- NHibernate
- SQL Server
- Redis (StackExchange.Redis)
- HTML, CSS, JavaScript (Razor)


## Antes de iniciar.

1. Instalar SQL Server 2019~2022 na máquina local https://www.microsoft.com/pt-br/sql-server/sql-server-downloads

2. Baixar o redis para rodar localmente https://github.com/tporadowski/redis/releases

3. Abrir o Projeto no visual studio 2019 ou superior https://visualstudio.microsoft.com/pt-br/downloads/

4. Verificar se as Bibliotecas abaixo estão no projeto:
	- Biblioteca nuget -> NHibernate
	- Biblioteca nuget -> FluentNHibernate
	- Biblioteca nuget -> System.Data.SqlClient
	- Biblioteca nuget -> StackExchange.Redis
	- Biblioteca nuget -> Microsoft.AspNet.Mvc
	- Biblioteca nuget -> Microsoft.AspNet.Razor
	- Biblioteca nuget -> Microsoft.AspNet.WebPages
	- Biblioteca nuget -> Newtonsoft.Json

## Instalação

1. Crie o banco de dados:
	- Execute o script `DatabaseScript/ClientesCreateDB.sql` no SQL Server.

2. Configure o Redis:
	- Redis deve estar rodando localmente na porta 6379.
	- Caso você precise mudar o apontamento do redis no projeto é só alterar 'RedisConfig' no appSettings no web.config

3. NHibernate:
   - O hibernate já está configurado para pegar a ConnectionString no webconfig do projeto.

4. Execute o projeto pelo Visual Studio.

## Funcionamento do Cache
- Ao consultar um cliente, a busca ocorre primeiro no Redis.
- Se não encontrado no Redis, consulta o SQL Server e atualiza o cache Redis.
- Operações de Create, Update e Delete também atualizam ou removem do Redis.

## Meu git
	https://github.com/guh008code/