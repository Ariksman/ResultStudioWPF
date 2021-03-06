﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.Interfaces;
using ResultStudioWPF.ViewModels;

namespace ResultStudioWPF.Mappers.Profiles
{
  public class MeasurementMappingProfile : Profile
  {
    public MeasurementMappingProfile()
    {
      CreateMap<MeasurementPointModel, MeasurementPoint>()
        .ForMember(dest => dest.Axis, opt => opt.MapFrom(src => src.MeasurementAxisType))
        .ReverseMap();
      CreateMap<IMeasurementPoint, MeasurementPointViewModel>().ReverseMap();
      CreateMap<MeasurementPoint, MeasurementPointViewModel>().ReverseMap();
    }
  }
}
