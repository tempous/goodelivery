**Создать консольное приложение для службы доставки, которое фильтрует заказы по району города и времени доставки**

Требования:
***Вывести заказы в ближайшие 30 минут после первого заказа в указанном районе***

Используемые инструменты:
- IDE Rider 2023
- PostgreSQL 15

Используемые технологии и библиотеки:
- .NET 8
- ASP.NET Core Minimal API
- EF Core
- MiniValidation
- Serilog

Ввод:
- Источник (файл/база данных)
- Формат данных (ID заказа, вес, район, время доставки `yyyy-MM-dd HH:mm:ss`)

Вывод:
- Логи (операции/ошибки) в файл/базу данных.
- Результаты фильтрации в файл/базу данных.

Исходные данные о доставках:
- Заполнение с помощью базы данных

Конфигурация запуска:
- Серверный проект с API
- Консольный проект App
- Результаты запусков находятся в папке /assets

Аргументы программы: `[<район>] [<дата и время первой доставки>] [<файл результатов>] [<файл логов>]`
- Аргумент в квадратных скобках может быть опущен (будет запрошен ввод с консоли с последующей валидацией и обработкой)
- В случае отсутствия указания пути для файла результатов, будет сформирован выходной файл `регион_дата_времяпервойдоставки.txt`

Дополнительные требования:
- Валидация входных данных
- Обработка ошибок (ввод-вывод/некорректные данные)
- Поддержка параметров через файлы конфигурации