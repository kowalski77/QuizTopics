using System.Linq;
using QuizTopics.Common;
using QuizTopics.Common.DomainDriven;

namespace QuizTopics.Candidate.Domain.QuizzesAggregate
{
    public class Level : Enumeration<Level>
    {
        public static readonly Level None = new(1, nameof(None), -1);
        public static readonly Level Easy = new(2, nameof(Easy), 15);
        public static readonly Level Medium = new(3, nameof(Medium), 25);
        public static readonly Level Hard = new(4, nameof(Hard), 35);
        public static readonly Level Max = new(5, nameof(Max), 45);

        private Level() { }

        private Level(int id, string name, int seconds)
            : base(id, name)
        {
            this.Seconds = seconds;
        }

        public int Seconds { get; }

        public static Level FindByName(string name)
        {
            //NOTE : domain exception is better
            return All.Single(x => x.Name == name);
        }
    }
}