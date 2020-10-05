﻿using ClangPowerTools.MVVM.Commands;
using ClangPowerTools.MVVM.Models;
using ClangPowerTools.MVVM.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClangPowerTools
{
  public class DetectStyleFileSelectorViewModel : CommonSettingsFunctionality
  {
    #region Members

    private readonly DetectStyleFileSelectorView view;

    private ICommand browseCommand;
    private ICommand removeAllFilesCommand;
    private ICommand detectFormatStyleCommand;

    long totalFilesSize = 0;
    private const long MAX_FILE_SIZE = 1000000; //  1 MB
    private const int MAX_SIZE_FILE_PATH = 90;

    #endregion


    #region Constructor

    public DetectStyleFileSelectorViewModel() { }

    public DetectStyleFileSelectorViewModel(DetectStyleFileSelectorView view)
    {
      this.view = view;
      view.DetectFormatStyleButton.IsEnabled = false;
      view.RemoveAllSection.IsEnabled = false;
    }

    #endregion


    #region Properties

    public ObservableCollection<SelectedFileModel> SelectedFiles { get; set; } = new ObservableCollection<SelectedFileModel>();

    public ICommand Browse
    {
      get => browseCommand ?? (browseCommand = new RelayCommand(() => BrowseForFiles(), () => CanExecute));
    }
    public ICommand RemoveAllFilesCommand
    {
      get => removeAllFilesCommand ?? (removeAllFilesCommand = new RelayCommand(() => RemoveAllFiles(), () => CanExecute));
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

    private void BrowseForFiles()
    {
      string[] filePaths = OpenFiles(string.Empty, ".cpp", ScriptConstants.FileExtensionsSelectFile);

      if (filePaths == null || filePaths.Length <= 0)
        return;

      for (int index = 0; index < filePaths.Length; ++index)
      {
        if (IsDuplicate(filePaths[index]))
          continue;

        string filePathToShow = CreateMiddleEllipsis(filePaths[index]);
        AddNewElement(filePaths[index], filePathToShow);
      }

      ChangeButtonsState(SelectedFiles.Count > 0);

      if (totalFilesSize > MAX_FILE_SIZE &&
        view.WarningTextBox.Visibility != System.Windows.Visibility.Visible)
      {
        view.WarningTextBox.Visibility = System.Windows.Visibility.Visible;
        var warning = new FileSizeWarningView(view);
        warning.Show();
      }
    }

    private bool IsDuplicate(string filePath) => SelectedFiles.FirstOrDefault(model => model.FilePath == filePath) != null;

    private void AddNewElement(string filePath, string filePathToShow)
    {
      var model = new SelectedFileModel(filePath, filePathToShow);
      totalFilesSize += model.FileSize;
      SelectedFiles.Add(model);
    }

    private string CreateMiddleEllipsis(string filePath)
    {
      if (filePath.Length <= MAX_SIZE_FILE_PATH)
        return filePath;

      while (filePath.Length > MAX_SIZE_FILE_PATH)
        filePath = filePath.Remove(filePath.Length / 2, 1);

      var begin = filePath.Substring(0, filePath.Length / 2);
      begin = begin.Reverse().SubstringAfter("\\").Reverse();

      var end = filePath.Substring(filePath.Length / 2);
      end = end.SubstringAfter("\\");

      return $"{begin}\\...\\{end}";
    }

    private void RemoveAllFiles()
    {
      SelectedFiles.Clear();
      view.WarningTextBox.Visibility = System.Windows.Visibility.Hidden;
      ChangeButtonsState(false);
    }

    public void RemoveFile(int index)
    {
      if (index < 0 || index >= SelectedFiles.Count)
        return;

      SelectedFileModel model = SelectedFiles[index];
      totalFilesSize -= model.FileSize;
      SelectedFiles.RemoveAt(index);

      ChangeButtonsState(SelectedFiles.Count != 0);

      if (totalFilesSize <= MAX_FILE_SIZE)
        view.WarningTextBox.Visibility = System.Windows.Visibility.Hidden;
    }

    private async Task DetectFormatStyleAsync()
    {
      var diffWin = new DiffWindow();
      List<string> filesPath = SelectedFiles.Select(model => model.FilePath).ToList();

      view.IsEnabled = false;
      await diffWin.ShowDiffAsync(filesPath, view);
      view.IsEnabled = true;
    }

    private void ChangeButtonsState(bool stateFlag)
    {
      view.DetectFormatStyleButton.IsEnabled = stateFlag;
      view.RemoveAllSection.IsEnabled = stateFlag;
      view.RemoveAllTextBlock.Foreground = stateFlag ?
        System.Windows.Media.Brushes.Black : System.Windows.Media.Brushes.Gray;
    }

    #endregion

  }
}
