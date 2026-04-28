import pika
import sys
import os
import time
from concurrent.futures import ThreadPoolExecutor

from WorkFlow.WorkFlow import StartWorkFlow


def main():
    rabbit_host = 'rabbitmq'

    connection = None
    while True:
        try:
            connection = pika.BlockingConnection(
                pika.ConnectionParameters(host=rabbit_host)
            )
            print(" [x] Connected to RabbitMQ!")
            break
        except pika.exceptions.AMQPConnectionError:
            print(" [!] RabbitMQ not ready yet, retrying in 5 seconds...")
            time.sleep(5)

    channel = connection.channel()

    channel.queue_declare(
        queue='generate_cv_queue',
        durable=True,
        exclusive=False,
        auto_delete=False,
        arguments=None
    )

    # Rabbit wyśle max 5 niepotwierdzonych wiadomości naraz
    channel.basic_qos(prefetch_count=5)

    # Max 5 równoległych zadań
    executor = ThreadPoolExecutor(max_workers=5)

    def process_message(body_str: str, delivery_tag: int):
        try:
            print(f" [>] Start processing delivery_tag={delivery_tag}")

            # Tutaj uruchamiasz właściwy workflow
            StartWorkFlow(body_str)

            print(f" [✓] Finished delivery_tag={delivery_tag}")

            # ACK trzeba bezpiecznie wykonać na wątku połączenia pika
            connection.add_callback_threadsafe(
                lambda: channel.basic_ack(delivery_tag=delivery_tag)
            )

        except Exception as e:
            print(f" [!] Error while processing delivery_tag={delivery_tag}: {e}")

            # Możesz odrzucić wiadomość i wrzucić z powrotem do kolejki
            connection.add_callback_threadsafe(
                lambda: channel.basic_nack(delivery_tag=delivery_tag, requeue=False)
            )

    def callback(ch, method, properties, body):
        print(f" [x] Received: {body}")

        try:
            body_str = body.decode("utf-8")
        except Exception as e:
            print(f" [!] Decode error: {e}")

            # jeśli body jest uszkodzone, odrzuć
            ch.basic_nack(delivery_tag=method.delivery_tag, requeue=False)
            return

        executor.submit(process_message, body_str, method.delivery_tag)

    channel.basic_consume(
        queue='generate_cv_queue',
        on_message_callback=callback,
        auto_ack=False
    )

    print(" [*] Waiting for messages.")
    channel.start_consuming()


if __name__ == '__main__':
    try:
        main()
    except KeyboardInterrupt:
        print('Interrupted')
        try:
            sys.exit(0)
        except SystemExit:
            os._exit(0)