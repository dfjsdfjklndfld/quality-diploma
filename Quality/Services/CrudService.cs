using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Quality.Infrastructure;

namespace Quality.Services
{
    public class CrudService
    {
        public int SaveDepartment(int? id, string name, string location, string phone)
        {
            using (var c = MySqlDb.CreateConnection())
            using (var cmd = c.CreateCommand())
            {
                c.Open();
                if (id.HasValue)
                {
                    cmd.CommandText = @"UPDATE departments SET name=@name, location=@location, phone=@phone WHERE id=@id;";
                    cmd.Parameters.AddWithValue("@id", id.Value);
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO departments(name, location, phone) VALUES(@name,@location,@phone);";
                }
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@location", ToDb(location));
                cmd.Parameters.AddWithValue("@phone", ToDb(phone));

                if (id.HasValue)
                {
                    cmd.ExecuteNonQuery();
                    return id.Value;
                }
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.LastInsertedId);
            }
        }

        public int SaveUser(int? id, string login, string password, string fullName, string roleName, int? departmentId, bool isActive)
        {
            using (var c = MySqlDb.CreateConnection())
            using (var cmd = c.CreateCommand())
            {
                c.Open();
                if (id.HasValue)
                {
                    if (string.IsNullOrWhiteSpace(password))
                    {
                        cmd.CommandText = @"UPDATE users SET login=@login, full_name=@full_name, role_name=@role_name, department_id=@department_id, is_active=@is_active WHERE id=@id;";
                    }
                    else
                    {
                        cmd.CommandText = @"UPDATE users SET login=@login, password_hash=SHA2(@password,256), full_name=@full_name, role_name=@role_name, department_id=@department_id, is_active=@is_active WHERE id=@id;";
                        cmd.Parameters.AddWithValue("@password", password);
                    }
                    cmd.Parameters.AddWithValue("@id", id.Value);
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO users(login, password_hash, full_name, role_name, department_id, is_active) VALUES(@login, SHA2(@password,256), @full_name, @role_name, @department_id, @is_active);";
                    cmd.Parameters.AddWithValue("@password", password);
                }
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@full_name", fullName);
                cmd.Parameters.AddWithValue("@role_name", roleName);
                cmd.Parameters.AddWithValue("@department_id", ToDb(departmentId));
                cmd.Parameters.AddWithValue("@is_active", isActive ? 1 : 0);
                if (id.HasValue)
                {
                    cmd.ExecuteNonQuery();
                    return id.Value;
                }
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.LastInsertedId);
            }
        }

        public int SavePatient(int? id, string cardNumber, string fullName, DateTime birthDate, string phone)
        {
            using (var c = MySqlDb.CreateConnection())
            using (var cmd = c.CreateCommand())
            {
                c.Open();
                if (id.HasValue)
                {
                    cmd.CommandText = @"UPDATE patients SET card_number=@card_number, full_name=@full_name, birth_date=@birth_date, phone=@phone WHERE id=@id;";
                    cmd.Parameters.AddWithValue("@id", id.Value);
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO patients(card_number, full_name, birth_date, phone) VALUES(@card_number,@full_name,@birth_date,@phone);";
                }
                cmd.Parameters.AddWithValue("@card_number", cardNumber);
                cmd.Parameters.AddWithValue("@full_name", fullName);
                cmd.Parameters.AddWithValue("@birth_date", birthDate.Date);
                cmd.Parameters.AddWithValue("@phone", ToDb(phone));
                if (id.HasValue)
                {
                    cmd.ExecuteNonQuery();
                    return id.Value;
                }
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.LastInsertedId);
            }
        }

        public int SaveAppointment(int? id, int patientId, int doctorId, int departmentId, DateTime appointmentDate, string status, string notes, List<int> serviceIds)
        {
            using (var c = MySqlDb.CreateConnection())
            {
                c.Open();
                using (var tx = c.BeginTransaction())
                {
                    try
                    {
                        int appointmentId = 0;
                        using (var cmd = c.CreateCommand())
                        {
                            cmd.Transaction = tx;
                            if (id.HasValue)
                            {
                                cmd.CommandText = @"UPDATE appointments SET patient_id=@patient_id, doctor_id=@doctor_id, department_id=@department_id, appointment_date=@appointment_date, status=@status, notes=@notes WHERE id=@id;";
                                cmd.Parameters.AddWithValue("@id", id.Value);
                                appointmentId = id.Value;
                            }
                            else
                            {
                                cmd.CommandText = @"INSERT INTO appointments(patient_id, doctor_id, department_id, appointment_date, status, notes) VALUES(@patient_id,@doctor_id,@department_id,@appointment_date,@status,@notes);";
                            }
                            cmd.Parameters.AddWithValue("@patient_id", patientId);
                            cmd.Parameters.AddWithValue("@doctor_id", doctorId);
                            cmd.Parameters.AddWithValue("@department_id", departmentId);
                            cmd.Parameters.AddWithValue("@appointment_date", appointmentDate);
                            cmd.Parameters.AddWithValue("@status", status);
                            cmd.Parameters.AddWithValue("@notes", ToDb(notes));
                            if (id.HasValue) cmd.ExecuteNonQuery();
                            else { cmd.ExecuteNonQuery(); appointmentId = Convert.ToInt32(cmd.LastInsertedId); }
                        }

                        using (var deleteCmd = c.CreateCommand())
                        {
                            deleteCmd.Transaction = tx;
                            deleteCmd.CommandText = "DELETE FROM appointment_services WHERE appointment_id=@appointment_id;";
                            deleteCmd.Parameters.AddWithValue("@appointment_id", appointmentId);
                            deleteCmd.ExecuteNonQuery();
                        }

                        foreach (var serviceId in serviceIds)
                        {
                            using (var insertServiceCmd = c.CreateCommand())
                            {
                                insertServiceCmd.Transaction = tx;
                                insertServiceCmd.CommandText = @"INSERT INTO appointment_services(appointment_id, service_id, quantity, price)
                                                                 SELECT @appointment_id, ms.id, 1, ms.price FROM medical_services ms WHERE ms.id=@service_id;";
                                insertServiceCmd.Parameters.AddWithValue("@appointment_id", appointmentId);
                                insertServiceCmd.Parameters.AddWithValue("@service_id", serviceId);
                                insertServiceCmd.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                        return appointmentId;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public int SaveSurvey(int? id, int appointmentId, DateTime surveyDate, int serviceScore, int attentionScore, int waitingScore, int cleanScore, string comment)
        {
            using (var c = MySqlDb.CreateConnection())
            using (var cmd = c.CreateCommand())
            {
                c.Open();
                if (id.HasValue)
                {
                    cmd.CommandText = @"UPDATE patient_surveys SET survey_date=@survey_date, service_quality_score=@service_quality_score, doctor_attention_score=@doctor_attention_score, waiting_time_score=@waiting_time_score, cleanliness_score=@cleanliness_score, comment=@comment WHERE id=@id;";
                    cmd.Parameters.AddWithValue("@id", id.Value);
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO patient_surveys(appointment_id, patient_id, doctor_id, department_id, survey_date, service_quality_score, doctor_attention_score, waiting_time_score, cleanliness_score, comment)
                                        SELECT a.id, a.patient_id, a.doctor_id, a.department_id, @survey_date, @service_quality_score, @doctor_attention_score, @waiting_time_score, @cleanliness_score, @comment
                                        FROM appointments a WHERE a.id=@appointment_id;";
                }
                cmd.Parameters.AddWithValue("@appointment_id", appointmentId);
                cmd.Parameters.AddWithValue("@survey_date", surveyDate);
                cmd.Parameters.AddWithValue("@service_quality_score", serviceScore);
                cmd.Parameters.AddWithValue("@doctor_attention_score", attentionScore);
                cmd.Parameters.AddWithValue("@waiting_time_score", waitingScore);
                cmd.Parameters.AddWithValue("@cleanliness_score", cleanScore);
                cmd.Parameters.AddWithValue("@comment", ToDb(comment));
                if (id.HasValue)
                {
                    cmd.ExecuteNonQuery();
                    return id.Value;
                }
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.LastInsertedId);
            }
        }

        public int SaveCorrectiveAction(int? id, string title, string description, int? departmentId, int? responsibleUserId, DateTime deadline, string status)
        {
            using (var c = MySqlDb.CreateConnection())
            using (var cmd = c.CreateCommand())
            {
                c.Open();
                if (id.HasValue)
                {
                    cmd.CommandText = @"UPDATE corrective_actions SET title=@title, description=@description, department_id=@department_id, responsible_user_id=@responsible_user_id, deadline=@deadline, status=@status, completed_at=CASE WHEN @status='completed' THEN IFNULL(completed_at, NOW()) ELSE NULL END WHERE id=@id;";
                    cmd.Parameters.AddWithValue("@id", id.Value);
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO corrective_actions(title, description, department_id, responsible_user_id, deadline, status, completed_at) VALUES(@title,@description,@department_id,@responsible_user_id,@deadline,@status, CASE WHEN @status='completed' THEN NOW() ELSE NULL END);";
                }
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", ToDb(description));
                cmd.Parameters.AddWithValue("@department_id", ToDb(departmentId));
                cmd.Parameters.AddWithValue("@responsible_user_id", ToDb(responsibleUserId));
                cmd.Parameters.AddWithValue("@deadline", deadline.Date);
                cmd.Parameters.AddWithValue("@status", status);
                if (id.HasValue)
                {
                    cmd.ExecuteNonQuery();
                    return id.Value;
                }
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.LastInsertedId);
            }
        }

        public void DeleteById(string tableName, int id)
        {
            var allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "users", "departments", "patients", "appointments", "corrective_actions"
            };
            if (!allowed.Contains(tableName)) throw new InvalidOperationException("Недопустимая таблица для удаления.");

            using (var c = MySqlDb.CreateConnection())
            using (var cmd = c.CreateCommand())
            {
                c.Open();
                cmd.CommandText = $"DELETE FROM {tableName} WHERE id=@id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        private object ToDb(object value)
        {
            if (value == null) return DBNull.Value;
            if (value is string && string.IsNullOrWhiteSpace((string)value)) return DBNull.Value;
            return value;
        }
    }
}
