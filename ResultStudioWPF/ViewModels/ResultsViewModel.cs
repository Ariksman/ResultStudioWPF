﻿using System;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ResultStudioWPF.Domain.DomainModel.Enumerations;
using ResultStudioWPF.ViewModels.Messages;
using LinearAxis = OxyPlot.Axes.LinearAxis;

namespace ResultStudioWPF.ViewModels
{
  public class ResultsViewModel : ViewModelBase
  {
    private readonly ISharedSettingsContext _settingsContext;
    private IEnumerable<MeasurementPointViewModel> _dataSet;

    public ResultsViewModel(ISharedSettingsContext settingsContext)
    {

      if (IsInDesignMode)
      {
        // Code runs in Blend --> create design time data.
      }
      else
      {
        // Code runs "for real"
        _settingsContext = settingsContext ?? throw new ArgumentNullException(nameof(settingsContext));

        // Messages
        AppMessages.PlotDataSet.Register(this, DrawPlotsForDataSet);

        MeasurementsX = new List<DataPoint>();
        MeasurementsY = new List<DataPoint>();
        MeasurementsZ = new List<DataPoint>();

        ReferenceX = new List<DataPoint>();
        ReferenceY = new List<DataPoint>();
        ReferenceZ = new List<DataPoint>();

        PlotModelX = new PlotModel();
        PlotModelY = new PlotModel();
        PlotModelZ = new PlotModel();

        SetUpModel();
      }
    }

    #region Public properties

    private PlotModel _plotModelX;

    public PlotModel PlotModelX
    {
      get => _plotModelX;
      set
      {
        _plotModelX = value;
        RaisePropertyChanged();
      }
    }

    private PlotModel _plotModelY;

    public PlotModel PlotModelY
    {
      get => _plotModelY;
      set
      {
        _plotModelY = value;
        RaisePropertyChanged();
      }
    }

    private PlotModel _plotModelZ;

    public PlotModel PlotModelZ
    {
      get => _plotModelZ;
      set
      {
        _plotModelZ = value;
        RaisePropertyChanged();
      }
    }

    private List<DataPoint> _referenceX;

    public List<DataPoint> ReferenceX
    {
      get => _referenceX;
      set
      {
        if (_referenceX == value)
        {
          return;
        }

        _referenceX = value;
      }
    }

    private List<DataPoint> _referenceY;

    public List<DataPoint> ReferenceY
    {
      get => _referenceY;
      set
      {
        if (_referenceY == value)
        {
          return;
        }

        _referenceY = value;
      }
    }

    private List<DataPoint> _referenceZ;

    public List<DataPoint> ReferenceZ
    {
      get => _referenceZ;
      set
      {
        if (_referenceZ == value)
        {
          return;
        }

        _referenceZ = value;
      }
    }

    private List<DataPoint> _measurementsX;

    public List<DataPoint> MeasurementsX
    {
      get => _measurementsX;
      set
      {
        if (_measurementsX == value)
        {
          return;
        }

        _measurementsX = value;
      }
    }

    private List<DataPoint> _measurementsY;

    public List<DataPoint> MeasurementsY
    {
      get => _measurementsY;
      set
      {
        if (_measurementsY == value)
        {
          return;
        }

        _measurementsY = value;
      }
    }

    private List<DataPoint> _measurementsZ;

    public List<DataPoint> MeasurementsZ
    {
      get => _measurementsZ;
      set
      {
        if (_measurementsZ == value)
        {
          return;
        }

        _measurementsZ = value;
      }
    }

    #endregion

    #region private methods

    private void SetUpModel()
    {
      var measurementNumberAxisX = new LinearAxis() { Position = AxisPosition.Bottom };
      var measurementValueAxisX = new LinearAxis()
      { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };

      var measurementNumberAxisY = new LinearAxis() { Position = AxisPosition.Bottom };
      var measurementValueAxisY = new LinearAxis()
      { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };

      var measurementNumberAxisZ = new LinearAxis() { Position = AxisPosition.Bottom };
      var measurementValueAxisZ = new LinearAxis()
      { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };

      PlotModelX.LegendOrientation = LegendOrientation.Horizontal;
      PlotModelX.LegendPlacement = LegendPlacement.Inside;
      PlotModelX.LegendPosition = LegendPosition.TopRight;
      PlotModelX.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
      PlotModelX.LegendBorder = OxyColors.Black;
      PlotModelX.Title = "X-axis";

      PlotModelX.Axes.Add(measurementNumberAxisX);
      PlotModelX.Axes.Add(measurementValueAxisX);

      PlotModelY.LegendOrientation = LegendOrientation.Horizontal;
      PlotModelY.LegendPlacement = LegendPlacement.Inside;
      PlotModelY.LegendPosition = LegendPosition.TopRight;
      PlotModelY.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
      PlotModelY.LegendBorder = OxyColors.Black;
      PlotModelY.Title = "Y-axis";

      PlotModelY.Axes.Add(measurementNumberAxisY);
      PlotModelY.Axes.Add(measurementValueAxisY);

      PlotModelZ.LegendOrientation = LegendOrientation.Horizontal;
      PlotModelZ.LegendPlacement = LegendPlacement.Inside;
      PlotModelZ.LegendPosition = LegendPosition.TopRight;
      PlotModelZ.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
      PlotModelZ.LegendBorder = OxyColors.Black;
      PlotModelZ.Title = "Z-axis";

      PlotModelZ.Axes.Add(measurementNumberAxisZ);
      PlotModelZ.Axes.Add(measurementValueAxisZ);
    }

    private void LoadDataForPlots()
    {
      PlotModelX.Series.Clear();
      PlotModelY.Series.Clear();
      PlotModelZ.Series.Clear();

      var lineSerieX = new LineSeries
      {
        StrokeThickness = 2,
        MarkerSize = 3,
        //MarkerStroke = colors[data.Key],
        //MarkerType = markerTypes[data.Key],
        CanTrackerInterpolatePoints = false,
        Title = "Value",
        ItemsSource = _measurementsX
      };

      var lineSerieXref = new LineSeries
      {
        StrokeThickness = 2,
        MarkerSize = 3,
        //MarkerStroke = colors[data.Key],
        //MarkerType = markerTypes[data.Key],
        CanTrackerInterpolatePoints = false,
        Title = "Reference",
        ItemsSource = _referenceX,
      };
      PlotModelX.Series.Add(lineSerieXref);
      PlotModelX.Series.Add(lineSerieX);

      var lineSerieY = new LineSeries
      {
        StrokeThickness = 2,
        MarkerSize = 3,
        //MarkerStroke = colors[data.Key],
        //MarkerType = markerTypes[data.Key],
        CanTrackerInterpolatePoints = false,
        Title = "Value",
        ItemsSource = _measurementsY
      };
      var lineSerieYref = new LineSeries
      {
        StrokeThickness = 2,
        MarkerSize = 3,
        //MarkerStroke = colors[data.Key],
        //MarkerType = markerTypes[data.Key],
        CanTrackerInterpolatePoints = false,
        Title = "Reference",
        ItemsSource = _referenceY,
      };
      PlotModelY.Series.Add(lineSerieYref);
      PlotModelY.Series.Add(lineSerieY);

      var lineSerieZ = new LineSeries
      {
        StrokeThickness = 2,
        MarkerSize = 3,
        //MarkerStroke = colors[data.Key],
        //MarkerType = markerTypes[data.Key],
        CanTrackerInterpolatePoints = false,
        Title = "Value",
        ItemsSource = _measurementsZ
      };
      var lineSerieZref = new LineSeries
      {
        StrokeThickness = 2,
        MarkerSize = 3,
        //MarkerStroke = colors[data.Key],
        //MarkerType = markerTypes[data.Key],
        CanTrackerInterpolatePoints = false,
        Title = "Reference",
        ItemsSource = _referenceZ,
      };
      PlotModelZ.Series.Add(lineSerieZref);
      PlotModelZ.Series.Add(lineSerieZ);
    }

    private void DrawPlotsForDataSet(IEnumerable<MeasurementPointViewModel> obj)
    {
      MeasurementsX.Clear();
      MeasurementsY.Clear();
      MeasurementsZ.Clear();

      ReferenceX.Clear();
      ReferenceY.Clear();
      ReferenceZ.Clear();

      _dataSet = _settingsContext.DataSet;

      CreateMeasurementDataForPlots();

      LoadDataForPlots();

      AppMessages.PlotRefresh.Send(true);
    }

    private void CreateMeasurementDataForPlots()
    {
      foreach (var measurementPoint in _dataSet)
      {
        if (!Equals(measurementPoint.Axis, MeasurementAxisType.X)) continue;
        _measurementsX.Add(new DataPoint(measurementPoint.Index, measurementPoint.Value));
        _referenceX.Add(new DataPoint(measurementPoint.Index,
          _settingsContext.XAxisReference));
      }

      foreach (var measurementPoint in _dataSet)
      {
        if (!Equals(measurementPoint.Axis, MeasurementAxisType.Y)) continue;
        _measurementsY.Add(new DataPoint(measurementPoint.Index, measurementPoint.Value));
        _referenceY.Add(new DataPoint(measurementPoint.Index,
          _settingsContext.YAxisReference));
      }

      foreach (var measurementPoint in _dataSet)
      {
        if (!Equals(measurementPoint.Axis, MeasurementAxisType.Z)) continue;
        _measurementsZ.Add(new DataPoint(measurementPoint.Index, measurementPoint.Value));
        _referenceZ.Add(new DataPoint(measurementPoint.Index,
          _settingsContext.ZAxisReference));
      }
    }

    #endregion
  }
}
