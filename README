# SyncHub: Blazor Frontend & ASP.NET Core WebAPI with Azure Deployment

## Overview

This repository contains a full-stack application with:

- **Frontend**: Blazor Server app (`src/Images/frontend/BlazorApp`)
- **Backend**: ASP.NET Core WebAPI (`src/Images/backend/WebAPI`)
- **Infrastructure**: ARM template for Azure deployment (`src/Infraestructure/deployment.json`)
- **Pipelines**: GitHub Actions and Azure DevOps YAMLs for CI/CD and IaC deployment (`src/Pipelines/`)

---

## Project Structure

```
src/
  Images/
    backend/
      WebAPI/         # ASP.NET Core WebAPI project
    frontend/
      BlazorApp/      # Blazor Server frontend project
  Infraestructure/
    deployment.json   # ARM template for Azure deployment
  Pipelines/
    bicep-docker-build-push.yml  # GitHub Actions for Docker build & push
    iac-deployment.yml           # Azure DevOps pipeline for ARM deployment
```

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
- Access to an [Azure Container Registry (ACR)](https://docs.microsoft.com/en-us/azure/container-registry/)
- (Optional) [Visual Studio Code](https://code.visualstudio.com/)

---

## Building Locally

### 1. Clone the repository

```sh
git clone <your-repo-url>
cd SyncHub
```

### 2. Build the Backend

```sh
cd src/Images/backend/WebAPI
dotnet build
```

### 3. Build the Frontend

```sh
cd ../../frontend/BlazorApp
dotnet build
```

---

## Running Locally

### 1. Run Backend

```sh
cd src/Images/backend/WebAPI
dotnet run
```

### 2. Run Frontend

```sh
cd src/Images/frontend/BlazorApp
dotnet run
```

---

## Building and Pushing Docker Images to ACR

1. **Login to your Azure Container Registry:**

```sh
az acr login --name <your-acr-name>
```

2. **Build and push the backend image:**

```sh
cd src/Images/backend/WebAPI
docker build -t <your-acr-name>.azurecr.io/webapibe:latest .
docker push <your-acr-name>.azurecr.io/webapibe:latest
```

3. **Build and push the frontend image:**

```sh
cd src/Images/frontend/BlazorApp
docker build -t <your-acr-name>.azurecr.io/blazorappfe:latest .
docker push <your-acr-name>.azurecr.io/blazorappfe:latest
```

---

## Deploying to Azure Using the Pipeline

- The pipeline in [`src/Pipelines/iac-deployment.yml`](src/Pipelines/iac-deployment.yml) will:
  - Deploy the ARM template [`src/Infraestructure/deployment.json`](src/Infraestructure/deployment.json)
  - Create the following Azure resources:
    - **Azure Container Apps** for frontend and backend
    - **Log Analytics Workspace** for monitoring
    - **Container App Environment**
    - Configure the apps to pull images from your ACR

### To deploy manually from your local machine:

1. **Login to Azure:**

```sh
az login
```

2. **Deploy the ARM template:**

```sh
az deployment group create \
  --resource-group <your-resource-group> \
  --template-file src/Infraestructure/deployment.json \
  --parameters \
      frontendAppName=<frontend-app-name> \
      backendAppName=<backend-app-name> \
      frontendImage=<your-acr-name>.azurecr.io/blazorappfe:latest \
      backendImage=<your-acr-name>.azurecr.io/webapibe:latest \
      registryServer=<your-acr-name>.azurecr.io \
      registryUsername=<acr-username> \
      registryPassword=<acr-password> \
      location=<azure-region>
```

---

## Services Deployed by the Pipeline

When you run the pipeline, the following Azure services are provisioned:

- **Azure Container Apps**: For both frontend (Blazor) and backend (WebAPI)
- **Azure Container App Environment**
- **Log Analytics Workspace**: For monitoring and logging
- **Container Registry Integration**: Pulls images from your ACR

---

## Notes

- Update the `AllowedOrigins` in your backend's `appsettings.json` to match your frontend URL for CORS.
- Secrets (like ACR credentials) should be stored securely (e.g., Azure Key Vault or pipeline secrets).
- For production, review and update configuration files as needed.

---

## References

- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [Azure Container Apps](https://docs.microsoft.com/en-us/azure/container-apps/)
- [Azure ARM Templates](https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/overview)