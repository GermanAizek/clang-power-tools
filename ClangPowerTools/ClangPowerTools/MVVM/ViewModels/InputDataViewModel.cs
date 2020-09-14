﻿using ClangPowerTools.MVVM.Commands;
using ClangPowerTools.MVVM.Models;
using ClangPowerTools.MVVM.Views;
using ClangPowerTools.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace ClangPowerTools
{
  public class InputDataViewModel : CommonSettingsFunctionality, INotifyPropertyChanged
  {
    #region Members

    public event PropertyChangedEventHandler PropertyChanged;

    private string inputToAdd;
    private string placeholder;
    private InputDataView inputDataView;
    private ICommand addCommand;
    private ICommand detectFormatStyleCommand;
    private string warningText;
    private readonly DetectFormatStyleMenuView view;

    #endregion

    #region Constructor

    public InputDataViewModel(string content)
    {
      CreateInputsCollection(content);
    }

    public InputDataViewModel() { }

    public InputDataViewModel(bool browse)
    {
      BrowseForFiles = browse;
    }

    public InputDataViewModel(DetectFormatStyleMenuView view, bool browse)
    {
      this.view = view;
      BrowseForFiles = browse;
    }

    #endregion


    #region Properties

    private bool BrowseForFiles { get; set; } = false;

    public string InputToAdd
    {
      get
      {
        return inputToAdd;
      }
      set
      {
        inputToAdd = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InputToAdd"));
      }
    }

    public string Placeholder
    {
      get
      {
        return placeholder;
      }
      set
      {
        placeholder = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Placeholder"));
      }
    }

    public string WarningText
    {
      get
      {
        return warningText;
      }
      set
      {
        warningText = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WarningText"));
      }
    }

    public ObservableCollection<InputDataModel> Inputs { get; set; } = new ObservableCollection<InputDataModel>();

    public ICommand AddCommand
    {
      get => addCommand ?? (addCommand = new RelayCommand(() => AddInput(), () => CanExecute));
    }

    public ICommand DetectFormatStyleCommand
    {
      get => detectFormatStyleCommand ?? (detectFormatStyleCommand = new RelayCommand(() => DetectFormatStyle(), () => CanExecute));
    }

    public bool CanExecute
    {
      get
      {
        return true;
      }
    }

    #endregion


    #region Methods

    public void ShowViewDialog()
    {
      inputDataView = new InputDataView(this);
      inputDataView.ShowDialog();
    }

    public void DeleteInput(int index)
    {
      if (index >= 0)
        Inputs.RemoveAt(index);
    }

    private void AddInput()
    {
      if (BrowseForFiles && string.IsNullOrWhiteSpace(inputToAdd))
      {
        AddBrowseFilePathsToCollection();
      }
      else
      {
        AddInputToCollection();
      }

      WarningText = "WARNIG";
      view.WarningTextBox.Visibility = System.Windows.Visibility.Visible;

      if (Inputs.Count > 20)
      {

      }
    }

    private void AddBrowseFilePathsToCollection()
    {
      var filePaths = OpenFiles(string.Empty, ".cpp", ScriptConstants.FileExtensionsSelectFile);

      if (filePaths == null || filePaths.Length <= 0)
        return;

      foreach (var path in filePaths)
        Inputs.Add(new InputDataModel(path));
    }

    private void AddInputToCollection()
    {
      if (string.IsNullOrWhiteSpace(inputToAdd) == false)
      {
        Inputs.Add(new InputDataModel(inputToAdd));
        InputToAdd = string.Empty;
      }
    }

    private void CreateInputsCollection(string content)
    {
      if (string.IsNullOrWhiteSpace(content))
        return;

      var splitContent = content.Split(';').ToList();
      foreach (var item in splitContent)
      {
        Inputs.Add(new InputDataModel(item));
      }
    }

    private void DetectFormatStyle()
    {

    }

    #endregion
  }
}
