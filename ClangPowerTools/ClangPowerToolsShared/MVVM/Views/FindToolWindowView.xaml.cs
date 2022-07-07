﻿using ClangPowerToolsShared.Commands;
using ClangPowerToolsShared.MVVM.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ClangPowerTools.Views
{
  /// <summary>
  /// Interaction logic for CompilerSettingsView.xaml
  /// </summary>
  public partial class FindToolWindowView : UserControl
  {
    private FindToolWindowViewModel findToolWindowViewModel;

    public FindToolWindowView()
    {
      findToolWindowViewModel = new FindToolWindowViewModel(this);
      DataContext = findToolWindowViewModel;
      InitializeComponent();
    }

    public void OpenFindToolWindow(List<string> filesPath)
    {
      findToolWindowViewModel.OpenToolWindow(filesPath);
    }

    public void RunQuery()
    {
      findToolWindowViewModel.RunQuery();
    }

    private void MatchDefaultArgs_click(object sender, RoutedEventArgs e)
    {
      findToolWindowViewModel.RunCommandFromView();
    }

    private void Hyperlink_Feedback(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
    {
      Process.Start(new ProcessStartInfo("https://clangpowertools.com/contact.html"));
      e.Handled = true;
    }

    private void Matcher_Click(object sender, RoutedEventArgs e)
    {
      var item = (sender as ListView).SelectedItem;
      if (item != null)
      {
        var type = item.GetType();
        if (type.IsGenericType)
        {
          if (type == typeof(KeyValuePair<int,string>))
          {
            var key = type.GetProperty("Key");
            var value = type.GetProperty("Value");
            var keyObj = key.GetValue(item, null);
            var valueObj = value.GetValue(item, null);
            var keyValueResult =  new KeyValuePair<object, object>(keyObj, valueObj);
            findToolWindowViewModel.SelectCommandToRun((int)keyValueResult.Key);
          }
        }
      }
    }
  }
}
