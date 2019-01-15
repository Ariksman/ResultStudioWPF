﻿using System;
using Autofac;
using ResultStudioWPF.Helpers;

namespace ResultStudioWPF.ViewModels
{
  public class AutofacModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<ResultsViewModel>().AsSelf().SingleInstance();
      builder.RegisterType<SettingsViewModel>().AsSelf().SingleInstance();

      Type[] types =
      {
        typeof (AnalyseDataSet),
        typeof (DataFileReader)
      };
      builder.RegisterTypes(types).AsImplementedInterfaces();
    }

  }
}
