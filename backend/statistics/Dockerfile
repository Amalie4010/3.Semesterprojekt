FROM python:3.12-slim
RUN apt update && apt install -y sqlite3 nodejs npm
COPY . .
RUN rm -rf node_modules
RUN pip install -r requirements.txt
RUN npm install
EXPOSE 8080
CMD ["node", "setup.js"]