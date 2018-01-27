using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ContosoUniversity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student { FirstMidName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { FirstMidName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstMidName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { FirstMidName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstMidName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01") }
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { FirstMidName = "Kim",     LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Fadi",    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstMidName = "Roger",   LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { FirstMidName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstMidName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };
            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorId  = instructors.Single( i => i.LastName == "Abercrombie").InstructorId },
                new Department { Name = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorId  = instructors.Single( i => i.LastName == "Fakhouri").InstructorId },
                new Department { Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorId  = instructors.Single( i => i.LastName == "Harui").InstructorId },
                new Department { Name = "Economics",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorId  = instructors.Single( i => i.LastName == "Kapoor").InstructorId }
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course {CourseId = 1050, Title = "Chemistry",      Credits = 3,
                  DepartmentId = departments.Single( s => s.Name == "Engineering").DepartmentId,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseId = 4022, Title = "Microeconomics", Credits = 3,
                  DepartmentId = departments.Single( s => s.Name == "Economics").DepartmentId,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseId = 4041, Title = "Macroeconomics", Credits = 3,
                  DepartmentId = departments.Single( s => s.Name == "Economics").DepartmentId,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseId = 1045, Title = "Calculus",       Credits = 4,
                  DepartmentId = departments.Single( s => s.Name == "Mathematics").DepartmentId,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseId = 3141, Title = "Trigonometry",   Credits = 4,
                  DepartmentId = departments.Single( s => s.Name == "Mathematics").DepartmentId,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseId = 2021, Title = "Composition",    Credits = 3,
                  DepartmentId = departments.Single( s => s.Name == "English").DepartmentId,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseId = 2042, Title = "Literature",     Credits = 4,
                  DepartmentId = departments.Single( s => s.Name == "English").DepartmentId,
                  Instructors = new List<Instructor>()
                },
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseId, s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {
                    InstructorId = instructors.Single( i => i.LastName == "Fakhouri").InstructorId,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    InstructorId = instructors.Single( i => i.LastName == "Harui").InstructorId,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    InstructorId = instructors.Single( i => i.LastName == "Kapoor").InstructorId,
                    Location = "Thompson 304" },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.Location, s));
            context.SaveChanges();

            AddOrUpdateInstructor(context, "Chemistry", "Kapoor");
            AddOrUpdateInstructor(context, "Chemistry", "Harui");
            AddOrUpdateInstructor(context, "Microeconomics", "Zheng");
            AddOrUpdateInstructor(context, "Macroeconomics", "Zheng");

            AddOrUpdateInstructor(context, "Calculus", "Fakhouri");
            AddOrUpdateInstructor(context, "Trigonometry", "Harui");
            AddOrUpdateInstructor(context, "Composition", "Abercrombie");
            AddOrUpdateInstructor(context, "Literature", "Abercrombie");

            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Alexander").StudentId,
                    CourseId = courses.Single(c => c.Title == "Chemistry" ).CourseId,
                    Grade = Grade.A
                },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Alexander").StudentId,
                    CourseId = courses.Single(c => c.Title == "Microeconomics" ).CourseId,
                    Grade = Grade.C
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Alexander").StudentId,
                    CourseId = courses.Single(c => c.Title == "Macroeconomics" ).CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentId = students.Single(s => s.LastName == "Alonso").StudentId,
                    CourseId = courses.Single(c => c.Title == "Calculus" ).CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentId = students.Single(s => s.LastName == "Alonso").StudentId,
                    CourseId = courses.Single(c => c.Title == "Trigonometry" ).CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Alonso").StudentId,
                    CourseId = courses.Single(c => c.Title == "Composition" ).CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Anand").StudentId,
                    CourseId = courses.Single(c => c.Title == "Chemistry" ).CourseId
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Anand").StudentId,
                    CourseId = courses.Single(c => c.Title == "Microeconomics").CourseId,
                    Grade = Grade.B
                 },
                new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Barzdukas").StudentId,
                    CourseId = courses.Single(c => c.Title == "Chemistry").CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Li").StudentId,
                    CourseId = courses.Single(c => c.Title == "Composition").CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Justice").StudentId,
                    CourseId = courses.Single(c => c.Title == "Literature").CourseId,
                    Grade = Grade.B
                 }
            };

            foreach (var enrollment in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments
                    .SingleOrDefault(_ =>
                        _.Student.StudentId == enrollment.StudentId &&
                        _.Course.CourseId == enrollment.CourseId);

                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(enrollment);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateInstructor(SchoolContext context, string courseTitle, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs?.Instructors.SingleOrDefault(i => i.LastName == instructorName);

            if (inst == null)
            {
                crs?.Instructors.Add(context.Instructors.Single(i => i.LastName == instructorName));
            }
        }
    }
}
