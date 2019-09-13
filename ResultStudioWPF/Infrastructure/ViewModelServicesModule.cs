﻿using Autofac;
using ResultStudioWPF.Application.Interfaces;
using ResultStudioWPF.Domain.Interfaces;
using ResultStudioWPF.Domain.Services;
using ResultStudioWPF.ViewModels;
using ResultStudioWPF.ViewModels.Services;
using Module = Autofac.Module;

namespace ResultStudioWPF.Infrastructure
{
  public class ViewModelServicesModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<DataFileReader>().As<IDataFileReader>();
      builder.RegisterType<AnalyzeDataSet>().As<IAnalyseDataSet>();
      builder.RegisterType<DataCreator>().As<IDataCreator>();

      builder.RegisterType<SettingsViewModel>().AsSelf().SingleInstance();
      builder.RegisterType<ResultsViewModel>().AsSelf().SingleInstance();
    }
  }
}
