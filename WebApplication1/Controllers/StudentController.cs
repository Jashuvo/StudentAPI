using Microsoft.AspNetCore.Mvc;
using StudentRestAPI.Models;

namespace StudentRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepo studentRepo;
        public StudentController(IStudentRepo studentRepo)
        {
            this.studentRepo = studentRepo;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Student>>> Search(string name, Gender?gender)
        {
            try
            {
                var result = await studentRepo.Search(name, gender);
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error retreiving data from Database");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            try
            {
                return Ok(await studentRepo.GetStudents());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreiving data from Database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var result = await studentRepo.GetStudent(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreiving data from Database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            try
            {
                if (student == null)
                {
                    return BadRequest();
                }
                var stu = await studentRepo.GetStudentByEmail(student.Email);
                if(stu != null)
                {
                    ModelState.AddModelError("Email", "Student Email already in use");
                    return BadRequest();
                }
                var createdStudent = await studentRepo.AddStudent(student);
                return CreatedAtAction(nameof(GetStudent),
                    new { id = createdStudent.StudentID }, createdStudent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new student record ");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id,Student student)
        {
            try
            {
                if (id != student.StudentID)
                    return BadRequest("Student ID Mismatch");

                var studentToUpdate = await studentRepo.GetStudent(id);
                if (studentToUpdate != null)
                {
                    return NotFound($"Student with ID={id} not found");
                }
                return await studentRepo.UpdateStudent(student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating student reccord");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteStudent(int id) 
        {
            try
            {
                var studentToDelete = await studentRepo.GetStudent(id);
                if (studentToDelete == null)
                {
                    return NotFound($"Student with ID = {id} not found");
                }
                await studentRepo.DeleteStudent(id);
                return Ok($"Student with ID ={id} deleted");
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting student record");
            }
            
        }
    }


}
