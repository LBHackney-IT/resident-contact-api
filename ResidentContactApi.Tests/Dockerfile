FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./ResidentContactApi.sln ./
COPY ./ResidentContactApi/ResidentContactApi.csproj ./ResidentContactApi/
COPY ./ResidentContactApi.Tests/ResidentContactApi.Tests.csproj ./ResidentContactApi.Tests/

RUN dotnet restore ./ResidentContactApi/ResidentContactApi.csproj
RUN dotnet restore ./ResidentContactApi.Tests/ResidentContactApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out ResidentContactApi.Tests/ResidentContactApi.Tests.csproj

CMD dotnet test
