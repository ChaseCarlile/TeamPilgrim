﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Command;
using JustAProgrammer.TeamPilgrim.VisualStudio.Common;
using JustAProgrammer.TeamPilgrim.VisualStudio.Domain.Entities;
using JustAProgrammer.TeamPilgrim.VisualStudio.Providers;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Build.Controls;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.VisualStudio.TeamFoundation.WorkItemTracking;

namespace JustAProgrammer.TeamPilgrim.VisualStudio.Model
{
    public class ProjectBuildModel : BaseModel
    {
        private readonly IPilgrimServiceModelProvider _pilgrimServiceModelProvider;
        private readonly TfsTeamProjectCollection _collection;
        private readonly Project _project;

        public ProjectBuildModel(IPilgrimServiceModelProvider pilgrimServiceModelProvider, TfsTeamProjectCollection collection, Project project)
        {
            _pilgrimServiceModelProvider = pilgrimServiceModelProvider;
            _collection = collection;
            _project = project;

            OpenBuildDefintionCommand = new RelayCommand<BuildDefinitionWrapper>(OpenBuildDefinition, CanOpenBuildDefinition);
            ViewBuildsCommand = new RelayCommand<BuildDefinitionWrapper>(ViewBuilds, CanViewBuilds);

            BuildDefinitionWrapper[] buildDefinitions;
            if (_pilgrimServiceModelProvider.TryGetBuildDefinitionsByProjectName(out buildDefinitions, _collection, _project.Name))
            {
                BuildDefinitions = buildDefinitions;
            }
        }

        #region OpenBuildDefinition Command
        
        public RelayCommand<BuildDefinitionWrapper> OpenBuildDefintionCommand { get; set; }

        private bool CanOpenBuildDefinition(BuildDefinitionWrapper buildDefinitionWrapper)
        {
            return true;
        }

        private void OpenBuildDefinition(BuildDefinitionWrapper buildDefinitionWrapper)
        {
            TeamPilgrimPackage.TeamPilgrimVsService.OpenBuildDefinition(buildDefinitionWrapper.Uri);
        }

        #endregion

        #region ViewBuilds Command

        public RelayCommand<BuildDefinitionWrapper> ViewBuildsCommand { get; set; }

        private bool CanViewBuilds(BuildDefinitionWrapper buildDefinitionWrapper)
        {
            return true;
        }

        private void ViewBuilds(BuildDefinitionWrapper buildDefinitionWrapper)
        {
            TeamPilgrimPackage.TeamPilgrimVsService.ViewBuilds(_project.Name, buildDefinitionWrapper.Name, String.Empty, DateFilter.Today);
            //TeamPilgrimPackage.TeamFoundationBuild.BuildExplorer.CompletedView.Show(_project.Name, buildDefinitionWrapper.Name, String.Empty, DateFilter.Today);
        }

        #endregion

        #region BuildModel

        private BuildDefinitionWrapper[] _buildDefinitions;

        public BuildDefinitionWrapper[] BuildDefinitions
        {
            get
            {
                return _buildDefinitions;
            }
            set
            {
                if (_buildDefinitions == value) return;

                _buildDefinitions = value;
                SendPropertyChanged("BuildDefinitions");
            }
        }

        #endregion
    }
}
