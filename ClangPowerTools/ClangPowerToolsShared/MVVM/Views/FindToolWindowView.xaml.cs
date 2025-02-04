﻿using ClangPowerToolsShared.Commands;
using ClangPowerToolsShared.MVVM.Interfaces;
using ClangPowerToolsShared.MVVM.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

    public void OpenFindToolWindow() { }

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

    private void ComboBox_Loaded(object sender, RoutedEventArgs e)
    {
      var combo = sender as ComboBox;
      combo.ItemsSource = LookInMenuController.MenuOptions;
      combo.SelectedIndex = 0;
    }

    private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
    {
      if(menu.SelectedItem != null)
      {
        var item = menu.SelectedItem as ClangPowerToolsShared.Commands.MenuItem;
        LookInMenuController.SetSelectedOption(item);
      }
    }

    private void Matcher_Click(object sender, RoutedEventArgs e)
    {
      var item = (sender as ListView).SelectedItem as IViewMatcher;
      if (item != null)
      {
        findToolWindowViewModel.SelectCommandToRun(item);
      }
    }
  }
}
