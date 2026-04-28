FROM python:3.12-slim

WORKDIR /app
ENV PYTHONUNBUFFERED=1

RUN apt-get update && apt-get install -y \
    gcc \
    pkg-config \
    libcairo2-dev \
    && rm -rf /var/lib/apt/lists/*

COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

COPY assets/fonts /app/assets/fonts
COPY assets/fonts /app/assets/staticImage
COPY . .

CMD ["python", "main.py"]