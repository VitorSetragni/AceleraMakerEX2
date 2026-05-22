# AceleraMakerEX2

Backend de um sistema de Blog Pessoal desenvolvido em **ASP.NET Core Web API**.

O projeto permite gerenciar usuários, temas e postagens, utilizando autenticação JWT, banco de dados PostgreSQL, Entity Framework Core, documentação com Swagger e análise de qualidade com SonarQube.

---

## Tecnologias utilizadas

- C#
- ASP.NET Core 8 Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Swagger / OpenAPI
- SonarQube
- Docker

---

## Objetivo do projeto

O objetivo deste projeto é criar uma API RESTful para um Blog Pessoal, aplicando conceitos de:

- Arquitetura em camadas
- CRUD com ASP.NET Core
- Persistência de dados com Entity Framework Core
- Relacionamento entre tabelas
- Autenticação com JWT
- Segurança em endpoints protegidos
- Documentação com Swagger
- Análise de qualidade com SonarQube

---

## Estrutura do projeto

```txt
AceleraMakerEX2/
│
├── Controllers/
│   ├── PostagemController.cs
│   ├── TemaController.cs
│   └── UsuarioController.cs
│
├── Services/
│   ├── IPostagemService.cs
│   ├── PostagemService.cs
│   ├── ITemaService.cs
│   ├── TemaService.cs
│   ├── IUsuarioService.cs
│   └── UsuarioService.cs
│
├── Repositories/
│   ├── IPostagemRepository.cs
│   ├── PostagemRepository.cs
│   ├── ITemaRepository.cs
│   ├── TemaRepository.cs
│   ├── IUsuarioRepository.cs
│   └── UsuarioRepository.cs
│
├── Models/
│   ├── Postagem.cs
│   ├── Tema.cs
│   ├── Usuario.cs
│   └── UsuarioLogin.cs
│
├── DTOs/
│   ├── PostagemDTO.cs
│   ├── TemaDTO.cs
│   ├── UsuarioDTO.cs
│   └── UsuarioRespostaDTO.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── Migrations/
├── Program.cs
├── appsettings.json
└── BlogPessoal.csproj
```

---

## Observações importantes

- O projeto é uma API Backend.
- O projeto não possui interface gráfica.
- A documentação e os testes dos endpoints são feitos pelo Swagger.
- A autenticação é feita com JWT.
- O banco de dados online foi configurado para facilitar a avaliação acadêmica.
- O desafio de Inteligência Artificial foi deixado como melhoria futura, por conta de problemas de saude do autor, que ja foi comunicado a um dos instrutores.

---

## Arquitetura utilizada

O projeto foi organizado em camadas:

#### Controllers

#### Services

#### Repositories

#### Models

#### DTOs



---

## Entidades do sistema

### Usuario

Representa o usuário cadastrado no sistema.

Campos principais:

- Id
- Nome
- Email
- Senha

### Tema

Representa o tema ou categoria de uma postagem.

Campos principais:

- Id
- Nome

### Postagem

Representa uma postagem criada por um usuário e vinculada a um tema.

Campos principais:

- Id
- Titulo
- Texto
- Data
- UsuarioId
- TemaId

### UsuarioLogin

Classe auxiliar usada para autenticação do usuário.

Campos principais:

- Email
- Senha
- Token

---

## Funcionalidades implementadas

- Cadastro de usuários
- Listagem de usuários
- Busca de usuário por ID
- Atualização de usuário
- Exclusão de usuário
- Login com JWT
- Logout simbólico
- Cadastro de temas
- Listagem de temas
- Busca de tema por ID
- Atualização de temas
- Exclusão de temas
- Cadastro de postagens
- Listagem de postagens
- Busca de postagem por ID
- Busca de postagens por usuário
- Busca de postagens por tema
- Filtro de postagens por autor e tema
- Atualização de postagens
- Exclusão de postagens
- Proteção de endpoints com JWT
- Validação de dono do recurso
- Documentação com Swagger
- Análise de qualidade com SonarQube

---

## Como rodar o projeto

### Pré-requisitos

Antes de executar o projeto, é necessário ter instalado:

- .NET SDK 8
- Git
- Entity Framework Core CLI
- PostgreSQL, caso queira usar banco local
- Docker, caso queira executar o SonarQube


> **OBS:** O banco de dados já está conectado a uma instância online do PostgreSQL para facilitar a validação do projeto pelos instrutores.
> O projeto utiliza ASP.NET Core Web API, que já está incluído no .NET SDK 8.

---

### 1. Clonar o repositório

```bash
git clone https://github.com/VitorSetragni/AceleraMakerEX2.git
```
###OBS:### lembre de entrar na pasta BlogPessoal para os proximos passos
---

### 2. Restaurar as dependências

```bash
dotnet restore
```

---

### 3. Aplicar as migrations no banco

```bash
dotnet ef database update
```

---

### 4. Compilar o projeto

```bash
dotnet build
```

---

### 5. Executar o projeto

```bash
dotnet run
```

Após executar, o terminal mostrará a URL da aplicação, por exemplo:

```txt
http://localhost:5242
```

---

### 6. Acessar o Swagger

Abra no navegador:

```txt
http://localhost:5242/swagger
```

Caso a porta seja diferente, use a porta exibida no terminal após executar `dotnet run`.

---

## Banco de dados

O projeto utiliza **PostgreSQL** com Entity Framework Core.

Para facilitar a avaliação, a connection string do banco online já está configurada no arquivo:

```txt
appsettings.json
```

> Em projetos reais, não é recomendado deixar senhas no `appsettings.json`. Neste projeto, a conexão esta configurada diretamente no arquivo apenas para fins acadêmicos e de avaliação.

---

## Endpoints da API

## Usuários

| Método | Endpoint | Descrição | Autenticação |

| GET | `/api/usuarios/{id}` | Busca usuário por ID | Não |
| POST | `/api/usuarios/cadastrar` | Cadastra um usuário | Não |
| POST | `/api/usuarios/login` | Realiza login | Não |
| POST | `/api/usuarios/logout` | Realiza logout simbólico | Não |
| PUT | `/api/usuarios/{id}` | Atualiza usuário | Sim |
| DELETE | `/api/usuarios/{id}` | Remove usuário | Sim |

---

## Temas

| Método | Endpoint | Descrição | Autenticação |

| GET | `/api/temas` | Lista todos os temas | Não |
| GET | `/api/temas/{id}` | Busca tema por ID | Não |
| POST | `/api/temas` | Cadastra tema | Sim |
| PUT | `/api/temas/{id}` | Atualiza tema | Sim |
| DELETE | `/api/temas/{id}` | Remove tema | Sim |

---

## Postagens

| Método | Endpoint | Descrição | Autenticação |

| GET | `/api/postagens` | Lista todas as postagens | Não |
| GET | `/api/postagens/{id}` | Busca postagem por ID | Não |
| GET | `/api/postagens/usuario/{usuarioId}` | Busca postagens por usuário | Não |
| GET | `/api/postagens/tema/{temaId}` | Busca postagens por tema | Não |
| GET | `/api/postagens/filtro?autor={id}&tema={id}` | Filtra postagens por autor e tema | Não |
| POST | `/api/postagens` | Cadastra postagem | Sim |
| PUT | `/api/postagens/{id}` | Atualiza postagem | Sim |
| DELETE | `/api/postagens/{id}` | Remove postagem | Sim |

---

## Regras de segurança

- As senhas são armazenadas com hash.
- O login retorna um token JWT.
- Endpoints protegidos exigem autenticação.
- Usuários só podem alterar ou deletar a própria conta.
- Usuários só podem alterar ou deletar suas próprias postagens.
- O autor da postagem é identificado pelo token JWT.
- O logout em JWT é simbólico: o token deve ser removido pelo cliente.

---

## Logout

Como a autenticação utiliza JWT, o backend não mantém uma sessão ativa do usuário.

Por isso, o logout é feito removendo o token do cliente. O endpoint de logout retorna apenas uma mensagem informativa.

Endpoint:

```http
POST /api/usuarios/logout
```

---

## Autenticação JWT

A API utiliza autenticação com JWT.

Fluxo de autenticação:

1. O usuário realiza o cadastro.
2. O usuário faz login com email e senha.
3. A API retorna um token JWT.
4. O token deve ser enviado nas requisições protegidas.
5. A API valida o token antes de permitir o acesso.

No Swagger, clique em **Authorize** e informe o token

## Como testar a API

### 1. Cadastrar usuário

Endpoint:

```http
POST /api/usuarios/cadastrar
```

Exemplo de corpo:

```json
{
  "nome": "Vitor",
  "email": "vitor@email.com",
  "senha": "123456",
}
```

---

### 2. Fazer login

Endpoint:

```http
POST /api/usuarios/login
```

Exemplo de corpo:

```json
{
  "email": "vitor@email.com",
  "senha": "123456"
}
```

A API retornará um token JWT.

---

### 3. Autorizar no Swagger

No Swagger, clique em **Authorize** e cole o token no formato:

```txt
Bearer TOKEN_RETORNADO_NO_LOGIN
```

---

### 4. Criar um tema

Endpoint:

```http
POST /api/temas
```

Exemplo de corpo:

```json
{
  "nome": "Tecnologia"
}
```

Esse endpoint é protegido por JWT.

---

### 5. Criar uma postagem

Endpoint:

```http
POST /api/postagens
```

Exemplo de corpo:

```json
{
  "titulo": "Minha primeira postagem",
  "texto": "Texto da minha postagem",
  "temaId": 1
}
```

Esse endpoint é protegido por JWT.

O usuário responsável pela postagem é identificado automaticamente pelo token JWT.

## SonarQube

O SonarQube foi utilizado para analisar a qualidade do código-fonte do projeto.

---

## Comandos úteis

## Migrations

### Criar uma nova migration

```bash
dotnet ef migrations add NomeDaMigration
```

### Aplicar as migrations no banco

```bash
dotnet ef database update
```

### Listar migrations existentes

```bash
dotnet ef migrations list
```
---
### Restaurar dependências

```bash
dotnet restore
```

### Compilar o projeto

```bash
dotnet build
```

### Rodar o projeto

```bash
dotnet run
```

### Aplicar migrations

```bash
dotnet ef database update
```

---

## Autor

Desenvolvido por Vitor Setragni.
