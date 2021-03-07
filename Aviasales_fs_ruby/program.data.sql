DROP TABLE IF EXISTS program;

CREATE TABLE program(id INTEGER PRIMARY KEY, title TEXT, description TEXT);

INSERT INTO program(title, description) VALUES('Aviasales / Jetradar', 'Поиск дешевых авиабилетов');
INSERT INTO program(title, description) VALUES('Hotellook', 'Бронирование отелей со скидками до 60%. Забронируйте номер по выгодной цене!');
INSERT INTO program(title, description) VALUES('Discover Cars', 'Аренда автомобилей по всему миру');
INSERT INTO program(title, description) VALUES('Hostelworld', 'Хостелы по всему миру');
INSERT INTO program(title, description) VALUES('Trainline', 'Поиск, сравнение и покупка билетов на автобусы и поезда по Европе');
INSERT INTO program(title, description) VALUES('Kiwi.com', 'Дешёвые билеты на самолёты');