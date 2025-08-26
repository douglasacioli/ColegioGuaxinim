
# 📚 Colegio Guaxinim

Sistema de cadastro de professores e alunos para uma instituição de ensino.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core MVC (.NET 8)
- C#
- Entity Framework Core (Code First)
- SQL Server
- HTML + CSS + JavaScript (Vanilla)
- Bootstrap 5

---

## 🎯 Funcionalidades

| Código | Requisito |
|--------|-----------|
| RN01 | Página inicial exibe a lista de professores |
| RN02 | Cada professor possui link para ver a lista de seus alunos |
| RN03 | Possibilidade de cadastrar novos professores |
| RN04 | Exibe alunos de um professor com Nome, Mensalidade (R$ 0,00), Vencimento (dd/MM/yyyy) |
| RN05 | Exclusão de aluno sem recarregar a página (via JS/fetch) |
| RN06 | Importação de alunos via arquivo `.txt` (padrão: Nome||Valor||Data) |
| RN07 | Bloqueio de nova importação por tempo definido no `appsettings.json` |
| RN08 | Botão de retorno à tela inicial em todas as páginas |

---

## 🧪 Como rodar o projeto

### Pré-requisitos

- .NET 8 SDK
- SQL Server LocalDB ou outro
- Visual Studio 2022 ou VS Code
- EF CLI (opcional): `dotnet tool install --global dotnet-ef`

### Clone o projeto

```bash
git clone https://github.com/seu-usuario/ColegioGuaxinim.git
cd ColegioGuaxinim
```

### Crie o banco de dados

#### 🧠 Opção 1: Via EF Migrations

1. Altere sua `appsettings.json` com sua string de conexão:

```json
"ConnectionStrings": {
  "SqlServer": "Server=(localdb)\\MSSQLLocalDB;Database=ColegioGuaxinimDB;Trusted_Connection=True;"
}
```

2. Execute os comandos:

```bash
cd ColegioGuaxinim.Infrastructure
dotnet ef database update
```

#### 🧾 Opção 2: Rodando o script SQL manualmente

Use o arquivo `ColegioGuaxinim.Schema.sql` (ver abaixo).

---

## 📝 Script SQL

Está na raiz do projeto com o nome:

```
ColegioGuaxinim.Schema.sql
```

Esse script cria as tabelas `Professor` e `Aluno` com as constraints apropriadas.

---

## 📂 Estrutura em Camadas (DDD)

- **Domain** → Entidades puras (Professor, Aluno)
- **Application** → DTOs, Interfaces e Serviços
- **Infrastructure** → EF Core, Migrations, Repositórios
- **Presentation** → ASP.NET MVC (Controllers + Views)

---

## ✨ Observações

- Código limpo, organizado em camadas
- Validações com DataAnnotations
- Uso de `TempData` + Toasts Bootstrap para mensagens
- Exclusão com `fetch()` e `@Html.AntiForgeryToken()` (seguro)
- Respeita padrões RESTful e SOLID

---

## 👨‍💻 Autor

Douglas Acioli 🦝  
Contato: [Seu LinkedIn/GitHub]



## 🛠️ Instalação do Banco de Dados

### 🧠 Opção 1: Via EF Migrations

Altere seu `appsettings.json` com sua string de conexão:

```json
"ConnectionStrings": {
  "SqlServer": "Server=(localdb)\\MSSQLLocalDB;Database=ColegioGuaxinimDB;Trusted_Connection=True;"
}
```

Execute os comandos:

```bash
cd ColegioGuaxinim.Infrastructure
dotnet ef database update
```

### 🧾 Opção 2: Rodando o script SQL manualmente

Use o arquivo `ColegioGuaxinim.Schema.sql` incluído no repositório.
