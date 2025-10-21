# 빌드 스테이지.
# 빌드 환경용 기본 이미지 구성 및 빌드하여 현재 스테이지의 /app 디렉토리에 파일 저장.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o /app

# 런타임 스테이지.
# 실행 환경용 기본 이미지 구성 및 현재 스테이지의 /app 디렉토리에 빌드 스테이지의 /app 디렉토리 안에 있던 파일 복사.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# 현재 환경에서의 실행.
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
ENTRYPOINT ["dotnet", "muNoteServer.dll"]

# 도커 컨테이너 실행 커맨드.
# docker run --rm --publish 8080:8080 --volume ~/muNoteServer/data:/app/data muNoteServer:latest