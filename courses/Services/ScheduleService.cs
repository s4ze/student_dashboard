using courses.Contracts;
using courses.Data;
using courses.Models;

namespace courses.Services;

public class ScheduleService(DataContext context)
{
    private readonly DataContext _context = context;
    public bool CheckIfScheduleExists(string number)
    {
        return _context.Schedules.Any(s => s.Group == number);
    }
    public Schedule GetGroupSchedule(string number)
    {
        return _context.Schedules.First(s => s.Group == number);
    }
    public Schedule CreateGroupSchedule(string number, GroupScheduleRequest data)
    {
        var schedule = new Schedule()
        {
            ScheduleId = new Guid(),
            Group = number,
            Monday = data.Monday,
            Tuesday = data.Tuesday,
            Wednesday = data.Wednesday,
            Thursday = data.Thursday,
            Friday = data.Friday,
            Saturday = data.Saturday
        };
        _context.Schedules.Add(schedule);
        _context.SaveChanges();
        return schedule;
    }
    public Schedule EditGroupSchedule(string number, GroupScheduleRequest data)
    {
        var schedule = GetGroupSchedule(number);
        schedule.Monday = data.Monday;
        schedule.Tuesday = data.Tuesday;
        schedule.Wednesday = data.Wednesday;
        schedule.Thursday = data.Thursday;
        schedule.Friday = data.Friday;
        schedule.Saturday = data.Saturday;
        _context.SaveChanges();
        return schedule;
    }
    public void RemoveGroupSchedule(string number)
    {
        var schedule = GetGroupSchedule(number);
        _context.Schedules.Remove(schedule);
        _context.SaveChanges();
    }
    public object ConvertScheduleToResponse(Schedule schedule)
    {
        return new
        {
            monday = new
            {
                lesson1 = new
                {
                    course = schedule.Monday?.Lesson1?.Course,
                    professor = schedule.Monday?.Lesson1?.Professor,
                    classroom = schedule.Monday?.Lesson1?.Classroom,
                    form = schedule.Monday?.Lesson1?.Form
                },
                lesson2 = new
                {
                    course = schedule.Monday?.Lesson2?.Course,
                    professor = schedule.Monday?.Lesson2?.Professor,
                    classroom = schedule.Monday?.Lesson2?.Classroom,
                    form = schedule.Monday?.Lesson2?.Form
                },
                lesson3 = new
                {
                    course = schedule.Monday?.Lesson3?.Course,
                    professor = schedule.Monday?.Lesson3?.Professor,
                    classroom = schedule.Monday?.Lesson3?.Classroom,
                    form = schedule.Monday?.Lesson3?.Form
                },
                lesson4 = new
                {
                    course = schedule.Monday?.Lesson4?.Course,
                    professor = schedule.Monday?.Lesson4?.Professor,
                    classroom = schedule.Monday?.Lesson4?.Classroom,
                    form = schedule.Monday?.Lesson4?.Form
                },
                lesson5 = new
                {
                    course = schedule.Monday?.Lesson5?.Course,
                    professor = schedule.Monday?.Lesson5?.Professor,
                    classroom = schedule.Monday?.Lesson5?.Classroom,
                    form = schedule.Monday?.Lesson5?.Form
                }
            },
            tuesday = new
            {
                lesson1 = new
                {
                    course = schedule.Tuesday?.Lesson1?.Course,
                    professor = schedule.Tuesday?.Lesson1?.Professor,
                    classroom = schedule.Tuesday?.Lesson1?.Classroom,
                    form = schedule.Tuesday?.Lesson1?.Form
                },
                lesson2 = new
                {
                    course = schedule.Tuesday?.Lesson2?.Course,
                    professor = schedule.Tuesday?.Lesson2?.Professor,
                    classroom = schedule.Tuesday?.Lesson2?.Classroom,
                    form = schedule.Tuesday?.Lesson2?.Form
                },
                lesson3 = new
                {
                    course = schedule.Tuesday?.Lesson3?.Course,
                    professor = schedule.Tuesday?.Lesson3?.Professor,
                    classroom = schedule.Tuesday?.Lesson3?.Classroom,
                    form = schedule.Tuesday?.Lesson3?.Form
                },
                lesson4 = new
                {
                    course = schedule.Tuesday?.Lesson4?.Course,
                    professor = schedule.Tuesday?.Lesson4?.Professor,
                    classroom = schedule.Tuesday?.Lesson4?.Classroom,
                    form = schedule.Tuesday?.Lesson4?.Form
                },
                lesson5 = new
                {
                    course = schedule.Tuesday?.Lesson5?.Course,
                    professor = schedule.Tuesday?.Lesson5?.Professor,
                    classroom = schedule.Tuesday?.Lesson5?.Classroom,
                    form = schedule.Tuesday?.Lesson5?.Form
                }
            },
            wednesday = new
            {
                lesson1 = new
                {
                    course = schedule.Wednesday?.Lesson1?.Course,
                    professor = schedule.Wednesday?.Lesson1?.Professor,
                    classroom = schedule.Wednesday?.Lesson1?.Classroom,
                    form = schedule.Wednesday?.Lesson1?.Form
                },
                lesson2 = new
                {
                    course = schedule.Wednesday?.Lesson2?.Course,
                    professor = schedule.Wednesday?.Lesson2?.Professor,
                    classroom = schedule.Wednesday?.Lesson2?.Classroom,
                    form = schedule.Wednesday?.Lesson2?.Form
                },
                lesson3 = new
                {
                    course = schedule.Wednesday?.Lesson3?.Course,
                    professor = schedule.Wednesday?.Lesson3?.Professor,
                    classroom = schedule.Wednesday?.Lesson3?.Classroom,
                    form = schedule.Wednesday?.Lesson3?.Form
                },
                lesson4 = new
                {
                    course = schedule.Wednesday?.Lesson4?.Course,
                    professor = schedule.Wednesday?.Lesson4?.Professor,
                    classroom = schedule.Wednesday?.Lesson4?.Classroom,
                    form = schedule.Wednesday?.Lesson4?.Form
                },
                lesson5 = new
                {
                    course = schedule.Wednesday?.Lesson5?.Course,
                    professor = schedule.Wednesday?.Lesson5?.Professor,
                    classroom = schedule.Wednesday?.Lesson5?.Classroom,
                    form = schedule.Wednesday?.Lesson5?.Form
                }
            },
            thursday = new
            {
                lesson1 = new
                {
                    course = schedule.Thursday?.Lesson1?.Course,
                    professor = schedule.Thursday?.Lesson1?.Professor,
                    classroom = schedule.Thursday?.Lesson1?.Classroom,
                    form = schedule.Thursday?.Lesson1?.Form
                },
                lesson2 = new
                {
                    course = schedule.Thursday?.Lesson2?.Course,
                    professor = schedule.Thursday?.Lesson2?.Professor,
                    classroom = schedule.Thursday?.Lesson2?.Classroom,
                    form = schedule.Thursday?.Lesson2?.Form
                },
                lesson3 = new
                {
                    course = schedule.Thursday?.Lesson3?.Course,
                    professor = schedule.Thursday?.Lesson3?.Professor,
                    classroom = schedule.Thursday?.Lesson3?.Classroom,
                    form = schedule.Thursday?.Lesson3?.Form
                },
                lesson4 = new
                {
                    course = schedule.Thursday?.Lesson4?.Course,
                    professor = schedule.Thursday?.Lesson4?.Professor,
                    classroom = schedule.Thursday?.Lesson4?.Classroom,
                    form = schedule.Thursday?.Lesson4?.Form
                },
                lesson5 = new
                {
                    course = schedule.Thursday?.Lesson5?.Course,
                    professor = schedule.Thursday?.Lesson5?.Professor,
                    classroom = schedule.Thursday?.Lesson5?.Classroom,
                    form = schedule.Thursday?.Lesson5?.Form
                }
            },
            friday = new
            {
                lesson1 = new
                {
                    course = schedule.Friday?.Lesson1?.Course,
                    professor = schedule.Friday?.Lesson1?.Professor,
                    classroom = schedule.Friday?.Lesson1?.Classroom,
                    form = schedule.Friday?.Lesson1?.Form
                },
                lesson2 = new
                {
                    course = schedule.Friday?.Lesson2?.Course,
                    professor = schedule.Friday?.Lesson2?.Professor,
                    classroom = schedule.Friday?.Lesson2?.Classroom,
                    form = schedule.Friday?.Lesson2?.Form
                },
                lesson3 = new
                {
                    course = schedule.Friday?.Lesson3?.Course,
                    professor = schedule.Friday?.Lesson3?.Professor,
                    classroom = schedule.Friday?.Lesson3?.Classroom,
                    form = schedule.Friday?.Lesson3?.Form
                },
                lesson4 = new
                {
                    course = schedule.Friday?.Lesson4?.Course,
                    professor = schedule.Friday?.Lesson4?.Professor,
                    classroom = schedule.Friday?.Lesson4?.Classroom,
                    form = schedule.Friday?.Lesson4?.Form
                },
                lesson5 = new
                {
                    course = schedule.Friday?.Lesson5?.Course,
                    professor = schedule.Friday?.Lesson5?.Professor,
                    classroom = schedule.Friday?.Lesson5?.Classroom,
                    form = schedule.Friday?.Lesson5?.Form
                }
            },
            saturday = new
            {
                lesson1 = new
                {
                    course = schedule.Saturday?.Lesson1?.Course,
                    professor = schedule.Saturday?.Lesson1?.Professor,
                    classroom = schedule.Saturday?.Lesson1?.Classroom,
                    form = schedule.Saturday?.Lesson1?.Form
                },
                lesson2 = new
                {
                    course = schedule.Saturday?.Lesson2?.Course,
                    professor = schedule.Saturday?.Lesson2?.Professor,
                    classroom = schedule.Saturday?.Lesson2?.Classroom,
                    form = schedule.Saturday?.Lesson2?.Form
                },
                lesson3 = new
                {
                    course = schedule.Saturday?.Lesson3?.Course,
                    professor = schedule.Saturday?.Lesson3?.Professor,
                    classroom = schedule.Saturday?.Lesson3?.Classroom,
                    form = schedule.Saturday?.Lesson3?.Form
                },
                lesson4 = new
                {
                    course = schedule.Saturday?.Lesson4?.Course,
                    professor = schedule.Saturday?.Lesson4?.Professor,
                    classroom = schedule.Saturday?.Lesson4?.Classroom,
                    form = schedule.Saturday?.Lesson4?.Form
                },
                lesson5 = new
                {
                    course = schedule.Saturday?.Lesson5?.Course,
                    professor = schedule.Saturday?.Lesson5?.Professor,
                    classroom = schedule.Saturday?.Lesson5?.Classroom,
                    form = schedule.Saturday?.Lesson5?.Form
                }
            }
        };
    }
}
