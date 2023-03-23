# DevStudyNotes 

Foi desenvolvida uma API REST completa de Notas de Estudo

## Tecnologias e práticas utilizadas
- ASP.NET Core com .NET 7
- Entity Framework Core
- SQL Server
- Swagger
- Injeção de Dependência
- Programação Orientada a Objetos
- Serialog

## Funcionalidades
- Cadastro, Listagem, Detalhes de Notas de Estudo
- Atualização de notas de estudo

## Passo a passo para rodar o Projeto (Visual Studio Code)
- Necessário SDK .NET 7.0 
##
- dotnet tool install --global dotnet-ef
- dotnet user-secrets init
- dotnet user-secrets set "ConnectionStrings:DevStudyNotes" "Server=IpDoSeuSqlServer;Database=DevStudyNotesDb;User ID=sa;Password=SenhaDoSeuDb")
- dotnet ef migrations add initialMigration -o Persistence/Migrations
- dotnet ef database update

