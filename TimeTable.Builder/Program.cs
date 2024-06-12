using GeneticSharp;
using Microsoft.EntityFrameworkCore;
using TimeTable.Core;
using TimeTable.Core.Models;

namespace TimeTable.Builder;

internal class Program
{
    static void Main(string[] args)
    {
        using var dataContext = new DataContext();

        Seed(dataContext);

        var selection = new EliteSelection();
        var crossover = new OrderedCrossover();
        var mutation = new InsertionMutation();
        var fitness = new TimeTableEvaluator();

        var courseCount = dataContext.Courses.Count();

        var foo = new TimeTableChromosome(dataContext, courseCount);
        Population population = new Population(50, 70, foo);

        var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
        ga.Termination = new GenerationNumberTermination(100);

        Console.WriteLine("GA running...");
        ga.Start();

        Console.WriteLine("Best solution found has {0} fitness.", ga.BestChromosome.Fitness);

        var best = ga.BestChromosome as TimeTableChromosome;

        var timeSlots = best.GetGenes().Select(x => (TimeSlotChromosome)x.Value);

        var timetable = timeSlots.Select(chromosome =>
                                        new TimeSlot()
                                        {
                                            CourseId = chromosome.CourseId,
                                            PlaceId = chromosome.PlaceId,
                                            Day = (DayOfWeek)chromosome.Day,
                                            Start = chromosome.StartAt,
                                            End = chromosome.EndAt,
                                        }).ToList();
        dataContext.TimeSlots.AddRange(timetable);
        dataContext.SaveChanges();
    }

    static void Seed(DataContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.Migrate();
        var room201 = new Place()
        {
            Name = "Room 201"
        };
        
        var room202 = new Place()
        {
            Name = "Room 202"
        };
        var room203 = new Place()
        {
            Name = "Room 203"
        };
        var room204 = new Place()
        {
            Name = "Room 204"
        };
        var room205 = new Place()
        {
            Name = "Room 205"
        };
        context.Places.Add(room201);
        context.Places.Add(room202);
        context.Places.Add(room203);
        context.Places.Add(room204);
        context.Places.Add(room205);

        var mrJoe = new Teacher()
        {
            Name = "Mr Joe"
        };
        var mrSmith = new Teacher()
        {
            Name = "Mr Smith"
        };
        var mrsAnn = new Teacher()
        {
            Name = "Mrs Ann"
        };
        context.Teachers.Add(mrJoe);
        context.Teachers.Add(mrSmith);
        context.Teachers.Add(mrsAnn);

        var scienceByMrJoe = new Course()
        {
            Title = "Science",
            Teacher = mrJoe,
        };
        var scienceByMrSmith = new Course()
        {
            Title = "Science",
            Teacher = mrSmith,
        };
        var pe = new Course()
        {
            Title = "PE",
            Teacher = mrsAnn,
        };
        var art = new Course()
        {
            Title = "Art",
            Teacher = mrsAnn,
        };
        context.Courses.Add(scienceByMrJoe);
        context.Courses.Add(scienceByMrSmith);
        context.Courses.Add(pe);
        context.Courses.Add(art);

        context.Students.AddRange(new List<Student>()
        {
            new()
            {
                Name = "Noah",
                Email = "noah@example.edu",
                Courses = new List<StudentCourse>()
                {
                    new()
                    {
                        Course = pe,
                    },
                    new()
                    {
                        Course = scienceByMrJoe,
                    }
                },
            },
            new()
            {
                Name = "Tyler",
                Email = "tyler@example.edu",
                Courses = new List<StudentCourse>()
                {
                    new()
                    {
                        Course = pe,
                    },
                    new()
                    {
                        Course = scienceByMrJoe,
                    }
                },
            },
            new()
            {
                Name = "Aaron",
                Email = "aaron@example.edu",
                Courses = new List<StudentCourse>()
                {
                    new()
                    {
                        Course = pe,
                    },
                    new()
                    {
                        Course = scienceByMrSmith,
                    }
                },
            },
            new()
            {
                Name = "Sindy",
                Email = "sindy@example.edu",
                Courses = new List<StudentCourse>()
                {
                    new()
                    {
                        Course = art,
                    },
                    new()
                    {
                        Course = pe,
                    }
                },
            },
            new()
            {
                Name = "Jenny",
                Email = "jenny@example.edu",
                Courses = new List<StudentCourse>()
                {
                    new()
                    {
                        Course = art,
                    }
                },
            },
            new()
            {
                Name = "Bob",
                Email = "Bob@example.edu",
                Courses = new List<StudentCourse>()
                {
                    new()
                    {
                        Course = art,
                    },
                    new()
                    {
                        Course = pe,
                    },
                    new()
                    {
                        Course = scienceByMrSmith,
                    }
                },
            },
            new()
            {
                Name = "Thomas",
                Email = "tom@example.edu",
                Courses = new List<StudentCourse>()
                {
                    new()
                    {
                        Course = art,
                    },
                    new()
                    {
                        Course = pe,
                    },
                    new()
                    {
                        Course = scienceByMrSmith,
                    }
                },
            },
            new()
            {
                Name = "John",
                Email = "john@example.edu",
                Courses = new List<StudentCourse>()
                {
                    new()
                    {
                        Course = art,
                    },
                    new()
                    {
                        Course = pe,
                    },
                    new()
                    {
                        Course = scienceByMrJoe,
                    }
                },
            },
            new()
            {
                Name = "Zach",
                Email = "zach@example.edu",
                Courses = new List<StudentCourse>()
                {
                    new()
                    {
                        Course = art,
                    },
                    new()
                    {
                        Course = pe,
                    },
                    new()
                    {
                        Course = scienceByMrJoe,
                    }
                },
            },
            new()
            {
                Name = "Cody",
                Email = "cody@example.edu",
                Courses = new List<StudentCourse>()
                {
                    new()
                    {
                        Course = art,
                    },
                    new()
                    {
                        Course = pe,
                    },
                    new()
                    {
                        Course = scienceByMrJoe,
                    }
                },
            },
        });
        context.SaveChanges();

    }
}
