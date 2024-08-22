FROM node:latest
RUN npm install -g npm
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build

EXPOSE 3001

CMD ["npm", "start"]
