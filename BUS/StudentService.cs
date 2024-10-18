using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class StudentService
    {
        Model1 context = new Model1();
        public List<Sinhvien> GetAll()
        {
            return context.Sinhviens.ToList();
        }
        public Lop findByTen(string ten)
            {
            return context.Lops.FirstOrDefault(l=>l.TenLop == ten);
        }
        public List<Lop> getAllLop()
        {
            return context.Lops.ToList();
        }
        public void Add(Sinhvien student)
        {
            try
            {
                context.Sinhviens.Add(student);
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                throw; // Rethrow the exception after logging it for debugging
            }
        }
        public void Delete(string maSV)
        {
            // Tìm sinh viên cần xóa bằng mã sinh viên
            Sinhvien student = context.Sinhviens.FirstOrDefault(s => s.MaSV == maSV);
            if (student != null)
            {
                // Xóa sinh viên khỏi context
                context.Sinhviens.Remove(student);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Sinh viên không tồn tại.");
            }
        }
        public void Edit(Sinhvien student)
        {
            // Tìm sinh viên cần chỉnh sửa bằng mã sinh viên
            Sinhvien existingStudent = context.Sinhviens.FirstOrDefault(s => s.MaSV == student.MaSV);
            if (existingStudent != null)
            {
                // Cập nhật các thông tin sinh viên
                existingStudent.HotenSV = student.HotenSV;
                existingStudent.Ngaysinh = student.Ngaysinh;
                existingStudent.MaLop = student.MaLop;

                // Lưu thay đổi vào cơ sở dữ liệu
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Sinh viên không tồn tại.");
            }
        }
        public List<Lop> GetAllClasses()
        {
            return context.Lops.ToList();
        }
        public List<Sinhvien> FindById(string maSV)
        {
            // Tìm danh sách sinh viên có mã sinh viên trùng khớp
            return context.Sinhviens.Where(sv => sv.MaSV == maSV).ToList();
        }
    }
}