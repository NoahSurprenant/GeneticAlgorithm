using GeneticSharp;

namespace TimeTable.Builder;
public class TimeTableEvaluator : IFitness
{
    public double Evaluate(IChromosome chromosome)
    {
        double score = 1;

        if (chromosome is not TimeTableChromosome ttc)
            throw new Exception("TimeTableEvaluator can only evaluate TimeTableChromosomes");

        var values = chromosome.GetGenes().Select(x => (TimeSlotChromosome)x.Value).ToList();

        var GetoverLaps = new Func<TimeSlotChromosome, List<TimeSlotChromosome>>(current => values
            .Except(new[] { current })
            .Where(slot => slot.Day == current.Day)
            .Where(slot => slot.StartAt == current.StartAt
                          || slot.StartAt <= current.EndAt && slot.StartAt >= current.StartAt
                          || slot.EndAt >= current.StartAt && slot.EndAt <= current.EndAt)
            .ToList());

        foreach (var value in values)
        {
            var overLaps = GetoverLaps(value);
            score -= overLaps.GroupBy(slot => slot.TeacherId).Sum(x => x.Count() - 1);
            score -= overLaps.GroupBy(slot => slot.PlaceId).Sum(x => x.Count() - 1);
            score -= overLaps.GroupBy(slot => slot.CourseId).Sum(x => x.Count() - 1);
            score -= overLaps.Sum(item => item.Students.Intersect(value.Students).Count());
        }

        score -= values.GroupBy(v => v.Day).Count() * 0.5;
        return Math.Pow(Math.Abs(score), -1);
    }
}
