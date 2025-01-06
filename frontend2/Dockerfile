# Gebruik een officiële Node.js-runtime als basisimage
FROM node:20

# Stel de werkmap in voor het project
WORKDIR /app

# Kopieer package.json en package-lock.json
COPY package*.json ./

# Installeer de afhankelijkheden
RUN npm install

# Kopieer de rest van de applicatiecode
COPY . .

# Bouw de React-applicatie voor productie
RUN npm run build

# Exposeer poort 80
EXPOSE 3000

# Start Nginx
CMD ["npm", "run", "start"]

