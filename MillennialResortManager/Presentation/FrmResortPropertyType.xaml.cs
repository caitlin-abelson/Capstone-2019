using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataObjects;
using LogicLayer;

namespace Presentation
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/09
    /// 
    /// Interaction logic for FrmResortPropertyType.xaml
    /// </summary>
    public partial class FrmResortPropertyType : UserControl
    {
        private Employee _employee;
        private List<ResortPropertyType> _resortPropertyTypes;
        private readonly IResortPropertyTypeManager _resortPropertyTypeManager;
        private bool _isBtnNewInNewMode;
        private string _errorText = "";

        private int _currentIndex;

        public FrmResortPropertyType()
        {
            InitializeComponent();

            _resortPropertyTypeManager = new ResortPropertyTypeManager();
        }

        #region Core Logic

        private void BtnPrevious_OnClick(object sender, RoutedEventArgs e)
        {
            if (_resortPropertyTypes != null)
                _currentIndex = (_currentIndex - 1 == -1) ? _resortPropertyTypes.Count - 1 : _currentIndex - 1;

            SetFormContext();
        }

        private void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            if (_resortPropertyTypes != null)
                _currentIndex = (_currentIndex + 1) % _resortPropertyTypes.Count;

            SetFormContext();
        }

        private void DtgResortProperties_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentIndex = dtgResortProperties.SelectedIndex;

            SetFormContext();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            bool success = ValidateArgs();

            if (success)
            {
                var resortProperty = new ResortPropertyType
                {
                    ResortPropertyTypeId = txtResortPropertyType.Text
                };

                if (Save(resortProperty, out string errorStr))
                {
                    MessageBox.Show("Resort Property Type saved successfully");

                    RefreshFormAtCurrentIndex();

                    EnableDirectionNavigation();

                    ToggleNowCancelButtonToNew();
                }
                else
                {
                    MessageBox.Show(
                        $"{(_isBtnNewInNewMode ? "Failed to create resort property type" : "Failed to update resort property type")}\n{errorStr}");
                }
            }
            else
            {
                MessageBox.Show(_errorText);
            }
        }

        private bool ValidateArgs()
        {
            var result = true;

            int resortPropertyTypeMaxLength =
                (int)((ResortPropertyTypeManager)_resortPropertyTypeManager)
                    .GetValidationCriteria()[nameof(ResortPropertyType.ResortPropertyTypeId)].UpperBound;

            if (string.IsNullOrEmpty(txtResortPropertyType.Text))
            {
                _errorText = $"Resort Property Cannot Be Empty\n";
                result = false;
            }
            else if (txtResortPropertyType.Text.Length > resortPropertyTypeMaxLength)
            {
                _errorText += $"Resort Property Type cannot be more than {resortPropertyTypeMaxLength} characters\n";
                result = false;
            }

            return result;
        }

        private void BtnNew_OnClick(object sender, RoutedEventArgs e)
        {
            DisableDirectionNavigation();

            if (!_isBtnNewInNewMode)
            {
                if (_resortPropertyTypes != null)
                    _currentIndex = _resortPropertyTypes.Count;

                this.resortPropertyTypeForm.DataContext = new ResortPropertyType();

                ToggleNewButtonToCancel();
            }
            else
            {
                SetupWindow();

                ToggleNowCancelButtonToNew();
            }
        }

        private void TxtFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string filterTxt = txtFilter.Text.ToLower();

            if (_resortPropertyTypes == null)
                return;

            var filteredDtg = _resortPropertyTypes.Where(
                x => x.ResortPropertyTypeId.ToLower().Contains(filterTxt)).ToList();

            dtgResortProperties.ItemsSource = filteredDtg;

            if (filteredDtg.Count != 0)
            {
                _currentIndex = 0;
                SetFormContext();
            }
            else
            {
                this.resortPropertyTypeForm.DataContext = new ResortPropertyType();
            }
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var resortProperty = ((FrameworkElement)sender).DataContext as ResortPropertyType;

            try
            {
                if (resortProperty != null)
                    _resortPropertyTypeManager.DeleteResortPropertyType(resortProperty.ResortPropertyTypeId, _employee);
                else
                {
                    MessageBox.Show("Unknown Error");
                    return;
                }

                MessageBox.Show("Deleted Successfully");

                SetupWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete\n{ex.Message}");
            }
        }

        #endregion

        #region Window Setup

        public void SetupForm(Employee employee)
        {
            _employee = employee;

            SetupWindow();
        }

        private void SetupWindow()
        {

            RefreshDataGrid();

            InitializeFormContext();
        }

        private void InitializeFormContext()
        {
            if (_resortPropertyTypes?.Count == 0)
                return;

            if (_resortPropertyTypes != null)
            {
                _currentIndex = 0;
                SetFormContext();
            }

        }

        private void RefreshDataGrid()
        {
            _resortPropertyTypes = GetResortPropertyTypes();

            dtgResortProperties.ItemsSource = _resortPropertyTypes;
        }

        #endregion

        #region External Resource Facing Functions

        private List<ResortPropertyType> GetResortPropertyTypes()
        {
            List<ResortPropertyType> resortPropertyTypes = null;

            try
            {
                resortPropertyTypes = _resortPropertyTypeManager.RetrieveResortPropertyTypes().ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failed to retrieve resort property types\n {e.Message}");
            }

            return resortPropertyTypes.Count == 0 ? null : resortPropertyTypes;
        }

        private bool Save(ResortPropertyType resortPropertyType, out string errorStr)
        {
            try
            {
                errorStr = "";

                if (_isBtnNewInNewMode)
                    _resortPropertyTypeManager.AddResortPropertyType(resortPropertyType);
                else
                    _resortPropertyTypeManager.UpdateResortPropertyType(_resortPropertyTypes[_currentIndex], resortPropertyType);

                return true;
            }
            catch (Exception e)
            {
                errorStr = $"{e.Message}\n{e.StackTrace}";
            }

            return false;
        }

        #endregion

        #region Helpers

        private void SetFormContext()
        {
            if (_resortPropertyTypes != null)
            {
                var decoupler = _resortPropertyTypes[_currentIndex].DeepClone();

                this.resortPropertyTypeForm.DataContext = decoupler;
            }

            HighlightRow();
        }

        private void EnableDirectionNavigation()
        {
            btnNext.IsEnabled = true;
            btnPrevious.IsEnabled = true;
        }

        private void DisableDirectionNavigation()
        {
            btnNext.IsEnabled = false;
            btnPrevious.IsEnabled = false;
        }

        private void RefreshFormAtCurrentIndex()
        {
            RefreshDataGrid();

            SetFormContext();
        }

        private void HighlightRow()
        {
            if (_resortPropertyTypes != null)
                dtgResortProperties.SelectedItem = _resortPropertyTypes[_currentIndex];
        }

        private void ToggleNewButtonToCancel()
        {
            _isBtnNewInNewMode = true;

            btnAdd.Content = "Cancel";
        }

        private void ToggleNowCancelButtonToNew()
        {
            _isBtnNewInNewMode = false;

            btnAdd.Content = "New";
        }
        #endregion
    }
}
