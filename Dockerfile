FROM microsoft/dotnet:2.1-sdk as build

WORKDIR /app

# copy csproj and restore as distinct layers
COPY . ./hackerNewsScrapper/
WORKDIR /app/hackerNewsScrapper
RUN dotnet restore

WORKDIR /app/hackerNewsScrapper
RUN dotnet publish HackerNewsScrapper.Host/HackerNewsScrapper.Host.csproj -c Release -o out

FROM microsoft/dotnet:2.1-runtime AS runtime
WORKDIR /app
COPY --from=build /app/hackerNewsScrapper/HackerNewsScrapper.Host/out/ ./
ENTRYPOINT ["dotnet", "HackerNewsScrapper.Host.dll", "--posts 30s"]