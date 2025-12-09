FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev

USER root
RUN mkdir -p /var/lib/apt/lists/partial

RUN apt-get update && apt-get install -y \
    git \
    curl \
    unzip \
    && rm -rf /var/lib/apt/lists/*

# Carpeta del workspace
WORKDIR /bmp-workspace

# Copiar código dentro del contenedor
COPY . .

# Restaurar herramientas locales
RUN dotnet tool restore || true

# Restaurar dependencias
RUN dotnet restore

CMD [ "bash" ]
