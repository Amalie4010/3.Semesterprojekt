FROM node:20
WORKDIR /backend/statistics

# Python setup
RUN apt-get update && \
    apt-get install -y python3 python3-pip python3-venv && \
    apt-get clean && rm -rf /var/lib/apt/lists/*
RUN python3 -m venv /pyenv
RUN /pyenv/bin/pip install --upgrade pip && \
    /pyenv/bin/pip install scipy
ENV PATH="/pyenv/bin:$PATH"

# Node setup
COPY package.json .
RUN npm install
COPY . .
CMD ["node","test.js"]