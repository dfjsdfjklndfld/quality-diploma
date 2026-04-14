using System.Data;
using Quality.Infrastructure;

namespace Quality.Services
{
    public class LookupService
    {
        public DataTable GetDepartments() => MySqlDb.GetTable("SELECT id, name FROM departments ORDER BY name;");
        public DataTable GetDoctors() => MySqlDb.GetTable("SELECT id, full_name FROM users WHERE role_name='Врач' AND is_active=1 ORDER BY full_name;");
        public DataTable GetPatients() => MySqlDb.GetTable("SELECT id, CONCAT(card_number, ' | ', full_name) AS display_name FROM patients ORDER BY full_name;");
        public DataTable GetServices() => MySqlDb.GetTable("SELECT id, CONCAT(name, ' (', FORMAT(price, 2), ' руб.)') AS display_name FROM medical_services WHERE is_active=1 ORDER BY name;");
        public DataTable GetCompletedAppointmentsWithoutSurvey(int doctorId)
        {
            return MySqlDb.GetTable($@"SELECT a.id, CONCAT(DATE_FORMAT(a.appointment_date, '%d.%m.%Y %H:%i'), ' | ', p.full_name) AS display_name
                                      FROM appointments a
                                      JOIN patients p ON p.id = a.patient_id
                                      LEFT JOIN patient_surveys ps ON ps.appointment_id = a.id
                                      WHERE a.doctor_id = {doctorId} AND a.status='completed' AND ps.id IS NULL
                                      ORDER BY a.appointment_date DESC;");
        }
    }
}
