FROM node:latest
RUN npm install -g npm
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build

EXPOSE 3002
CMD ["npm", "start"]
