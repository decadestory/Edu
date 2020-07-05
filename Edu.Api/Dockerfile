#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["Edu.Api/01.Edu.Api.csproj", "Edu.Api/"]
COPY ["Edu.Repo/03.Edu.Repo.csproj", "Edu.Repo/"]
COPY ["Edu.Model/04.Edu.Model.csproj", "Edu.Model/"]
COPY ["Atom.Permissioner/Atom.Permissioner.csproj", "Atom.Permissioner/"]
COPY ["Atom.Lib/Atom.Lib.csproj", "Atom.Lib/"]
COPY ["Atom.ConfigCenter/Atom.ConfigCenter.csproj", "Atom.ConfigCenter/"]
COPY ["Atom.EF/Atom.EF.csproj", "Atom.EF/"]
COPY ["Edu.Entity/05.Edu.Entity.csproj", "Edu.Entity/"]
COPY ["Atom.ConfigCenter.Ui/Atom.ConfigCenter.Ui.csproj", "Atom.ConfigCenter.Ui/"]
COPY ["Atom.Starter.Ui/Atom.Starter.Ui.csproj", "Atom.Starter.Ui/"]
COPY ["Atom.Starter/Atom.Starter.csproj", "Atom.Starter/"]
COPY ["Atom.Logger.Ui/Atom.Logger.Ui.csproj", "Atom.Logger.Ui/"]
COPY ["Atom.Logger/Atom.Logger.csproj", "Atom.Logger/"]
COPY ["Edu.Svc/02.Edu.Svc.csproj", "Edu.Svc/"]
RUN dotnet restore "Edu.Api/01.Edu.Api.csproj"
COPY . .
WORKDIR "/src/Edu.Api"
RUN dotnet build "01.Edu.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "01.Edu.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Edu.Api.dll"]