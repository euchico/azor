# Configuração local

O backend usa SQL Server e JWT. A configuração sem segredos fica em `Source/Web/appsettings.json` e `Source/Web/appsettings.Development.json`.

Para desenvolvimento nesta máquina, a string integrada configurada é:

```text
Server=localhost;Database=Azor;Integrated Security=True;Encrypt=False
```

`Encrypt=False` é exclusivo deste ambiente: a instância local não oferece suporte TLS ao cliente instalado. Em produção, use uma connection string com TLS configurado por variável de ambiente `ConnectionStrings__AzorDb`.

Configure a chave JWT fora do repositório:

```powershell
dotnet user-secrets set "Jwt:Key" "uma-chave-longa-e-aleatoria" --project Source/Web/Web.csproj
```

Em produção, use a variável de ambiente `Jwt__Key`. A chave deve ter ao menos 32 bytes aleatórios e nunca deve ser enviada ao Git.

Para criar/aplicar migrations:

```powershell
dotnet ef migrations add NomeDaMigration --project Source/Infrastructure/Infrastructure.csproj --startup-project Source/Web/Web.csproj --context AzorDbContext
dotnet ef database update --project Source/Infrastructure/Infrastructure.csproj --startup-project Source/Web/Web.csproj --context AzorDbContext
```
