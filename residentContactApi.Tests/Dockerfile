FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./residentContactApi.sln ./
COPY ./residentContactApi/residentContactApi.csproj ./residentContactApi/
COPY ./residentContactApi.Tests/residentContactApi.Tests.csproj ./residentContactApi.Tests/

RUN dotnet restore ./residentContactApi/residentContactApi.csproj
RUN dotnet restore ./residentContactApi.Tests/residentContactApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out residentContactApi.Tests/residentContactApi.Tests.csproj

CMD dotnet test
