FROM nginx:alpine AS final
WORKDIR /app
EXPOSE 5000
COPY publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf