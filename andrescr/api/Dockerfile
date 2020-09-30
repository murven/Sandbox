FROM node:12

WORKDIR /Users/andreschavesrobles/VS Projects/Azulo Pasantia/Sandbox/andrescr/api

COPY package*.json ./

RUN npm install

COPY . .

EXPOSE 37851

CMD [ "node", "app.js" ]

