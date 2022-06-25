const { Kafka } = require('kafkajs')

const kafka = new Kafka({
    clientId: '0',
    brokers: ['localhost:9092'],
});

const producer = kafka.producer();

(async () => {
    await producer.connect()
    await producer.send({
        topic: 'test',
        messages: [
            { value: 'Hello KafkaJS user!' },
        ],
    })

    await producer.disconnect()
})();
