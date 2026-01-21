# FiapCloudGames Payments API

API construída em **.NET 8** para controle de processamento de pagamento dos jogos adquiridos.

**Obs**: Explicação de Infra em https://github.com/joseeduardoleite/fcg-orchestration

## 📦 Tecnologias & Ferramentas

- .NET 8
- C#
- ASP.NET Core Web API
- MassTransit
- RabbitMQ
- Docker
- Kubernetes
- FluentValidation
- AutoMapper
- Moq + xUnit (para testes unitários)
- Asp.Versioning (API versioning)

## 🚀 Funcionalidades

- Processamento e mensageria do fluxo de compra dos jogos
- Validação de DTOs usando FluentValidation
- Controle de acesso via claims e roles (`Admin`, `Usuario`)
- API versioning


## 🐳 Docker

Esta API possui suporte a containerização via Docker.

### Build da imagem

Na raiz do projeto:

```bash
docker build -t fcg-payments-api .
```
Se quiser, pode executar o container localmente
```bash
docker run -d -p 5003:80 --name fcg-payments-api fcg-payments-api
```
A API ficará disponível em http://localhost:5003/swagger

## ☸ Kubernetes

Esta API faz parte da arquitetura de microserviços do projeto FiapCloudGames - 2º fase.

Orquestrada com Kubernetes e comunicação assíncrona via RabbitMQ.

Os manifests desta API estão localizados na pasta:
```bash
/k8s
```
Para realizar o deploy individual desta API no cluster:
```bash
kubectl apply -f k8s/
```

## 🔧 Setup

1. Clone o repositório:

```bash
git clone https://github.com/joseeduardoleite/fcg-payments-api.git
```

2. Restaure os pacotes:
```bash
dotnet restore
```

3. Build do projeto:
```bash
dotnet build
```

## 🏃‍♂️ Executar a API
```bash
dotnet run --project FiapCloudGames.Payments.Api
```

## Atenção
- Esta API não possui controllers por se tratar de um Consumer.

## 🔄 Mapping (AutoMapper)

- AutoMapper é usado para converter entre Entities e DTOs.

- Perfis são carregados automaticamente via DI.

## 👮 Controle de acesso

- Role `Admin`: acesso total a todos os endpoints.

- Role `Usuario`: acesso restrito (ex.: apenas ao próprio recurso).

- Métodos que requerem admin possuem `[Authorize(Roles = "Admin")]`.