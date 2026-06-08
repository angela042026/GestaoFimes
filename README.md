# MovieManagement

Projeto desenvolvido em C# aplicando Arquitetura em Camadas, Interfaces, Regras de Negócio, Persistência de Dados e controlo de versões com Git e GitHub.

## 🎯 Objetivo Geral
O objetivo deste projeto é consolidar progressivamente os conceitos de engenharia de software através do desenvolvimento estruturado de uma aplicação de gestão de filmes.

---

## 🏗️ Estrutura da Solução
A aplicação está organizada em quatro camadas distintas para garantir a separação de responsabilidades:

* **MovieManagement.UI**: Interação com o utilizador.
* **MovieManagement.Business**: Regras de negócio e validações.
* **MovieManagement.Data**: Persistência e acesso aos dados.
* **MovieManagement.Domain**: Entidades do sistema e interfaces.

---

## 🚀 Fases de Desenvolvimento

### Parte 1: Implementação da Entidade Filme
Estrutura inicial focada nos dados dos filmes e na sua manipulação em memória.

* **Entidade Filme**: `Id`, `Título`, `Ano`, `Língua`, `Classificação`.
* **Funcionalidades**: Adicionar, listar, procurar por título e remover filmes.
* **Regras de Negócio**: Título obrigatório e único. Classificação entre 0 e 5.
* **Persistência**: Armazenamento temporário em memória através de `List<Filme>`.
* **Commit Obrigatório**: `Conclusão Parte 1`

### Parte 2: Implementação de Categorias e Realizadores
Expansão do domínio da aplicação com novas entidades independentes.

* **Entidade Categoria**: `Id`, `Nome`.
* **Entidade Realizador**: `Id`, `Nome`, `País`.
* **Funcionalidades**: Operações CRUD básicas (adicionar, listar, procurar e remover).
* **Regras de Negócio**: Nome obrigatório e único para Categorias. Nome e País obrigatórios para Realizadores.
* **Persistência**: Armazenamento em memória com `List<Categoria>` e `List<Realizador>`.
* **Commit Obrigatório**: `Conclusão Parte 2`

### Parte 3: Relações entre Entidades e SQLite
Foco na integridade dos dados e transição para um sistema relacional definitivo.

* **Relações**: Cada filme possui uma Categoria (`CategoriaId`) e um Realizador (`RealizadorId`).
* **Validações**: A *Business Layer* valida se a categoria e o realizador existem antes de adicionar o filme.
* **Persistência Híbrida**: Suporte simultâneo para `List<T>` e base de dados `SQLite`.
* **Desacoplamento**: Troca de persistência transparente sem alterar as camadas UI, Business ou Domain.
* **Commit Obrigatório**: `Conclusão Parte 3`


