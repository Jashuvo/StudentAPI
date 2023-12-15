namespace StudentRestAPI.Models
{
    public interface IStudentRepo
    {
        Task<IEnumerable<Student>> Search(string name, Gender?gender);
        Task<Student> GetStudent(int StudentId);
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudentByEmail(string email);
        Task<Student> AddStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        Task<Student> DeleteStudent(int StudentID);
    }
}
