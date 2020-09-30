FROM node:12


# make the 'app' folder the current working directory
WORKDIR /Users/andreschavesrobles/VS Projects/Azulo Pasantia/Sandbox/andrescr/web

# copy both 'package.json' and 'package-lock.json' 
COPY package*.json ./

# install project dependencies
RUN npm install

# copy project files and folders to the current working directory (i.e. 'app' folder)
COPY . .


EXPOSE 5000
CMD [ "npm", "serve", "--", "--port" "5000" ]