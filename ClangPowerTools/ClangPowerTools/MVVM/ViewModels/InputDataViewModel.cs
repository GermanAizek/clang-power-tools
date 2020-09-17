﻿using ClangPowerTools.MVVM.Commands;
using ClangPowerTools.MVVM.Models;
using ClangPowerTools.MVVM.Views;
using ClangPowerTools.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

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

    private const string MAX_FILE_WARNING = "This action will take some time due to the number of selected files.";

    private const long MAX_FILE_SIZE = 5000;
    private long totalFilesSize = 0;

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
      get => detectFormatStyleCommand ?? (detectFormatStyleCommand = new RelayCommand(() => DetectFormatStyleAsync().SafeFireAndForget(), () => CanExecute));
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
      if (index < 0 || index >= Inputs.Count)
        return;

      var model = Inputs[index];
      totalFilesSize -= model.FileSize;

      Inputs.RemoveAt(index);

      // Is not the last element update all the elements position from the index to the end
      if (index != Inputs.Count)
      {
        for (int position = index; position < Inputs.Count; ++position)
          Inputs[position].LineNumber = position + 1;
      }

      if (view == null)
        return;

      if (totalFilesSize <= MAX_FILE_SIZE)
        view.WarningTextBox.Visibility = System.Windows.Visibility.Hidden;
    }

    private void AddInput()
    {
      if (BrowseForFiles && string.IsNullOrWhiteSpace(inputToAdd))
        AddBrowseFilePathsToCollection();
      else
        AddInputToCollection();

      if (view == null)
        return;

      if (totalFilesSize > MAX_FILE_SIZE)
      {
        WarningText = MAX_FILE_WARNING;
        view.WarningTextBox.Foreground = Brushes.Orange;
        view.WarningTextBox.Visibility = System.Windows.Visibility.Visible;
      }
    }

    private void AddBrowseFilePathsToCollection()
    {
      var filePaths = OpenFiles(string.Empty, ".cpp", ScriptConstants.FileExtensionsSelectFile);

      if (filePaths == null || filePaths.Length <= 0)
        return;

      for (int index = 0; index < filePaths.Length; ++index)
      {
        int position = Inputs.Count == 0 ? 1 : Inputs.Last().LineNumber + 1;
        AddNewElement(filePaths[index], position);
      }
    }

    private void AddInputToCollection()
    {
      if (string.IsNullOrWhiteSpace(inputToAdd) == false)
      {
        int index = Inputs.Count == 0 ? 1 : Inputs.Last().LineNumber + 1;
        AddNewElement(inputToAdd, index);
        InputToAdd = string.Empty;
      }
    }

    private void CreateInputsCollection(string content)
    {
      if (string.IsNullOrWhiteSpace(content))
        return;

      var splitContent = content.Split(';').ToList();
      for (int index = 0; index < splitContent.Count; ++index)
        AddNewElement(splitContent[index], index + 1);
    }

    private void AddNewElement(string filePath, int index)
    {
      var model = new InputDataModel(filePath, index);
      totalFilesSize += model.FileSize;
      Inputs.Add(model);
    }

    private async Task DetectFormatStyleAsync()
    {
      var diffWin = new DiffWindow();
      List<string> filesPath = Inputs.Select(model => model.InputData).ToList();
      await diffWin.ShowDiffAsync(filesPath);
    }

    #endregion
  }
}
