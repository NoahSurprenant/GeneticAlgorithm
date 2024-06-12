using System.Diagnostics.CodeAnalysis;

namespace TimeTable.Builder;
public struct TimeSlotChromosome
{
    public TimeSpan StartAt { get; set; }
    public TimeSpan EndAt => StartAt.Add(TimeSpan.FromHours(3));
    public int CourseId { get; set; }
    public int PlaceId { get; set; }
    public int TeacherId { get; set; }
    public List<int> Students { get; set; }
    public int Day { get; set; }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is null) return false;

        if (obj is not TimeSlotChromosome other) return false;

        if (!StartAt.Equals(other.StartAt)) return false;

        if (!CourseId.Equals(other.CourseId)) return false;

        if (!PlaceId.Equals(other.PlaceId)) return false;

        if (!TeacherId.Equals(other.TeacherId)) return false;

        //if (!Students.Equals(other.Students)) return false;

        if (!Day.Equals(other.Day)) return false;

        return true;
    }
}
