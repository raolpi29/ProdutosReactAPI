# Etapa 1: build do React
FROM node:18-alpine AS build

WORKDIR /app

COPY package.json package-lock.json ./
RUN npm install

COPY . .

# Aceita variável de build para a URL da API
ARG REACT_APP_API_URL
ENV REACT_APP_API_URL=$REACT_APP_API_URL

RUN npm run build

# Etapa 2: Nginx para servir os arquivos estáticos
FROM nginx:alpine

# Copia os arquivos gerados para o diretório público do Nginx
COPY --from=build /app/build /usr/share/nginx/html

# Expondo a porta padrão do Nginx
EXPOSE 80

# Comando padrão do Nginx
CMD ["nginx", "-g", "daemon off;"]