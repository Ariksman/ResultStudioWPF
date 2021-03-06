﻿using Autofac;
using ResultStudioWPF.Application.Interfaces;
using ResultStudioWPF.Domain.ImpureServices;
using ResultStudioWPF.Domain.Interfaces;
using ResultStudioWPF.Domain.PureServices;
using ResultStudioWPF.ViewModels;
using ResultStudioWPF.ViewModels.Services;
using Module = Autofac.Module;

namespace ResultStudioWPF.Infrastructure
{
  public class ViewModelServicesModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<MeasurementPointModel>().AsSelf();
      builder.RegisterType<FileDialogProvider>().As<IFileDialogProvider>();

      builder.RegisterType<FileDialogProvider>().As<IFileDialogProvider>();
      builder.RegisterType<DataFileReader>().As<IDataFileReader>();
      builder.RegisterType<OokiiMessageDialogService>().As<IMessageDialogService>();
      builder.RegisterType<DataCreator>().As<IDataCreator>();

      builder.RegisterType<DataSetModel>().AsSelf().As<ISharedSettingsContext>().SingleInstance();
      builder.RegisterType<DataSetViewModel>().AsSelf().SingleInstance();
      builder.RegisterType<MeasurementPointViewModel>().AsSelf();
      builder.RegisterType<ResultsViewModel>().AsSelf().SingleInstance();
    }
  }
}
