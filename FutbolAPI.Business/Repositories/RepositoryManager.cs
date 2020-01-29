using FutbolAPI.Business.Models;

namespace FutbolAPI.Business.Repositories
{
    public interface IRepositoryManager
    {
        IRepository<Manager> Manager { get; }
        IRepository<Player> Player { get; }
        IRepository<Referee> Referee { get; }
        IRepository<Match> Match { get; }
        IRepository<MatchPlayerAway> MatchPlayerAway { get; }
        IRepository<MatchPlayerHome> MatchPlayerHome { get; }

    }
    public class RepositoryManager: IRepositoryManager
    {
        private readonly IRepository<Manager> manager;
        private readonly IRepository<Player> player;
        private readonly IRepository<MatchPlayerAway> matchPlayerAway;
        private readonly IRepository<MatchPlayerHome> matchPlayerHome;
        private readonly IRepository<Referee> referee;
        private readonly IRepository<Match> match;

        public RepositoryManager(IRepository<Manager> Manager,
        IRepository<Player> Player,
        IRepository<MatchPlayerAway> MatchPlayerAway,
        IRepository<MatchPlayerHome> MatchPlayerHome,
        IRepository<Referee> Referee,
        IRepository<Match> Match )
        {
            manager = Manager;
            player = Player;
            matchPlayerAway = MatchPlayerAway;
            matchPlayerHome = MatchPlayerHome;
            referee = Referee;
            match = Match;
        }

        public IRepository<Manager> Manager => manager;
        public IRepository<Player> Player => player;

        public IRepository<MatchPlayerAway> MatchPlayerAway => matchPlayerAway;
        public IRepository<MatchPlayerHome> MatchPlayerHome => matchPlayerHome;

        public IRepository<Referee> Referee => referee;

        public IRepository<Match> Match => match;
    }
}
