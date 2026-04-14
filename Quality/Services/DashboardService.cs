using System.Data;
using Quality.Infrastructure;
namespace Quality.Services
{
    public class DashboardService
    {
        public DataTable GetUsers() => MySqlDb.GetTable("SELECT u.id, u.login AS 'Логин', u.full_name AS 'ФИО', u.role_name AS 'Роль', d.name AS 'Отделение', IF(u.is_active=1, 'Да', 'Нет') AS 'Активен' FROM users u LEFT JOIN departments d ON d.id = u.department_id ORDER BY u.full_name;");
        public DataTable GetDepartments() => MySqlDb.GetTable("SELECT id, name AS 'Название', location AS 'Расположение', phone AS 'Телефон' FROM departments ORDER BY name;");
        public DataTable GetPatients() => MySqlDb.GetTable("SELECT id, card_number AS 'Код', full_name AS 'ФИО', phone AS 'Телефон', birth_date AS 'Дата рождения' FROM patients ORDER BY full_name;");
        public DataTable GetAppointmentsByDoctor(int doctorId) => MySqlDb.GetTable($"SELECT a.id, a.appointment_date AS 'Дата', p.full_name AS 'Пациент', d.name AS 'Отделение', a.status AS 'Статус' FROM appointments a JOIN patients p ON p.id = a.patient_id JOIN departments d ON d.id = a.department_id WHERE a.doctor_id = {doctorId} ORDER BY a.appointment_date DESC;");
        public DataTable GetSurveyAnalytics() => MySqlDb.GetTable("SELECT d.name AS 'Отделение', ROUND(AVG(ps.service_quality_score),2) AS 'Качество', ROUND(AVG(ps.waiting_time_score),2) AS 'Ожидание', COUNT(ps.id) AS 'Опросов' FROM patient_surveys ps JOIN departments d ON d.id = ps.department_id GROUP BY d.name ORDER BY d.name;");
        public DataTable GetSurveys() => MySqlDb.GetTable("SELECT id, survey_date AS 'Дата', department_name AS 'Отделение', doctor_name AS 'Врач', patient_name AS 'Пациент', service_quality_score AS 'Качество', doctor_attention_score AS 'Внимательность', waiting_time_score AS 'Ожидание', cleanliness_score AS 'Чистота', comment AS 'Комментарий' FROM v_patient_surveys ORDER BY survey_date DESC;");
        public DataTable GetCorrectiveActions() => MySqlDb.GetTable("SELECT ca.id, ca.title AS 'Название', d.name AS 'Отделение', u.full_name AS 'Ответственный', ca.status AS 'Статус', ca.deadline AS 'Срок' FROM corrective_actions ca LEFT JOIN departments d ON d.id = ca.department_id LEFT JOIN users u ON u.id = ca.responsible_user_id ORDER BY ca.deadline;");
        public DataTable GetAdminStats() => MySqlDb.GetTable("SELECT 'Пользователи' AS metric, COUNT(*) AS value FROM users UNION ALL SELECT 'Врачи', COUNT(*) FROM users WHERE role_name='Врач' UNION ALL SELECT 'Пациенты', COUNT(*) FROM patients UNION ALL SELECT 'Приёмы', COUNT(*) FROM appointments UNION ALL SELECT 'Просроченные действия', COUNT(*) FROM corrective_actions WHERE status <> 'completed' AND deadline < CURDATE();");
    }
}
