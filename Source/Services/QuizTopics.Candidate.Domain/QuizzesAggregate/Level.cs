using System.Linq;
using QuizTopics.Common.DomainDriven;

namespace QuizTopics.Candidate.Domain.QuizzesAggregate
{
    public class Level : Enumeration<Level>
    {
        public static readonly Level None = new(6, nameof(None), -1);
        public static readonly Level Easy = new(1, nameof(Easy), 15);
        public static readonly Level Medium = new(2, nameof(Medium), 25);
        public static readonly Level Hard = new(3, nameof(Hard), 35);
        public static readonly Level Max = new(4, nameof(Max), 45);

        private Level() { }

        private Level(int id, string name, int seconds)
            : base(id, name)
        {
            this.Seconds = seconds;
        }

        public int Seconds { get; }

        public static Level FindById(int id)
        {
            //NOTE : domain exception is better when null.
            return All.Single(x => x.Id == id);
        }
    }
}