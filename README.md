# 🐱 CatControl

**Sistema completo de gestão e cuidados para gatos domésticos**

CatControl é uma aplicação web full-stack que centraliza todas as informações importantes sobre cuidados com gatos em uma única plataforma, facilitando o acompanhamento da saúde, controle de estoque de produtos e gestão financeira.

***

## 🚀 Tecnologias

### Backend
- **.NET 9.0** - Framework principal
- **ASP.NET Core Web API** - API RESTful
- **Entity Framework Core 8.0** - ORM
- **PostgreSQL** - Banco de dados relacional
- **JWT Bearer** - Autenticação e autorização
- **BCrypt.Net 4.0.3** - Criptografia de senhas
- **Swagger/OpenAPI** - Documentação da API

### Frontend
- **Angular 20.3** - Framework SPA
- **TypeScript 5.9** - Linguagem principal
- **Standalone Components** - Arquitetura modular
- **Angular SSR** - Server-Side Rendering
- **RxJS 7.8** - Programação reativa
- **Angular Material 20.2** - Componentes UI
- **Tailwind CSS 3.4** - Framework de estilos
- **ngx-toastr 19.1** - Sistema de notificações
- **date-fns 4.1** - Manipulação de datas
- **Express 5.1** - Servidor Node.js para SSR

***

## ✨ Funcionalidades

### 🐈 Gestão de Gatos
- Cadastro individual com dados completos (nome, raça, cor, sexo, peso, microchip)
- Upload de fotos de perfil
- Cálculo automático de idade
- Controle de castração
- Histórico de peso
- Observações personalizadas

### 💉 Controle de Vacinas
- Registro completo de vacinas (V3, V4, V5, Antirrábica)
- Agendamento de próximas doses
- Alertas de vencimento automáticos
- Histórico completo de vacinação
- Registro de veterinário e local de aplicação
- Controle de valores gastos

### 💊 Controle de Vermífugos
- Registro de medicamentos aplicados
- Controle de dosagem por peso
- Histórico de vermifugação
- Lembretes de próximas aplicações

### 📦 Controle de Estoque
- Gestão de ração, petiscos, sachês e areia higiênica
- Alertas automáticos de estoque baixo
- Controle de validade com notificações
- Cálculo de consumo médio diário
- Estimativa de duração do estoque
- Controle de preços e marcas

### 💰 Gestão Financeira
- Registro de despesas por categoria
- Relatórios de gastos mensais e anuais
- Gráficos de gastos por categoria
- Controle de despesas recorrentes
- Histórico completo de transações
- Planejamento de orçamento

### 🎁 Lista de Desejos
- Wishlist de produtos para os gatos
- Sistema de prioridades (Alta, Média, Baixa)
- Links diretos para produtos
- Controle de preços estimados
- Marcação de itens comprados
- Total estimado de gastos

### 🔔 Sistema de Notificações
- Lembretes automáticos de vacinas
- Alertas de estoque baixo
- Notificações de produtos vencendo
- Central de notificações com filtros
- Contador de notificações não lidas
- Sistema de prioridades

### ✨ Cuidados Higiênicos
- Registro de escovação, banho e corte de unhas
- Agendamento de próximos cuidados
- Histórico completo de procedimentos

### 🏥 Consultas Veterinárias
- Registro de visitas ao veterinário
- Diagnósticos e tratamentos
- Controle de valores gastos
- Agendamento de retornos

---

## 📋 Pré-requisitos

### Backend
- .NET 9 SDK
- PostgreSQL 14+
- Visual Studio 2022 ou VS Code

### Frontend
- Node.js 18+
- npm ou yarn
- Angular CLI 20

***

## 🔧 Instalação

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/catcontrol.git
cd catcontrol
```

### 2. Configurar Backend

```bash
cd backend

# Restaurar pacotes
dotnet restore

# Configurar string de conexão no appsettings.json
# Editar: ConnectionStrings:DefaultConnection

# Criar banco de dados
dotnet ef database update

# Executar
dotnet run
```

**Backend rodará em:** `http://localhost:5000` e `https://localhost:5001`

**Swagger UI:** `https://localhost:5001/swagger`

### 3. Configurar Frontend

```bash
cd frontend

# Instalar dependências
npm install

# Configurar URL da API no environment.ts
# Editar: src/environments/environment.ts

# Executar em modo desenvolvimento
npm start
```

**Frontend rodará em:** `http://localhost:4200`

***

## 📁 Estrutura do Projeto

```
cat-control/
├── backend/
│   ├── Controllers/          # Endpoints da API
│   ├── Services/             # Lógica de negócio
│   ├── Models/               # Entidades do banco
│   ├── DTOs/                 # Data Transfer Objects
│   ├── Data/                 # DbContext e Migrations
│   ├── Utils/                # Helpers (JWT, Password)
│   ├── Program.cs            # Configuração da aplicação
│   └── CatControl.Api.csproj # Dependências do projeto
│
└── frontend/
    ├── src/
    │   ├── app/
    │   │   ├── core/
    │   │   │   ├── guards/       # Proteção de rotas
    │   │   │   ├── interceptors/ # HTTP Interceptors
    │   │   │   ├── services/     # Services da API
    │   │   │   └── models/       # Interfaces TypeScript
    │   │   ├── features/         # Módulos da aplicação
    │   │   │   ├── auth/
    │   │   │   ├── dashboard/
    │   │   │   ├── cats/
    │   │   │   ├── vaccines/
    │   │   │   ├── stock/
    │   │   │   ├── finance/
    │   │   │   ├── wishlist/
    │   │   │   └── notifications/
    │   │   └── shared/           # Componentes compartilhados
    │   ├── environments/         # Configurações
    │   ├── main.ts              # Bootstrap da aplicação
    │   ├── main.server.ts       # Bootstrap SSR
    │   └── server.ts            # Servidor Express
    ├── angular.json             # Configuração do Angular CLI
    └── package.json             # Dependências do projeto
```

***

## 🔐 API Endpoints

### Autenticação
- `POST /api/auth/register` - Criar nova conta
- `POST /api/auth/login` - Fazer login
- `GET /api/auth/check-email` - Verificar email

### Gatos
- `GET /api/cats` - Listar gatos
- `GET /api/cats/{id}` - Buscar gato específico
- `POST /api/cats` - Criar gato
- `PUT /api/cats/{id}` - Atualizar gato
- `DELETE /api/cats/{id}` - Excluir gato

### Vacinas
- `GET /api/vaccines` - Listar vacinas
- `GET /api/vaccines/cat/{catId}` - Vacinas de um gato
- `GET /api/vaccines/upcoming` - Vacinas próximas
- `POST /api/vaccines` - Registrar vacina
- `PUT /api/vaccines/{id}` - Atualizar vacina
- `DELETE /api/vaccines/{id}` - Excluir vacina

### Estoque
- `GET /api/stock` - Listar estoque
- `GET /api/stock/low-stock` - Itens com estoque baixo
- `GET /api/stock/expiring` - Itens vencendo
- `POST /api/stock` - Adicionar item
- `PUT /api/stock/{id}` - Atualizar item
- `DELETE /api/stock/{id}` - Excluir item

### Financeiro
- `GET /api/finance` - Listar gastos
- `GET /api/finance/summary` - Resumo financeiro
- `GET /api/finance/period` - Gastos por período
- `POST /api/finance` - Registrar gasto
- `PUT /api/finance/{id}` - Atualizar gasto
- `DELETE /api/finance/{id}` - Excluir gasto

### Wishlist
- `GET /api/wishlist` - Listar wishlist
- `GET /api/wishlist/priority/{priority}` - Filtrar por prioridade
- `POST /api/wishlist` - Adicionar item
- `POST /api/wishlist/{id}/purchase` - Marcar como comprado
- `PUT /api/wishlist/{id}` - Atualizar item
- `DELETE /api/wishlist/{id}` - Excluir item

### Notificações
- `GET /api/notifications` - Listar notificações
- `GET /api/notifications/unread` - Não lidas
- `GET /api/notifications/unread/count` - Contador
- `POST /api/notifications/{id}/read` - Marcar como lida
- `POST /api/notifications/read-all` - Marcar todas como lidas
- `DELETE /api/notifications/{id}` - Excluir notificação

***

## 🎨 Recursos da Interface

- **Design Responsivo** - Otimizado para desktop, tablet e mobile
- **Server-Side Rendering** - Melhor SEO e performance inicial
- **Angular Material** - Componentes UI consistentes
- **Notificações Toast** - Feedback visual com ngx-toastr
- **Tema Customizável** - Paleta de cores configurável
- **Notificações em Tempo Real** - Contador de alertas não lidos
- **Filtros Avançados** - Busca e organização de dados
- **Gráficos Interativos** - Visualização de dados financeiros
- **Layout Intuitivo** - Navegação simples e eficiente
- **Feedback Visual** - Loading states e mensagens de sucesso/erro

***

## 📊 Banco de Dados

### Tabelas Principais
- **Users** - Usuários do sistema
- **Cats** - Gatos cadastrados
- **Vaccines** - Histórico de vacinas
- **Dewormings** - Histórico de vermífugos
- **Stocks** - Controle de estoque
- **Finances** - Registros financeiros
- **Budgets** - Orçamentos planejados
- **Wishlists** - Lista de desejos
- **Notifications** - Notificações do sistema
- **WeightHistories** - Histórico de peso
- **VeterinaryVisits** - Consultas veterinárias
- **Hygienes** - Cuidados higiênicos

***

## 📝 Scripts Disponíveis

### Backend
```bash
dotnet restore    # Restaurar dependências
dotnet build      # Compilar projeto
dotnet run        # Executar aplicação
dotnet ef migrations add [Nome]    # Criar migration
dotnet ef database update          # Aplicar migrations
```

### Frontend
```bash
npm start         # Servidor de desenvolvimento
npm run build     # Build para produção
npm test          # Executar testes
npm run watch     # Build em modo watch
```

***

## 🔒 Segurança

- **Autenticação JWT** - Tokens seguros com expiração configurável
- **Bcrypt** - Hash de senhas com salt
- **CORS** - Configuração de origens permitidas
- **Validação de Dados** - DTOs com Data Annotations
- **SQL Injection Protection** - Entity Framework com queries parametrizadas

***

## 📝 Licença

Este projeto está sob a licença MIT.

***

**Desenvolvido com 💙 para tutores que amam seus gatos**

[1](https://angular.dev/reference/configs/file-structure)
[2](https://www.youtube.com/watch?v=8BtQXFm5ufA)
[3](https://www.angulararchitects.io/blog/the-perfect-project-setup-for-angular-structure-and-automation-for-more-quality/)
[4](https://stackoverflow.com/questions/78526073/what-is-the-proper-way-to-reference-assets-in-the-new-angular-18-public-folder)
[5](https://www.bairesdev.com/blog/angular-project-structure-best-practices-tips/)
[6](https://angular.dev/reference/configs/workspace-config)
[7](https://nx.dev/blog/architecting-angular-applications)
[8](https://www.youtube.com/watch?v=c6BPUIUHG6o)