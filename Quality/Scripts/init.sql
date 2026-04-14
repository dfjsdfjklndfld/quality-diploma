CREATE TABLE IF NOT EXISTS departments (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    location VARCHAR(150) NULL,
    phone VARCHAR(30) NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);;
CREATE TABLE IF NOT EXISTS users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    login VARCHAR(50) NOT NULL UNIQUE,
    password_hash CHAR(64) NOT NULL,
    full_name VARCHAR(150) NOT NULL,
    role_name ENUM('Администратор', 'Врач', 'Менеджер по качеству') NOT NULL,
    department_id INT NULL,
    is_active TINYINT(1) NOT NULL DEFAULT 1,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_users_department FOREIGN KEY (department_id) REFERENCES departments(id) ON DELETE SET NULL ON UPDATE CASCADE
);;
CREATE TABLE IF NOT EXISTS patients (
    id INT AUTO_INCREMENT PRIMARY KEY,
    card_number VARCHAR(30) NOT NULL UNIQUE,
    full_name VARCHAR(150) NOT NULL,
    birth_date DATE NOT NULL,
    phone VARCHAR(30) NULL,
    address VARCHAR(255) NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);;
CREATE TABLE IF NOT EXISTS medical_services (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(120) NOT NULL,
    price DECIMAL(10,2) NOT NULL DEFAULT 0,
    is_active TINYINT(1) NOT NULL DEFAULT 1
);;
CREATE TABLE IF NOT EXISTS appointments (
    id INT AUTO_INCREMENT PRIMARY KEY,
    patient_id INT NOT NULL,
    doctor_id INT NOT NULL,
    department_id INT NOT NULL,
    appointment_date DATETIME NOT NULL,
    status ENUM('scheduled','completed','cancelled') NOT NULL DEFAULT 'scheduled',
    diagnosis VARCHAR(255) NULL,
    notes TEXT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_appointments_patient FOREIGN KEY (patient_id) REFERENCES patients(id),
    CONSTRAINT fk_appointments_doctor FOREIGN KEY (doctor_id) REFERENCES users(id),
    CONSTRAINT fk_appointments_department FOREIGN KEY (department_id) REFERENCES departments(id)
);;
CREATE TABLE IF NOT EXISTS appointment_services (
    id INT AUTO_INCREMENT PRIMARY KEY,
    appointment_id INT NOT NULL,
    service_id INT NOT NULL,
    quantity INT NOT NULL DEFAULT 1,
    price DECIMAL(10,2) NOT NULL DEFAULT 0,
    CONSTRAINT fk_appointment_services_appointment FOREIGN KEY (appointment_id) REFERENCES appointments(id) ON DELETE CASCADE,
    CONSTRAINT fk_appointment_services_service FOREIGN KEY (service_id) REFERENCES medical_services(id)
);;
CREATE TABLE IF NOT EXISTS patient_surveys (
    id INT AUTO_INCREMENT PRIMARY KEY,
    appointment_id INT NOT NULL UNIQUE,
    patient_id INT NOT NULL,
    doctor_id INT NOT NULL,
    department_id INT NOT NULL,
    survey_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    service_quality_score TINYINT NOT NULL,
    doctor_attention_score TINYINT NOT NULL,
    waiting_time_score TINYINT NOT NULL,
    cleanliness_score TINYINT NOT NULL,
    comment TEXT NULL,
    CONSTRAINT fk_patient_surveys_appointment FOREIGN KEY (appointment_id) REFERENCES appointments(id),
    CONSTRAINT fk_patient_surveys_patient FOREIGN KEY (patient_id) REFERENCES patients(id),
    CONSTRAINT fk_patient_surveys_doctor FOREIGN KEY (doctor_id) REFERENCES users(id),
    CONSTRAINT fk_patient_surveys_department FOREIGN KEY (department_id) REFERENCES departments(id)
);;
CREATE TABLE IF NOT EXISTS quality_indicators (
    id INT AUTO_INCREMENT PRIMARY KEY,
    indicator_code VARCHAR(50) NOT NULL UNIQUE,
    name VARCHAR(120) NOT NULL,
    description TEXT NULL,
    target_value DECIMAL(10,2) NULL,
    unit_name VARCHAR(30) NULL,
    is_active TINYINT(1) NOT NULL DEFAULT 1
);;
CREATE TABLE IF NOT EXISTS indicator_values (
    id INT AUTO_INCREMENT PRIMARY KEY,
    indicator_id INT NOT NULL,
    department_id INT NOT NULL,
    period_month DATE NOT NULL,
    indicator_value DECIMAL(10,2) NOT NULL,
    comment TEXT NULL,
    created_by INT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_indicator_values_indicator FOREIGN KEY (indicator_id) REFERENCES quality_indicators(id),
    CONSTRAINT fk_indicator_values_department FOREIGN KEY (department_id) REFERENCES departments(id),
    CONSTRAINT fk_indicator_values_user FOREIGN KEY (created_by) REFERENCES users(id) ON DELETE SET NULL
);;
CREATE TABLE IF NOT EXISTS corrective_actions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(180) NOT NULL,
    description TEXT NULL,
    department_id INT NULL,
    responsible_user_id INT NULL,
    status ENUM('new','in_progress','completed') NOT NULL DEFAULT 'new',
    deadline DATE NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    completed_at DATETIME NULL,
    CONSTRAINT fk_corrective_actions_department FOREIGN KEY (department_id) REFERENCES departments(id) ON DELETE SET NULL,
    CONSTRAINT fk_corrective_actions_user FOREIGN KEY (responsible_user_id) REFERENCES users(id) ON DELETE SET NULL
);;
CREATE TABLE IF NOT EXISTS audit_log (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NULL,
    action_name VARCHAR(100) NOT NULL,
    entity_name VARCHAR(100) NOT NULL,
    entity_id INT NULL,
    details TEXT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_audit_log_user FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE SET NULL
);;
CREATE OR REPLACE VIEW v_patient_surveys AS
SELECT ps.id, ps.survey_date, d.name AS department_name, u.full_name AS doctor_name, p.full_name AS patient_name, ps.service_quality_score, ps.doctor_attention_score, ps.waiting_time_score, ps.cleanliness_score, ps.comment
FROM patient_surveys ps
JOIN departments d ON d.id = ps.department_id
JOIN users u ON u.id = ps.doctor_id
JOIN patients p ON p.id = ps.patient_id;;
