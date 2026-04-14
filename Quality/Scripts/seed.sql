INSERT INTO departments(name, location, phone) VALUES
('Терапия', '1 этаж, корпус А', '+7 (900) 100-10-10'),
('Хирургия', '2 этаж, корпус А', '+7 (900) 200-20-20'),
('Кардиология', '3 этаж, корпус Б', '+7 (900) 300-30-30');;
INSERT INTO users(login, password_hash, full_name, role_name, department_id) VALUES
('admin', SHA2('admin123', 256), 'Системный администратор', 'Администратор', NULL),
('doctor1', SHA2('doctor123', 256), 'Иванов Иван Сергеевич', 'Врач', 1),
('doctor2', SHA2('doctor123', 256), 'Петрова Анна Викторовна', 'Врач', 2),
('quality', SHA2('quality123', 256), 'Соколова Мария Андреевна', 'Менеджер по качеству', NULL);;
INSERT INTO patients(card_number, full_name, birth_date, phone, address) VALUES
('P-1001', 'Смирнов Алексей Павлович', '1988-04-16', '+7 (921) 111-11-11', 'ул. Центральная, 10'),
('P-1002', 'Кузнецова Елена Олеговна', '1992-08-03', '+7 (921) 222-22-22', 'пр. Ленина, 25'),
('P-1003', 'Орлов Дмитрий Николаевич', '1979-01-11', '+7 (921) 333-33-33', 'ул. Парковая, 8');;
INSERT INTO medical_services(name, price) VALUES
('Первичный приём', 1500.00), ('Повторный приём', 1000.00), ('ЭКГ', 900.00), ('УЗИ', 1800.00), ('Перевязка', 700.00);;
INSERT INTO appointments(patient_id, doctor_id, department_id, appointment_date, status, diagnosis, notes) VALUES
(1, 2, 1, NOW() - INTERVAL 7 DAY, 'completed', 'ОРВИ', 'Назначено лечение'),
(2, 2, 1, NOW() - INTERVAL 2 DAY, 'completed', 'Профосмотр', 'Рекомендовано наблюдение'),
(3, 3, 2, NOW() + INTERVAL 1 DAY, 'scheduled', NULL, 'Подготовка к консультации');;
INSERT INTO appointment_services(appointment_id, service_id, quantity, price) VALUES
(1, 1, 1, 1500.00), (2, 2, 1, 1000.00), (2, 3, 1, 900.00), (3, 1, 1, 1500.00);;
INSERT INTO patient_surveys(appointment_id, patient_id, doctor_id, department_id, service_quality_score, doctor_attention_score, waiting_time_score, cleanliness_score, comment) VALUES
(1, 1, 2, 1, 5, 5, 4, 5, 'Вежливый врач, быстро приняли'),
(2, 2, 2, 1, 4, 5, 3, 4, 'Небольшая очередь, но качество хорошее');;
INSERT INTO quality_indicators(indicator_code, name, description, target_value, unit_name) VALUES
('AVG_QUALITY', 'Средняя оценка качества', 'Средняя оценка качества услуг по опросам', 4.50, 'балл'),
('AVG_WAITING', 'Средняя оценка ожидания', 'Средняя оценка времени ожидания по опросам', 4.00, 'балл'),
('OVERDUE_ACTIONS', 'Просроченные корректирующие действия', 'Количество просроченных действий', 0, 'шт');;
INSERT INTO indicator_values(indicator_id, department_id, period_month, indicator_value, comment, created_by) VALUES
(1, 1, DATE_FORMAT(CURDATE(), '%Y-%m-01'), 4.50, 'Стабильный уровень сервиса', 4),
(2, 1, DATE_FORMAT(CURDATE(), '%Y-%m-01'), 3.50, 'Нужно сократить время ожидания', 4);;
INSERT INTO corrective_actions(title, description, department_id, responsible_user_id, status, deadline) VALUES
('Сократить время ожидания в терапии', 'Оптимизировать расписание врачей и поток пациентов', 1, 4, 'in_progress', CURDATE() + INTERVAL 10 DAY),
('Проверить регламент записи на консультацию', 'Актуализировать инструкции для регистратуры', 2, 4, 'new', CURDATE() - INTERVAL 3 DAY);;
INSERT INTO audit_log(user_id, action_name, entity_name, entity_id, details) VALUES
(1, 'INIT', 'database', NULL, 'Первичное заполнение тестовыми данными');;
