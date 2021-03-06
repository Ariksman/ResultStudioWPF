﻿using System;
using ResultStudioWPF.Common.CQS;
using ResultStudioWPF.Domain.DomainModel.ValueObjects;

namespace ResultStudioWPF.Domain.UseCases.DataSet
{
  public class GetRandomDataSetQuery : IQuery
  {
    public int FrameCount { get; }
    public int Spread { get; }
    public IProgress<int> Progress { get; }
    public Reference Reference { get; }

    public GetRandomDataSetQuery(double xReference, double yReference, double zReference, int frameCount, int spread, IProgress<int> progress = null)
    {
      FrameCount = frameCount;
      Spread = spread;
      Progress = progress;
      Reference = new Reference(xReference, yReference, zReference);
    }
  }
}
