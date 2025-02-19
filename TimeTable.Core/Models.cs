﻿#nullable disable

namespace TimeTable.Core.Models;
public class Entity
{
    public int Id { get; set; }
}

public class Course : Entity
{
    public string Title { get; set; }
    public List<TimeSlot> Slots { get; set; }
    public Teacher Teacher { get; set; }
    public List<StudentCourse> Students { get; set; }

}

public class Place : Entity
{
    public string Name { get; set; }
    public List<TimeSlot> TimeSlots { get; set; }
}

public class Student : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public List<StudentCourse> Courses { get; set; }
    public List<TimeSlot> TimeSlots { get; set; }
}

public class StudentCourse
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }

    public Student Student { get; set; }
    public Course Course { get; set; }

}

public class Teacher : Entity
{
    public List<Course> Courses { get; set; }
    public string Name { get; set; }
}

public class TimeSlot : Entity
{
    public DayOfWeek Day { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }
    public int PlaceId { get; set; }
    public Place Place { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public List<Student> Students { get; set; }
}
