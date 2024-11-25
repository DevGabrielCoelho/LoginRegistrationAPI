# LoginRegistrationAPI

Este repositório contém a implementação de uma API para cadastro e autenticação de usuários utilizando ASP.NET Core. A API permite realizar operações de login e registro, além de utilizar autenticação baseada em JWT para segurança.

## Estrutura do Repositório

```
LoginRegistrationAPI/
├──Application/
├── Controllers/: Contém os controladores da API responsáveis pelas rotas.
├── Domain/Models/: Contém os modelos de dados, como usuário e autenticação.
├── Infrastructure/: Contém implementações relacionadas ao banco de dados e persistência de dados.
├── Migrations/: Contém as migrações do banco de dados.
├── Properties/: Contém informações do projeto e configurações.
├── Key.cs: Contém a chave secreta para a geração de tokens JWT.
├── LoginRegistrationAPI.csproj: Arquivo do projeto da API.
├── LoginRegistrationAPI.http: Arquivo para testar as requisições da API.
├── Program.cs: Arquivo principal de configuração e execução da API.
├── appsettings.Development.json: Arquivo de configurações para o ambiente de desenvolvimento.
└── appsettings.json: Arquivo de configurações gerais da API.
```

## Como Usar

1. Clone este repositório para sua máquina local:
   ```bash
   git clone https://github.com/seu-usuario/LoginRegistrationAPI.git
   ```

2. Abra o repositório no seu ambiente de desenvolvimento (Visual Studio ou VS Code).

3. Restaure as dependências e execute a API:
   ```bash
   dotnet restore
   dotnet run
   ```
   
4. Para testar as rotas da API, você pode usar o arquivo `LoginRegistrationAPI.http` ou ferramentas como Postman ou cURL.

5. Lembre de fazer o update no banco de dados:
   ```bash
   dotnet ef database update
   ```

## Funcionalidades

- **Registro de Usuários**: Permite que novos usuários se registrem com nome, email e senha.
- **Login de Usuários**: Permite que usuários autenticados recebam um token JWT para acessar as rotas protegidas.
- **Autenticação JWT**: Utiliza tokens JWT para autenticar e autorizar requisições.

## Tecnologias Utilizadas

- **ASP.NET Core**: Framework para o desenvolvimento da API.
- **Argon2**: Utilizado para Hashing de senhas.
- **Entity Framework Core**: ORM utilizado para interagir com o banco de dados.
- **JWT (JSON Web Tokens)**: Utilizado para autenticação e autorização.
- **MySql**: Banco de dados utilizado para armazenar os dados dos usuários.

## Tópicos Abordados

- Criação de API com ASP.NET Core.
- Implementação de autenticação e autorização com JWT.
- Uso de Entity Framework Core para persistência de dados.
- Desenvolvimento de controle de acesso e segurança na API.
