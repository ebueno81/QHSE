#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN sed -i 's/^MinProtocol\s*=.*$/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf && \
    sed -i 's/^CipherString\s*=.*$/CipherString = DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf


##para que funcione fastReport en linux de lo contrario no mostrara nada
RUN ln -s /lib/x86_64-linux-gnu/libdl-2.24.so /lib/x86_64-linux-gnu/libdl.so
RUN apt-get update \
 && apt-get install -y --allow-unauthenticated \
 libc6-dev \
 libgdiplus \
 libx11-dev \
 && rm -rf /var/lib/apt/lists/*
ENV DISPLAY :99

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["Server/QHSE.Server.csproj", "Server/"]
COPY ["Client/QHSE.Client.csproj", "Client/"]
COPY ["Shared/QHSE.Shared.csproj", "Shared/"]

RUN dotnet restore "Server/QHSE.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "QHSE.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QHSE.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "QHSE.Server.dll"]