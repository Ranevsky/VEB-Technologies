# VEB-Technologies
## Примечания

- Код библиотек используется микросервисами (в данном проекте они вырезаны).

- В Entity моделях в качестве ключа используются string, но в бд используется Sequential GUID (благодаря конвертации данных). 
Выбрал string для меньшего изменения кода, если будет меняться бд (например, на MongoDb и др.).

- Подтверждение почты отправляется на RabbitMQ, а то сообщение уже вытягивается, обрабатывается и отправляется на почту (в этом репозитории его нет).

- appsettings.json по хорошему нужно держать пустым, я его не очищал для удобства (если кто-то будет запускать код).
