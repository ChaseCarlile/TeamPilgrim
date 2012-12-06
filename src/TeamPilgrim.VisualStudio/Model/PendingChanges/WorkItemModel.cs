using JustAProgrammer.TeamPilgrim.VisualStudio.Common.Enums;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace JustAProgrammer.TeamPilgrim.VisualStudio.Model.PendingChanges
{
    public class WorkItemModel : BaseModel
    {
        public WorkItemModel(WorkItem workItem)
        {
            WorkItem = workItem;
        }

        private WorkItem _workItem;
        public WorkItem WorkItem
        {
            get { return _workItem; }
            set
            {
                if (_workItem == value) return;

                _workItem = value;

                SendPropertyChanged("WorkItem");
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected == value) return;

                _isSelected = value;

                SendPropertyChanged("IsSelected");
            }
        }

        private SelectedWorkItemCheckinActionEnum _workItemCheckinAction;
        public SelectedWorkItemCheckinActionEnum WorkItemCheckinAction
        {
            get
            {
                return _workItemCheckinAction;
            }
            set
            {
                if (_workItemCheckinAction == value) return;

                _workItemCheckinAction = value;

                SendPropertyChanged("WorkItemCheckinAction");
            }
        }
    }
}