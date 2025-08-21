# Build stage
FROM node:20 AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci
RUN npm install -g @angular/cli
COPY . .
RUN npm run build --configuration=production

# Serve stage
FROM nginx:latest
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/RecipeShare/browser /usr/share/nginx/html

EXPOSE 80
