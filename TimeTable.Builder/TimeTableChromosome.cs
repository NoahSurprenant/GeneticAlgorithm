using GeneticSharp;
using Microsoft.EntityFrameworkCore;
using TimeTable.Core;

namespace TimeTable.Builder;
public class TimeTableChromosome : ChromosomeBase
{
    private readonly DataContext _dataContext;
    private readonly int _geneCount;
    static Random Random = new Random();

    static TimeSpan RandomStartTime()
    {
        return TimeSpan.FromMilliseconds(Random.Next((int)TimeSpan.FromHours(9).TotalMilliseconds,
            (int)TimeSpan.FromHours(17).TotalMilliseconds));
    }

    public TimeTableChromosome(DataContext dataContext, int geneCount) : base(geneCount)
    {
        _geneCount = geneCount;
        _dataContext = dataContext;
        CreateGenes();
    }

    public override Gene GenerateGene(int geneIndex)
    {
        var course = _dataContext.Courses
                .Include(course => course.Teacher)
                .Include(course => course.Students)
                .Skip(geneIndex)
                .FirstOrDefault() ?? throw new Exception($"Missing course with index {geneIndex}");

        return new Gene(new TimeSlotChromosome()
        {
            Students = course.Students.Select(student => student.StudentId).ToList(),
            CourseId = course.Id,
            StartAt = RandomStartTime(),
            PlaceId = _dataContext.Places.OrderBy(place => Guid.NewGuid()).FirstOrDefault().Id,
            TeacherId = course.Teacher.Id,
            Day = Random.Next(1, 5)
        });
    }

    public override IChromosome CreateNew()
    {
        var timeTableChromosome = new TimeTableChromosome(_dataContext, _geneCount);
        timeTableChromosome.CreateGenes();
        return timeTableChromosome;
    }
}
