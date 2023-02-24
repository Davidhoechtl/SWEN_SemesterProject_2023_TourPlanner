﻿using Caliburn.Micro;
using MTCG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourPlannerBackEnd.Infrastructure;
using TourPlannerBackEnd.Models;
using TourPlannerBackEnd.Repositories;
using TourPlannerFrontEnd.Infrastructure;

namespace TourPlannerFrontEnd.Modules.CreateTour
{
    class CreateTourScreenViewModel : Screen
    {
        private readonly ShellViewModel conductor;
        private readonly TourRepository tourRepository;
        private readonly UnitOfWorkFactory unitOfWorkFactory;

        public CreateTourViewModel CreateTourViewModel { get; init; }


        public CreateTourScreenViewModel(
            ShellViewModel conductor, 
            TourRepository tourRepository, 
            UnitOfWorkFactory unitOfWorkFactory, 
            MapQuestApiService mapQuestApiService)
        {
            this.conductor = conductor;
            this.tourRepository = tourRepository;
            this.unitOfWorkFactory = unitOfWorkFactory;
            DisplayName = "Create Tour";
            CreateTourViewModel = new CreateTourViewModel(tourRepository, unitOfWorkFactory, mapQuestApiService);
            CreateTourViewModel.Model = new Tour();
        }

  
        public async Task Back()
        {
            await conductor.NavigateBackOneStep(new System.Threading.CancellationToken());
        }


    }
}
