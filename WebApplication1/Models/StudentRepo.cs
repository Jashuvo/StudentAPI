using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace StudentRestAPI.Models
{
    public class StudentRepo : IStudentRepo
    {
        private readonly AppDbContext appDbContext;

        public StudentRepo(AppDbContext appDbContext) 
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Student> AddStudent(Student student)
        {
            var result = await appDbContext.Students.AddAsync(student);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteStudent(int studentId)
        {
            var result = await appDbContext.Students.FirstOrDefaultAsync(e => e.StudentID == studentId);
            if (result != null)
            {
                appDbContext.Students.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Student> GetStudent(int studentId)
        {
            return await appDbContext.Students.FirstOrDefaultAsync(e => e.StudentID == studentId);
        }

        public async Task<Student> GetStudentByEmail(string email)
        {
            return await appDbContext.Students.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Student>> GetStudents() 
        {
            return await appDbContext.Students.ToListAsync();
        }

        public async Task<IEnumerable<Student>> Search(string name, Gender?gender)
        {
            IQueryable<Student> query = appDbContext.Students;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }

            if (gender != null)
            {
                query = query.Where(e => e.Gender == gender);
            }

            return await query.ToListAsync();
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            var result = await appDbContext.Students.FirstOrDefaultAsync(e => e.StudentID == student.StudentID);
            if (result != null)
            {
                result.FirstName = student.FirstName;
                result.LastName = student.LastName;
                result.Gender = student.Gender;
                result.Email = student.Email;
                result.DateOfBirth = student.DateOfBirth;
                if (student.DepartmentID != 0)
                {
                    result.DepartmentID = student.DepartmentID;
                }
                result.PhotoPath = student.PhotoPath;
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        Task<Student> IStudentRepo.DeleteStudent(int StudentID)
        {
            throw new NotImplementedException();
        }
    }
}
