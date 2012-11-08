using JustAProgrammer.TeamPilgrim.VisualStudio.Domain.BusinessInterfaces;
using JustAProgrammer.TeamPilgrim.VisualStudio.Providers;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace JustAProgrammer.TeamPilgrim.VisualStudio.Model.PendingChanges
{
    public class PendingChangeModel : BaseModel
    {
        public PendingChange Change { get; private set; }

        public PendingChangeModel(ITeamPilgrimServiceModelProvider teamPilgrimServiceModelProvider, ITeamPilgrimVsService teamPilgrimVsService, PendingChange change)
            : base(teamPilgrimServiceModelProvider, teamPilgrimVsService)
        {
            Change = change;
        }

        private bool _includeChange;

        public bool IncludeChange
        {
            get
            {
                return _includeChange;
            }
            set
            {
                if (_includeChange == value) return;

                _includeChange = value;

                SendPropertyChanged("IncludeChange");
            }
        }
    }
}