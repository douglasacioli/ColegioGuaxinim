
## ✅ 2. `ColegioGuaxinim.Schema.sql` — Estrutura do Banco de Dados

```sql
-- Criação da tabela Professor
CREATE TABLE Professor (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(120) NOT NULL,
    BloquearTempoDeImportacao DATETIME2 NULL
);

-- Criação da tabela Aluno
CREATE TABLE Aluno (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(120) NOT NULL,
    Mensalidade DECIMAL(18,2) NOT NULL,
    DataDeVencimento DATE NOT NULL,
    ProfessorId INT NOT NULL,
    FOREIGN KEY (ProfessorId) REFERENCES Professor(Id)
);
